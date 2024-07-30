using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;
using Model.Dao;
using Model.EF;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Monitor.Controllers
{
    public class UsersController : Controller
    {
        private PhongbanDAO phongbanDAO;
        private DonviDAO donviDAO;


        public UsersController()
        {
            phongbanDAO = new PhongbanDAO();
            donviDAO = new DonviDAO();
        }

        public ActionResult User()
        {
            return View();
        }

        //Phòng ban
        public ActionResult GetPhongbans([DataSourceRequest] DataSourceRequest request)
        {
            var phongbans = phongbanDAO.GetPhongbans();
            return Json(phongbans.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddDepartment(Phongban department)
        {
            // Kiểm tra xem có phòng ban trùng tên trong đơn vị không
            //if (phongbanDAO.IsDuplicateDepartmentNameInUnit(department.id_donvi, department.name))
            //{
            //    return Json(new { success = false, message = "Tên phòng ban đã tồn tại trong đơn vị." });
            //}

            phongbanDAO.AddPhongban(department);
            return Json(new { success = true });
        }


        [HttpPost]
        public ActionResult UpdatePhongban(Phongban phongban)
        {
            phongbanDAO.UpdatePhongban(phongban);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeletePhongban(int id)
        {
            // Kiểm tra xem có người dùng nào thuộc phòng ban không
            if (phongbanDAO.HasUsersInDepartment(id))
            {
                return Json(new { success = false, message = "Phòng ban đang chứa người dùng. Không thể xóa." });
            }
            phongbanDAO.DeletePhongban(id);
            return Json(new { success = true });
        }


        //Đơn vị
        public ActionResult GetUnit([DataSourceRequest] DataSourceRequest request)
        {
            var units = donviDAO.GetUnits();
            return Json(units.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

    
        [HttpPost]
        public ActionResult AddUnit(Donvi unit)
        {
            // Kiểm tra xem có đơn vị trùng tên không
            if (donviDAO.IsDuplicateUnitName(unit.name))
            {
                return Json(new { success = false, message = "Tên đơn vị đã tồn tại." });
            }
            donviDAO.AddUnit(unit);
            return Json(new { success = true });
        }


        [HttpPost]
        public ActionResult UpdateUnit(Donvi unit)
        {
            donviDAO.UpdateUnit(unit);
            return Json(new { success = true });
        }


        [HttpPost]
        public ActionResult DeleteUnit(int id)
        {
            // Kiểm tra xem có phòng ban nào thuộc đơn vị không
            if (phongbanDAO.HasDepartmentsInUnit(id))
            {
                return Json(new { success = false, message = "Đơn vị đang chứa các phòng ban. Không thể xóa." });
            }
            donviDAO.DeleteUnit(id);
            return Json(new { success = true });
        }



        //Dropdownlist Unit

        public ActionResult PopulateUnitList()
        {
            var units = donviDAO.GetUnits();
            var list = JsonConvert.SerializeObject(units,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }


        public ActionResult PopulateDepartbyID([DataSourceRequest] DataSourceRequest request, string unitID)
        {
            List<Phongban> phongBan;
            if (string.IsNullOrEmpty(unitID))
            {
                phongBan = phongbanDAO.GetPhongbans();
            }
            else
            {
                try
                {
                    int tempUnit = Convert.ToInt32(unitID);
                    phongBan = phongbanDAO.GetDepBYUnitID(tempUnit);
                }
                catch (FormatException ex)
                {

                    Console.WriteLine($"Error converting string to integer: {ex.Message}");
                    phongBan = new List<Phongban>();
                }
            }
            return Json(phongBan.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }




        //

        public ActionResult PopulateDepartList()
        {
            var departs = phongbanDAO.GetPhongbans();
            var list = JsonConvert.SerializeObject(departs,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");

        }


        //Searchbar Dep
        [HttpPost]
        public ActionResult SearchDep(string searchText)
        {
            var projects = phongbanDAO.SearchDep(searchText);
            return Json(projects);
        }

        //Searchbar Unit
        [HttpPost]
        public ActionResult SearchUnit(string searchText)
        {
            var units = donviDAO.SearchUnit(searchText);
            return Json(units);
        }

    }
}
