using Kendo.Mvc.UI;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Monitor.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        private CustomerDAO customerDAO;

        public ActionResult Customer()
        {
            return View();
        }




        [HttpPost]
        public ActionResult DeleteCus(int id)
        {
            customerDAO.DeleteCustomer(id);
            return Json(new { success = true });
        }


        [HttpPost]
        public ActionResult SendNotification(string description)
        {
            // Lưu thông báo vào Session
            List<string> notifications = Session["Notifications"] as List<string> ?? new List<string>();
            notifications.Add(description);
            Session["Notifications"] = notifications;

            return Json(new { success = true });
        }


    }

}
