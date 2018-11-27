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

using System.Security.Cryptography;
using DayPilot.Web.Mvc.Json;
using DayPilot.Web.Ui;
using JobPortal.Model;

namespace MyWeb.Controllers
{

    public class CalendarController : Controller
    {
        private OfferReference.IOfferService _offerProxy = new OfferReference.OfferServiceClient("OfferServiceHttpEndpoint");
        public ActionResult Backend()
        {
            return new Dpc().CallBack(this);
        }

        class Dpc : DayPilot.Web.Mvc.DayPilotCalendar
        {
            protected override void OnInit(InitArgs e)
            {

                UpdateWithMessage("Welcome!", CallBackUpdateType.Full);
            }
        }
       
           

           // public ActionResult Edit(string id)
           // {
           //     var ids = Convert.ToInt32(id);
           //     var t = (from tr in dc.Events where tr.Id == ids select tr).First();
           //     var ev = new EventData
           //     {
           //         Id = t.Id,
           //         Start = t.Start,
           //         End = t.End,
           //         Text = t.Text
           //     };
           //     return View(ev);
           // }
          //
          //  [AcceptVerbs(HttpVerbs.Post)]
          //  public ActionResult Edit(FormCollection form)
          //  {
          //      int id = Convert.ToInt32(form["Id"]);
          //      DateTime start = Convert.ToDateTime(form["Start"]);
          //      DateTime end = Convert.ToDateTime(form["End"]);
          //      string text = form["Text"];
          //
          //      var record = (from e in dc.Events where e.Id == id select e).First();
          //      record.Start = start;
          //      record.End = end;
          //      record.Text = text;
          //      dc.SubmitChanges();
          //
          //      return JavaScript(SimpleJsonSerializer.Serialize("OK"));
          //  }


            public ActionResult AddHours(DateTime start, DateTime end, int serviceId )
            {

            DayOfWeek wd = start.DayOfWeek;
            TimeSpan starttime = start.TimeOfDay;
            TimeSpan endtime = end.TimeOfDay;
            int serId = serviceId;
            if(_offerProxy.AddHoursToOffer(new WorkingTime { WeekDay = wd, HoursFrom = starttime, HoursTo = endtime, OfferId = serId }))
                return JavaScript(SimpleJsonSerializer.Serialize("OK"));
            return null;
        }

            

            public class EventData
            {
                public int Id { get; set; }
                public DateTime Start { get; set; }
                public DateTime End { get; set; }
                public int  ServiceId { get; set; }
                public string EmployeeId { get; set; }
            }

        }
    }