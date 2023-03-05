using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaCapitalMX.Controllers
{
    public class LosJemosController : Controller
    {

        public ActionResult Index()
        {

            return View("~/Views/Home/LosJemos.cshtml");
        }
    }
}