using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using HtmlToOpenXml.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private DocumentConstructorContext db = new DocumentConstructorContext();
        
        public ActionResult Index()
        {
            var headers = db.DocumentConstructorLeftDatas.OrderBy(i => i.Npp).ToList();
            return View(headers);
        }
        public ActionResult Edit()
        {
            return View(db.DocumentConstructorLeftDatas.ToList().OrderBy(i => i.Npp));
        }     
    }
}


