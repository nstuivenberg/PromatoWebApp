using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PromatoWebApp.Controllers
{
    public class PromatoController : Controller
    {
        // GET: Promato
        public ActionResult Index()
        {
            return View();
        }

        //Get: Upload.cshtml
        public ActionResult Upload()
        {
            return View();
        }

        //
        // GET: HelloWorld/Welcome
        public string Welcome()
        {
            return "JE MOEDER!";
        }

        /// <summary>
        /// Hier staat de code om het geupload bestand te controleren, uploaden en verwerken.
        /// Daarna wordt de pagina doorgestuurd naar iets.
        /// </summary>
        /// <returns>Save.cshtml</returns>
        [HttpPost]
        public ActionResult Save(HttpPostedFileBase file)
        {
            ViewBag.Message = "";
            if (file != null)
            {

                if(file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                    ViewBag.Message = "Het XML bestand is succesvol geupload.";
                    HttpCookie promatoCookie = new HttpCookie("Promato");
                    promatoCookie.Values["xmlFile"] = fileName;
                    Response.Cookies.Add(promatoCookie);
                }
            }
            else
            {
                ViewBag.Message = "U dient een bestand te selecteren om up te loaden.";
            }

            return View();
            //Andere optie:
            // return RedirectToAction("Index");
        }

        public XDocument Watch()
        {
            XDocument xmlBestand;
            if(Request.Cookies["Promato"] != null)
            {
                HttpCookie aCookie = Request.Cookies["Promato"];
                if(aCookie.Values["xmlFile"] != null)
                {
                    string bestandsnaam = aCookie.Values["xmlFile"];
                    if (System.IO.File.Exists(Server.MapPath("~/App_Data/uploads/") + bestandsnaam))
                    {
                        xmlBestand = new XDocument(XDocument.Load(Server.MapPath("~/App_Data/uploads/") + bestandsnaam));
                        return xmlBestand;
                    }
                }
            }


            return null;
        }
    }
}