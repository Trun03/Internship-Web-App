using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Monitor.Controllers
{
    public class NotificationController : Controller
    {
        public JsonResult GetLatestNotifications()
        {
            // Replace this with your logic to fetch notifications from the database
            var notifications = new List<string>
        {
            "New user registered",
            "Device borrowed"
        };

            return Json(notifications, JsonRequestBehavior.AllowGet);
        }
    }

}