using System.Web.Mvc;
using DocumentFormat.OpenXml.Presentation;
using Kendo.Mvc.UI;
using Model.Dao;
using Model.DAO;
using Kendo.Mvc.Extensions;
using Model.EF;

namespace Monitor.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerDAO customerDAO;

        public CustomerController()
        {
            customerDAO = new CustomerDAO();
        }


        public ActionResult Customer()
        {
            return View();
        }

        public ActionResult GetCusData([DataSourceRequest] DataSourceRequest request)
        {
            var customers = customerDAO.GetCustomers();
            return Json(customers.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateCus(Customer customer)
        {
            customerDAO.AddCustomer(customer);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult UpdateCus(Customer customer)
        {
            customerDAO.UpdateCustomer(customer);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteCus(int id)
        {
            customerDAO.DeleteCustomer(id);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult HandleAction(string action, int id, string notification)
        {
            // Thực hiện xử lý tương ứng với action
            // Ví dụ: cập nhật trạng thái yêu cầu trong cơ sở dữ liệu

            return Json(new { success = true });
        }



        }
    }