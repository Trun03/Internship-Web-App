//using System.Web.Mvc;
//using Model.Dao;
//using Model.EF;

//namespace Monitor.Controllers
//{
//    public class LogController : Controller
//    {
//        private DataContextDAO dataContextDAO = new DataContextDAO();

//        // GET: Log
//        public ActionResult Login()
//        {
//            return View();
//        }

//        // POST: Log
//        [HttpPost]
//        public ActionResult Login(string account, string password)
//        {
//            var user = dataContextDAO.GetAccount(account, password);
//            if (user != null)
//            {
//                string roleLower = user.Role.ToLower(); 

//                if (roleLower == "admin")
//                {
//                    return RedirectToAction("Index", "Admin");
//                }
//                else if (roleLower == "submanager")
//                {
//                    return RedirectToAction("Overview", "All");
//                }
//            }

//            ViewBag.ErrorMessage = "Invalid account or password";
//            return View();
//        }
//    }
//}
