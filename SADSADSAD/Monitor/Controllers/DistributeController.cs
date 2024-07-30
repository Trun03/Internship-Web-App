using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Model.Dao;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Monitor.Controllers
{
    public class DistributeController : Controller
    {
        private ProDao proDAO;
        private SubProjectDAO subProjectDao;
        private StorageDAO storageDAO;

        public DistributeController()
        {
            proDAO = new ProDao();
            subProjectDao = new SubProjectDAO();
            storageDAO = new StorageDAO();
        }

        // GET: Distribute
        public ActionResult Distribute()
        {
            return View();
        }


        public ActionResult GetDevice([DataSourceRequest] DataSourceRequest request)
        {
            var devices = storageDAO.GetDevices();

            // Lọc danh sách thiết bị chỉ theo trạng thái "available"
            devices = devices.Where(d => d.Status == "available").ToList();

            return Json(devices.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        // Action để lấy danh sách Subproject dựa trên Project được chọn
        public ActionResult PopulateSubprojectListByProjectId(int projectId)
        {
            var subprojects = subProjectDao.GetSubProjectsBYProID(projectId);

            var subprojectList = subprojects.Select(s => new
            {
                SubProjectID = s.SubProjectID,
                Name = s.Name
            }).ToList();

            return Json(subprojectList, JsonRequestBehavior.AllowGet);
        }


        // Action để lấy danh sách tất cả các Subproject
        public ActionResult PopulateSubprojectList()
        {
            var subprojects = subProjectDao.GetAllSubProject();

            var subprojectList = subprojects.Select(s => new
            {
                SubProjectID = s.SubProjectID,
                Name = s.Name
            }).ToList();

            return Json(subprojectList, JsonRequestBehavior.AllowGet);
        }

        // Action để lấy danh sách tất cả các Project
        public ActionResult PopulateProjectList()
        {
            var projects = proDAO.GetProData();

            var projectList = projects.Select(p => new
            {
                id = p.id,
                namepj = p.namepj
            }).ToList();

            return Json(projectList, JsonRequestBehavior.AllowGet);
        }
    }
}
