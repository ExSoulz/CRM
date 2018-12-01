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

        #region Заявки
        public ActionResult ApplicationList()
        {
            ViewBag.Title = "Заявки";
            using (var session = NhibernateHelper.OpenSession())
            {
                Repository<CRMApplication> repository = new Repository<CRMApplication>(session);
                var list = repository.GetList();
                ViewBag.Applications = list;
            }
            return View();
        }
        [HttpPost]
        public ActionResult ApplicationList(CRMApplication _application)
        {
            try
            {
                ViewBag.Title = "Заявки";
                using (var session = NhibernateHelper.OpenSession())
                {
                    var repository = new Repository<CRMApplication>(session);
                    var employee = new Repository<Operator>(session).GetEntity(1);

                    _application.Type = new Repository<TechType>(session).GetListByValue("Name", _application.TypeName)[0];
                    _application.Model = new Repository<Model>(session).GetListByValue("Name", _application.ModelName)[0];
                    _application.Brand = new Repository<Brand>(session).GetListByValue("Name", _application.BrandName)[0];
                    _application.Operator = employee;
                    _application.ServicePoint = employee.ServicePoint;
                    _application.AcceptingDate = DateTime.Now;
                    repository.Save(_application);
                }

                return ApplicationList();
            }
            catch
            {
                return WrongInput();
            }
        }
        #endregion

        #region Сотрудники
        public ActionResult Employees()
        {
            ViewBag.Title = "Сотрудники";
            using (var session = NhibernateHelper.OpenSession())
            {
                Repository<Operator> repository = new Repository<Operator>(session);
                var list = repository.GetList();
                ViewBag.Employees = list;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Employees(Operator _operator)
        {
            ViewBag.Title = "Сотрудники";
            using (var session = NhibernateHelper.OpenSession())
            {
                var employee = new Repository<Operator>(session);
                _operator.ServicePoint = new Repository<ServicePoint>(session).GetEntity(1);
                employee.Save(_operator);
            }

            return Employees();
        }
        #endregion

        #region Сервисные центры
        public ActionResult ServicePoints()
        {
            ViewBag.Title = "Сервисные центры";
            using (var session = NhibernateHelper.OpenSession())
            {
                Repository<ServicePoint> repository = new Repository<ServicePoint>(session);
                var list = repository.GetList();
                ViewBag.SPoints = list;
            }
            return View();
        }

        [HttpPost]
        public ActionResult ServicePoints(ServicePoint _servicePoint)
        {
            ViewBag.Message = "Сервисные центры";
            using (var session = NhibernateHelper.OpenSession())
            {
                var sp = new Repository<ServicePoint>(session);
                sp.Save(_servicePoint);
            }
            return ServicePoints();
        }
        #endregion

        public ActionResult WrongInput()
        {
            return View();
        }

    }
}