﻿using System.Web.Mvc;

namespace AbcSample.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}