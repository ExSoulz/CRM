using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBLib;
using DBLib.SQLite.Mappings;

namespace WebCRM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApplicationList()
        {

            using (var session = NhibernateHelper.OpenSession())
            {
                session.Flush();
                ViewBag.Title = "Чет дичь";
                Repository<CRMApplication> repository = new Repository<CRMApplication>(session);
                var list = repository.GetList();
                ViewBag.Applications = list;
            }
                return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}