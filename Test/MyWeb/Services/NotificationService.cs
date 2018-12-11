using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading;
using System.Text;
using Repository;
using System.Configuration;
using Repository.DbConnection;
using System.Net.Mail;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace ServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    public class NotificationService : INotificationService
    {
        private static IUnitOfWork _unitOfWork;
        private Thread notifyBeforeWork;
        private Thread notifyUser;
        private bool InitCalled = false;
        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Init();
        }
        public NotificationService()
        {
            _unitOfWork = new UnitOfWork(new JobPortalDatabaseDataContext());
            Init();
        }
        public void Init()
        {
            if (InitCalled == false)
            {
                notifyBeforeWork = new Thread(new ThreadStart(NotifyBeforeWork));
                notifyBeforeWork.Start();
                notifyUser = new Thread(new ThreadStart(NotifyUnactiveUser));
                notifyUser.Start();
                InitCalled = true;
            }
           
        }
        public static void NotifyBeforeWork()
        {
            IScheduler scheduler = Scheduler.GetInstance();
            IJobDetail job = JobBuilder.Create<Notification>()
                .WithIdentity("name2", "group2")
                .UsingJobData("name2", "Bob2")
                .UsingJobData("count2", 1)
                .Build();
            ITrigger trigger = TriggerBuilder
                .Create()
                .WithCronSchedule("0 0 22  1/1 * ? *")
                .StartNow()
                .Build();

            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
            Thread.Sleep(TimeSpan.FromMinutes(10));
            scheduler.Standby();
        }
        public static void NotifyUnactiveUser()
        {
            Thread.Sleep(5000);
            IScheduler scheduler = Scheduler.GetInstance();
            IJobDetail job = JobBuilder.Create<UserReminder>()
                .WithIdentity("name", "group")
                .UsingJobData("name", "Bob")
                .UsingJobData("count", 1)
                .Build();
            ITrigger trigger = TriggerBuilder
                .Create()
                .WithCronSchedule("0 37 13 ? * MON * ")
                .StartNow()
                .Build();

            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
            Thread.Sleep(TimeSpan.FromMinutes(10));
            scheduler.Standby();
        }
    }

    public class Notification : IJob
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new JobPortalDatabaseDataContext());
        public Task Execute(IJobExecutionContext context)
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                var allSalelinesForNextDay = _unitOfWork.Orders.GetAllSalelines().Where(x => x.BookedDate.BookedDate1.Date == (DateTime.Now.Date.AddDays(1)));
                foreach (var saleline in allSalelinesForNextDay)
                {
                    var email = saleline.ServiceOffer.AspNetUsers.Email;
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(ConfigurationManager.AppSettings["Glogin"], "JobPortal");
                    mail.To.Add(new MailAddress(email, "Receiver"));
                    mail.Subject = "JobPortal";
                    mail.Body = "Hi! We would like to remind you that you have some work tomorrow. Kind regards, Job Portal  ";
                    mail.Priority = MailPriority.Normal;

                    using (SmtpClient MailClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        MailClient.EnableSsl = true;
                        MailClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Glogin"], ConfigurationManager.AppSettings["Gpassowrd"]);
                        MailClient.Send(mail);
                    }

                }
                return true;
            });

        }
    }
    public class UserReminder : IJob
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new JobPortalDatabaseDataContext());
        public Task Execute(IJobExecutionContext context)
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                var emails = _unitOfWork.Orders.GetAllOrders().Where(x => x.Date.Value.Date < DateTime.Now.Date.AddMonths(-2)).Select(x => x.AspNetUsers.Email);
                foreach (var email in emails.Distinct())
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(ConfigurationManager.AppSettings["Glogin"], "JobPortal");
                    mail.To.Add(new MailAddress(email, "Receiver"));
                    mail.Subject = "JobPortal";
                    mail.Body = "Hi! We saw that you haven't used our website for a long time. You are encouraged to visit us and check new service offers. Kind regards, Job Portal  ";
                    mail.Priority = MailPriority.Normal;

                    using (SmtpClient MailClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        MailClient.EnableSsl = true;
                        MailClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Glogin"], ConfigurationManager.AppSettings["Gpassowrd"]);
                        MailClient.Send(mail);
                    }

                }
                return true;
            });

        }
    }
    public static class Scheduler
    {
        private static IScheduler instance;
        public static IScheduler GetInstance()
        {
            if (instance == null)
            {
                instance = new StdSchedulerFactory().GetScheduler().Result;
            }
            return instance;
        }
    }

}

