using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Model.Dao;
using Model.EF;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace YourNamespace.Controllers
{
    public class HomeController : Controller
    {
        private DevicesDAO devicesDao;
        private CauhinhDAO cauhinhDao;
        private SubProjectDAO subProjectDao;
        private ProDao proDao;

        public HomeController()
        {
            devicesDao = new DevicesDAO();
            cauhinhDao = new CauhinhDAO();
            subProjectDao = new SubProjectDAO();
            proDao = new ProDao();
        }

        public ActionResult Index()
        {
            return View();
        }

        //Dropdownlist PRO
        public ActionResult PopulateSuprobyID([DataSourceRequest] DataSourceRequest request, string proID)
        {
            List<SubProject> subProject;
            if (string.IsNullOrEmpty(proID))
            {
                subProject = subProjectDao.GetAllSubProject();
            }
            else
            {
                try
                {
                    int tempro = Convert.ToInt32(proID);
                    subProject = subProjectDao.GetSubProjectsBYProID(tempro);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error converting string to integer: {ex.Message}");
                    subProject = new List<SubProject>();
                }
            }
            return Json(subProject.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateProjectList()
        {
            var projects = proDao.GetProData();
            var list = JsonConvert.SerializeObject(projects,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }

        //Dropdownlist SbPRO
        public ActionResult PopulateDevicebyID([DataSourceRequest] DataSourceRequest request, string subProjectID)
        {
            List<Device> devices;
            int temp;
            if (subProjectID.Length == 0)
            {
                devices = devicesDao.GetAllDevices();
            }
            else
            {
                temp = Convert.ToInt32(subProjectID);
                devices = devicesDao.GetDevicebySubProjectsID(temp);
            }
            return Json(devices.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateSubprojectList()
        {
            var subprojects = subProjectDao.GetAllSubProject();
            var list = JsonConvert.SerializeObject(subprojects,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }

        //Dropdownlist Thiết Bị
        public ActionResult PopulateDeviceList()
        {
            var devices = devicesDao.GetAllDevices();
            var list = JsonConvert.SerializeObject(devices,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }

        //Dropdownlist Cấu hình
        public ActionResult PopulateCauhinhbyID([DataSourceRequest] DataSourceRequest request, string DeviceID)
        {
            List<Cauhinh> cauhinh;
            int tempdev;
            if (DeviceID.Length == 0)
            {
                cauhinh = cauhinhDao.GetCauHinhData();
            }
            else
            {
                tempdev = Convert.ToInt32(DeviceID);
                cauhinh = cauhinhDao.GetCauhinhByDeviceID(tempdev);
            }
            return Json(cauhinh.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        //Pro
        public ActionResult GetProData([DataSourceRequest] DataSourceRequest request)
        {
            var proData = proDao.GetProData();
            return Json(proData.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreatePro(Pro project)
        {
            try
            {
                if (proDao.IsDuplicateProjectName(project.namepj))
                {
                    return Json(new { success = false, message = "Tên Project đã tồn tại." });
                }
                else
                {
                    proDao.CreatePro(project);
                    return Json(new { success = true, message = "Project đã được tạo thành công." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi khi tạo Project: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdatePro(Pro project)
        {
            proDao.UpdatePro(project);
            return Json(project);
        }

        [HttpPost]
        public ActionResult DeletePro(int id)
        {
            try
            {
                if (proDao.ProjectContainsSubProjects(id))
                {
                    return Json(new { success = false, message = "Cannot delete Pro because it contains SubProjects." });
                }
                proDao.DeletePro(id);

                return Json(new { success = true, message = "Pro deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting Pro: " + ex.Message });
            }
        }

        //GOí thầu
        public ActionResult GetAllSubProject([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var subprojects = subProjectDao.GetAllSubProject();

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var json = JsonConvert.SerializeObject(subprojects, Formatting.None);

                return Json(subprojects.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving subprojects: {ex.Message}");
                return Json(new { errors = "An error occurred while retrieving subprojects." });
            }
        }

        [HttpPost]
        public ActionResult CreateSubProject(SubProject subpro)
        {
            try
            {
                if (subProjectDao.IsDuplicateSubProjectName(subpro.Idprj, subpro.Name))
                {
                    return Json(new { success = false, message = "Tên gói thầu đã tồn tại trong dự án." });
                }
                else
                {
                    subProjectDao.CreateSubProject(subpro);
                    return Json(subpro);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi khi tạo SubProject: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateSubProject(SubProject subpro)
        {
            try
            {
                if (subProjectDao.IsDuplicateSubProjectName(subpro.Idprj, subpro.Name, subpro.SubProjectID))
                {
                    return Json(new { success = false, message = "Tên gói thầu đã tồn tại trong dự án." });
                }
                else
                {
                    subProjectDao.UpdateSubProject(subpro);
                    return Json(subpro);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi khi cập nhật SubProject: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DeleteSubProject(int subProjectID)
        {
            try
            {
                if (!subProjectDao.SubProjectContainsDevices(subProjectID))
                {
                    subProjectDao.DeleteSubProject(subProjectID);
                    return Json(new { success = true, message = "SubProject deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Cannot delete SubProject because it contains devices." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting SubProject: " + ex.Message });
            }
        }

        //Cau hinh
        public ActionResult GetCauHinhData([DataSourceRequest] DataSourceRequest request)
        {
            var cauhinhData = cauhinhDao.GetCauHinhData();
            return Json(cauhinhData.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateCauHinh(Cauhinh cauhinh)
        {
            // Kiểm tra xem ID thiết bị đã tồn tại hay chưa
            if (cauhinhDao.GetCauHinhByIdDevice(cauhinh.id_Device) != null)
            {
                return Json(new { success = false, message = "ID thiết bị đã tồn tại" }, JsonRequestBehavior.AllowGet);
            }


            cauhinhDao.CreateCauHinh(cauhinh);
            return Json(cauhinh);
        }

        [HttpPost]
        public ActionResult UpdateCauHinh(Cauhinh cauhinh)
        {
            cauhinhDao.UpdateCauHinh(cauhinh);
            return Json(cauhinh);
        }

        [HttpPost]
        public ActionResult DeleteCauHinh(int id)
        {
            cauhinhDao.DeleteCauHinh(id);
            return Json(new { success = true });
        }

        //Thiết bị
        public ActionResult GetAllDevices([DataSourceRequest] DataSourceRequest request)
        {
            var DevData = devicesDao.GetAllDevices();
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

        //Searchbar Pro
        [HttpPost]
        public ActionResult SearchProject(string searchText)
        {
            var projects = proDao.SearchProjects(searchText);
            return Json(projects);
        }

        //Searchbar Gói thầu
        [HttpPost]
        public ActionResult SearchSubProject(string searchText)
        {
            var subprojects = subProjectDao.SearchSubProject(searchText);
            return Json(subprojects, JsonRequestBehavior.AllowGet);
        }

        //Searchbar Thiết bị
        [HttpPost]
        public ActionResult SearchDevices(string searchText)
        {
            var devices = devicesDao.SearchDevices(searchText);
            return Json(devices);
        }

        //Searchbar Cấu hình
        [HttpPost]
        public ActionResult SearchCauHinh(string searchText)
        {
            var cauhinhDAO = new CauhinhDAO();
            var cauhinhData = cauhinhDAO.GetCauHinhData();
            var filteredData = cauhinhData.Where(x => x.Chip.Contains(searchText) || x.RAM.Contains(searchText) || x.HDD.Contains(searchText) || x.SSD.Contains(searchText) || x.Main.Contains(searchText) || x.description.Contains(searchText)).ToList();
            return Json(filteredData);
        }
    }
}
