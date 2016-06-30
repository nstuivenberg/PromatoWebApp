using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PromatoWebApp.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload/Index.cshtml
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Save(FormCollection formCollection)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];

                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    System.Diagnostics.Debug.WriteLine(fileContentType);
                    System.Diagnostics.Debug.WriteLine(fileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);

                }
            }

            return View();
        }

    }


}