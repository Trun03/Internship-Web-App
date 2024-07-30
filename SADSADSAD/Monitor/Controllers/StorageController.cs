using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Model.Dao;
using Model.EF;

namespace Monitor.Controllers
{
    public class StorageController : Controller
    {
        private StorageDAO storageDAO;

        public StorageController()
        {
            storageDAO = new StorageDAO();
        }

        public ActionResult Storage()
        {
            return View();
        }

        public ActionResult GetDevice([DataSourceRequest] DataSourceRequest request, string search = null)
        {
            var devices = storageDAO.GetDevices();

            if (!string.IsNullOrEmpty(search))
            {
                devices = devices.Where(d => d.Device_Name.Contains(search) || d.Serial_No.Contains(search)).ToList();
            }

            return Json(devices.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreatePro([DataSourceRequest] DataSourceRequest request, Storage device)
        {
            if (device != null && ModelState.IsValid)
            {
                device.ImportDate = DateTime.Now; // Set ImportDate to current date if needed
                storageDAO.CreateDevice(device);
            }
            return Json(new[] { device }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult UpdatePro([DataSourceRequest] DataSourceRequest request, Storage device)
        {
            if (device != null && ModelState.IsValid)
            {
                storageDAO.UpdateDevice(device);
            }
            return Json(new[] { device }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult DeletePro([DataSourceRequest] DataSourceRequest request, Storage device)
        {
            if (device != null)
            {
                storageDAO.DeleteDevice(device.Id);
            }
            return Json(new[] { device }.ToDataSourceResult(request, ModelState));
        }
    }
}
