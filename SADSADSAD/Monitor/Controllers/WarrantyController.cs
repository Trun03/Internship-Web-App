using Model.Dao;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Model.DAO;

namespace Monitor.Controllers
{
    public class WarrantyController : Controller
    {
        private DevicesDAO devicesDao;
        private CustomerDAO customerDAO;

        public WarrantyController()
        {
            devicesDao = new DevicesDAO();
            customerDAO = new CustomerDAO();
        }

        public ActionResult Warranty()
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
                ViewBag.DeviceList = new SelectList(deviceList, "DeviceID", "Name", "Serial");
                ViewBag.SubProjectList = new SelectList(subProjectList, "SubProjectID", "Name");
                ViewBag.ProjectList = new SelectList(projectList, "id", "namepj");

                return PartialView(users);
            }
        }

        public ActionResult GetAllDevices([DataSourceRequest] DataSourceRequest request)
        {
            var DevData = devicesDao.GetAllDevices().Where(d => d.Status == "Unavailable");
            return Json(DevData.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUsers([DataSourceRequest] DataSourceRequest request, List<int> selectedIds)
        {
            using (var dbContext = new InternshipDbContext())
            {
                var users = dbContext.Users.AsQueryable();

                // Filter out the selected users
                if (selectedIds != null && selectedIds.Count > 0)
                {
                    users = users.Where(u => !selectedIds.Contains(u.id));
                }

                return Json(users.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
