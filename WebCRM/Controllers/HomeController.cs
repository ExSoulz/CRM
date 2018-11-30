﻿using System;
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
                var servicepoints = new Repository<ServicePoint>(session).GetList();
                _operator.ServicePoint = new Repository<ServicePoint>(session).GetEntity(new Random(DateTime.Now.Millisecond).Next(0, servicepoints.Count - 1));
                employee.Save(_operator);
            }

            return View();
        }
        #endregion

        #region Сервисные центры
        public ActionResult ServicePoints()
        {
            ViewBag.Title = "Сотрудники";
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
            return View();
        }
        #endregion
    }
}