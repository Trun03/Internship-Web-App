using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Monitor.Controllers
{
    public class AllController : Controller
    {
        public ActionResult Overview()
        {
            using (var dbContext = new InternshipDbContext())
            {
                var users = dbContext.Users.ToList();
                var donviList = dbContext.Donvis.ToList();
                var phongbanList = dbContext.Phongbans.ToList();
                var deviceList = dbContext.Devices.ToList();
                var subProjectList = dbContext.SubProjects.ToList();
                var projectList = dbContext.Pros.ToList();

                ViewBag.DonviList = new SelectList(donviList, "id", "name");
                ViewBag.PhongbanList = new SelectList(phongbanList, "id", "name");
                ViewBag.DeviceList = new SelectList(deviceList, "DeviceID", "Name");
                ViewBag.SubProjectList = new SelectList(subProjectList, "SubProjectID", "Name");
                ViewBag.ProjectList = new SelectList(projectList, "id", "namepj");

                return PartialView(users);
            }
        }

        public ActionResult GetUsers([DataSourceRequest] DataSourceRequest request)
        {
            using (var dbContext = new InternshipDbContext())
            {
                var users = dbContext.Users.ToList();
                return Json(users.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDevices([DataSourceRequest] DataSourceRequest request)
        {
            using (var dbContext = new InternshipDbContext())
            {
                var devices = dbContext.Devices.ToList();
                return Json(devices.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
