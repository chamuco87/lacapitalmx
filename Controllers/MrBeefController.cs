using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaCapitalMX.Controllers
{
    public class MrBeefController : Controller
    {
        public ActionResult Index()
        {

            return View("~/Views/Home/MrBeef.cshtml");
        }
    }
}