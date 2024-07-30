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
using DocumentFormat.OpenXml.Bibliography;

namespace Monitor.Controllers
{
    public class ShipController : Controller
    {
        private DevicesDAO devicesDao;
        private CustomerDAO customerDAO;

        public ShipController()
        {
            devicesDao = new DevicesDAO();
            customerDAO = new CustomerDAO();
        }

        public ActionResult Overview()
        {
            return View();
        }

        public ActionResult GetAllDevices([DataSourceRequest] DataSourceRequest request)
        
        {
            var DevData = devicesDao.GetAllDevices().Where(d => d.Status == "Available"); // Filter devices by status "Available"
            return Json(DevData.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateDevice(Device device)
        {
            devicesDao.CreateDevice(device);
            return Json(device);
        }

        [HttpPost]
        public ActionResult UpdateDevice(Device device)
        {
            devicesDao.UpdateDevice(device);
            return Json(device);
        }

        [HttpPost]
        public ActionResult DeleteDevice(int deviceID)
        {
            try
            {
                devicesDao.DeleteDevice(deviceID);
                return Json(new { success = true, message = "Device deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult PopulateDevicebyID([DataSourceRequest] DataSourceRequest request, string subProjectID)
        {
            List<Device> devices;
            int temp;
            if (string.IsNullOrEmpty(subProjectID))
            {
                devices = devicesDao.GetAllDevices().Where(d => d.Status == "Available").ToList(); // Filter devices by status "Available"
            }
            else
            {
                temp = Convert.ToInt32(subProjectID);
                devices = devicesDao.GetDevicebySubProjectsID(temp).Where(d => d.Status == "Available").ToList(); // Filter devices by status "Available"
            }
            return Json(devices.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateCustomerList()
        {
            var customers = customerDAO.GetCustomers().Select(c => new
            {
                ID = c.ID,
                Name = c.Name,
                Unit = c.Unit,
                Department = c.Department,
            }).ToList();

            var list = JsonConvert.SerializeObject(customers, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Content(list, "application/json");
        }
    }
}
