using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using Microsoft.AspNet.Identity;
using System.Security.Cryptography;
using DayPilot.Web.Mvc.Json;
using DayPilot.Web.Ui;
using JobPortal.Model;


namespace MyWeb.Controllers
{

    public class CalendarController : Controller
    {
        private OfferReference.IOfferService _offerProxy = new OfferReference.OfferServiceClient("OfferServiceHttpEndpoint");
        public ActionResult Backend(int serviceId)
        {
            return new Dpc(serviceId).CallBack(this);
        }



        public ActionResult AddHours(DateTime startT, DateTime endT, int serviceId)
        {

            DayOfWeek wd = startT.DayOfWeek;
            TimeSpan starttime = startT.TimeOfDay;
            TimeSpan endtime = endT.TimeOfDay;
            int serId = serviceId;
            if (_offerProxy.AddHoursToOffer(new WorkingTime { WeekDay = wd,
                Start = starttime, End = endtime, OfferId = serId }))
                return JavaScript(SimpleJsonSerializer.Serialize("Hours are added"));
            return JavaScript(SimpleJsonSerializer.Serialize("You can't add working hours " 
                + starttime + "- " + endtime + "for " + wd));
        }

    }




    class Dpc : DayPilot.Web.Mvc.DayPilotCalendar
    {
        private OfferReference.IOfferService _offerProxy = new OfferReference.OfferServiceClient("OfferServiceHttpEndpoint");
        private int _serviceId;
        public Dpc(int serviceId)
        {
            this._serviceId = serviceId;
        }
        protected override void OnInit(InitArgs e)
        {
            WeekStarts = (WeekStarts)WeekStarts.Parse(typeof(WeekStarts), "" + (int)DateTime.Now.AddDays(7).DayOfWeek);
            UpdateWithMessage("Choose your working hours!", CallBackUpdateType.Full);
        }

        protected override void OnCommand(CommandArgs e)
        {
            switch (e.Command)
            {
                case "refresh":
                    Update();
                    break;
            }
        }

        
        protected override void OnFinish()
        {
            if (UpdateType == CallBackUpdateType.None)
            {
                return;
            }

            DataIdField = "Id";
            DataStartField = "Start";
            DataEndField = "End";
            DataTextField = "Text";

            var days = _offerProxy.GetAllWorkingDays().Where(x => x.OfferId == _serviceId);
            DateTime date = DateTime.Now;
            IList<WorkingDate> list = new List<WorkingDate>();
            foreach (var day in days)
            {
                DateTime startdate = DateTime.Now;
                DateTime enddate = DateTime.Now;

                if (day.WeekDay == DateTime.Now.DayOfWeek)
                {
                    startdate= startdate.Subtract(TimeSpan.FromDays(7)).Date.Add(day.Start);
                    enddate= enddate.AddDays(-7).Date.Add(day.End);
                }
                else if (day.WeekDay == DateTime.Now.AddDays(1).DayOfWeek)
                {
                    startdate = startdate.AddDays(-6).Date.Add(day.Start);
                    enddate  = enddate.AddDays(-6).Date.Add(day.End);

                }
                else if (day.WeekDay == DateTime.Now.AddDays(2).DayOfWeek)
                {
                    startdate = startdate.AddDays(-5).Date.Add(day.Start);
                    enddate  =  enddate.AddDays(-5).Date.Add(day.End);

                }
                else if (day.WeekDay == DateTime.Now.AddDays(3).DayOfWeek)
                {
                    startdate = startdate.AddDays(-4).Date.Add(day.Start);
                    enddate  = enddate.AddDays(-4).Date.Add(day.End);

                }
                else if (day.WeekDay == DateTime.Now.AddDays(4).DayOfWeek)
                {
                    startdate = startdate.AddDays(-3).Date.Add(day.Start);
                    enddate  = enddate.AddDays(-3).Date.Add(day.End);

                }
                else if (day.WeekDay == DateTime.Now.AddDays(5).DayOfWeek)
                {
                    startdate = startdate.AddDays(-2).Date.Add(day.Start);
                    enddate  =  enddate.AddDays(-2).Date.Add(day.End);

                }
                else if (day.WeekDay == DateTime.Now.AddDays(6).DayOfWeek)
                {
                    startdate = startdate.AddDays(-1).Date.Add(day.Start);
                    enddate  =  enddate.AddDays(-1).Date.Add(day.End);

                }

                list.Add(new WorkingDate { Id = day.Id, OfferId = day.OfferId, Text = day.Text, Start = startdate, End = enddate });

                
            }
            Events = list;
        }



    }
}
