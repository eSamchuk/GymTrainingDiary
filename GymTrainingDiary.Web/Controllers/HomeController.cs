using GymTrainingDiary.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymTrainingDiary.Web.Controllers
{
    public class HomeController : Controller
    {
        public IndexModel model;

        public HomeController()
        {
            this.model = new IndexModel();
        }



        public ActionResult Index()
        {
            model.Message = ConfigurationManager.AppSettings["message"];
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}