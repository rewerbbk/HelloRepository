using EmptyUILayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyUILayer.Controllers
{
    public class NewCustomController : Controller
    {
        private string version;

        public NewCustomController()
        {
            version = ConfigurationManager.AppSettings["Version"];
        }

        // GET: NewCustom
        public ActionResult Index()
        {
            ViewBag.Title = "Custom Title";
            ViewBag.CField = "some text from the bag";
            ViewBag.version = version;
            return View("NewCustom");
        }

        public ActionResult Page2()
        {
            ViewBag.Title = "Custom Title 2";
            ViewBag.CField = "some text from the bag page 2";
            ViewBag.version = version;
            return View("NewCustom2");
        }

        [HttpPost]
        public ActionResult Page3(CustomModel modelIn)
        {
            ViewBag.Title = "Custom Title 3";
            ViewBag.CField = "some text from the bag page 3 with model";
            ViewBag.version = version;

            modelIn.addressLine1 += " modified";
            modelIn.numberField++;

            ViewBag.CustomModel = modelIn;

            return View("NewCustom3");
        }
    }
}