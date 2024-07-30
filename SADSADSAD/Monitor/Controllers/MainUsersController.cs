using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Model.Dao;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class MainUsersController : Controller
    {

        private UserDAO userDAO;
        private PhongbanDAO phongbanDAO;

        public MainUsersController()
        {
            userDAO = new UserDAO();
            phongbanDAO = new PhongbanDAO();
        }

        public ActionResult GetUser([DataSourceRequest] DataSourceRequest request)
        {
            var users = userDAO.GetUsers();
            return Json(users.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult User()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser([DataSourceRequest] DataSourceRequest request, User user)
        {
            if (userDAO.GetUsers().Any(u => u.id_thietbi == user.id_thietbi))
            {
                return Json(new { success = false, message = "Device ID must be unique." });
            }

            if (ModelState.IsValid)
            {
                userDAO.AddUser(user);
                return Json(new[] { user }.ToDataSourceResult(request, ModelState));
            }
            return Json(ModelState.ToDataSourceResult());
        }


        [HttpPost]
        public ActionResult UpdateUser([DataSourceRequest] DataSourceRequest request, User user)
        {
            if (userDAO.GetUsers().Any(u => u.id_thietbi == user.id_thietbi && u.id != user.id))
            {
                return Json(new { success = false, message = "Device ID must be unique." });
            }

            if (ModelState.IsValid)
            {
                userDAO.UpdateUser(user);
                return Json(new[] { user }.ToDataSourceResult(request, ModelState));
            }
            return Json(ModelState.ToDataSourceResult());
        }

        [HttpPost]
        public ActionResult DeleteUser([DataSourceRequest] DataSourceRequest request, User user)
        {
            userDAO.DeleteUser(user.id);
            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult PopulateUserbyID([DataSourceRequest] DataSourceRequest request, string DepartID, string UnitID)
        {
            List<User> users;
            int tempu = 0; // Khởi tạo tempu với giá trị mặc định

            if (string.IsNullOrEmpty(DepartID))
            {
                users = userDAO.GetUsers();
            }
            else
            {
                tempu = Convert.ToInt32(DepartID);
                users = userDAO.GetUsersByUnit(tempu);
            }

            ViewBag.DepartmentId = tempu;

            return Json(users.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateDepartList()
        {
            var departments = phongbanDAO.GetPhongbans();
            return Json(departments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateUserList()
        {
            var users = userDAO.GetUsers();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDepartmentName(int departmentId)
        {
            var department = phongbanDAO.GetDepartmentById(departmentId); // Thay đổi phương thức này tương ứng với DAO của bạn
            if (department != null)
            {
                return Json(department.name, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
