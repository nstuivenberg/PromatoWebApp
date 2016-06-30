using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Xml.Linq;
using System.IO;
using System.Net.Http.Headers;
using PromatoWebApp.Models;
using System.Xml.Serialization;
using System.Xml;
using System;

namespace PromatoWebApp.Controllers
{
    public class CirkelController : Controller
    {
        // GET: Cirkel
        public ActionResult Index()
        {
            Level level = TreeViewList();
            return View(level);
            
        }

        public Level TreeViewList()
        {
            Bedrijf b = LeesXMLBestand();
            Level l = BedrijfNaarLevel(b);

            return l;
        }

        public Bedrijf LeesXMLBestand()
        {
            Bedrijf bedrijf = new Bedrijf();
            XDocument xmlBestand = new XDocument();
            if (Request.Cookies["Promato"] != null)
            {
                HttpCookie aCookie = Request.Cookies["Promato"];
                if (aCookie.Values["xmlFile"] != null)
                {
                    string bestandsnaam = aCookie.Values["xmlFile"];
                    if (System.IO.File.Exists(Server.MapPath("~/App_Data/uploads/") + bestandsnaam))
                    {
                        xmlBestand = new XDocument(XDocument.Load(Server.MapPath("~/App_Data/uploads/") + bestandsnaam));
                        System.Diagnostics.Debug.WriteLine(xmlBestand.ToString());
                    }
                }
            }

            if (xmlBestand != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Bedrijf));
                XmlReader reader = xmlBestand.CreateReader();
                reader.MoveToContent();
                try
                {
                    bedrijf = (Bedrijf)serializer.Deserialize(reader);
                }
                catch (InvalidOperationException invalid)
                {
                    System.Diagnostics.Debug.WriteLine(invalid);
                }
            }
            return bedrijf;
        }



        public async System.Threading.Tasks.Task<ActionResult> Cirkel()
        {

            string apiUrl = "http://ecab2b72-0ee0-4-231-b9ee.azurewebsites.net/api/Cirkel";


            //XML-Bestand van de webserver afhalen en omzetten naar XDocument
            XDocument xmlBestand = new XDocument();
            if (Request.Cookies["Promato"] != null)
            {
                HttpCookie aCookie = Request.Cookies["Promato"];
                if (aCookie.Values["xmlFile"] != null)
                {
                    string bestandsnaam = aCookie.Values["xmlFile"];
                    if (System.IO.File.Exists(Server.MapPath("~/App_Data/uploads/") + bestandsnaam))
                    {
                        xmlBestand = new XDocument(XDocument.Load(Server.MapPath("~/App_Data/uploads/") + bestandsnaam));
                        System.Diagnostics.Debug.WriteLine(xmlBestand.ToString());
                    }
                }
            }

            if (xmlBestand != null)
            {
                /*
                using (var client = new WebClient())
                {
                    //client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    //client.Headers.Add("Custom", xmlBestand.ToString());
                    //var response = client.UploadString(apiUrl, "test");
                    //ViewBag.lol = response.ToString();

                    byte[] postArray = Encoding.ASCII.GetBytes(xmlBestand.ToString());
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    var response = client.UploadData(apiUrl, postArray);

                    ViewBag.lol = response.ToString();
                }
                */
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {


                        MemoryStream ms = new MemoryStream();
                        xmlBestand.Save(ms);
                        byte[] bytes = ms.ToArray();



                        var fileContent = new ByteArrayContent(bytes);




                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "userxml.xml"
                        };
                        content.Add(fileContent);
                        // oud
                        //var result = client.PostAsync(apiUrl, content).Result;
                        //ViewBag.lol = result.ToString();
                        //nieuwer
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            //var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                            System.Diagnostics.Debug.WriteLine("DATA: "+data);
                            ViewBag.lol = data.ToString();

                        }
                    }
                }



                /*
                 * OUDE HALF WERKENDE SHIT
                 * using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiUrl);
                        var content = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("test", "test")
                        });
                        //WEGGEHAALD TIJDENS KEYVALUEPAIR
                        //client.DefaultRequestHeaders.Accept.Clear();
                        //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        //var httpContent = new StringContent(xmlBestand.ToString(), Encoding.UTF8, "application/xml");



                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            //var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                            ViewBag.lol = data.ToString();
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)");

                    string data = "kort testje";
                    byte[] bret = client.UploadData(apiUrl, "POST", System.Text.Encoding.ASCII.GetBytes(data));

                    ViewBag.lol = System.Text.Encoding.ASCII.GetString(bret);
                }

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var httpContent = new StringContent("testvalue", Encoding.UTF8, "application/xml");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        //var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                        ViewBag.lol = data.ToString();
                    }

                }
                */



            }
            Level level = TreeViewList();
            return View(level);
            //return View();

        }

        public Level BedrijfNaarLevel(Bedrijf b)
        {
            Level l = new Level();
            l.name = b.BedrijfsNaam;
            l.elementName = "Bedrijf";
            l.subLevels = new List<Level>();
            if (b.HasChildren())
            {
                foreach (Afdeling a in b.afdelingen)
                {
                    Level levelAfdeling = AfdelingNaarLevel(a);
                    l.subLevels.Add(levelAfdeling);
                }
            }



            return l;
        }

        private Level AfdelingNaarLevel(Afdeling a)
        {
            Level l = new Level();
            l.subLevels = new List<Level>();
            l.name = a.AfdelingsNaam;
            l.elementName = "Afdeling_" + a.AfdelingsID.ToString();
            if (a.HasChildren())
            {
                foreach (Project p in a.afdelingsProjecten)
                {
                    Level levelProject = ProjectNaarLevel(p);
                    l.subLevels.Add(levelProject);
                }
            }
            return l;
        }

        private Level ProjectNaarLevel(Project p)
        {
            Level l = new Level();
            l.name = p.ProjectNaam;
            l.elementName = "Project_" + p.ProjectID.ToString();
            l.startDatum = p.StartDatum;
            l.eindDatum = p.EindDatum;
            l.subLevels = new List<Level>();
            if (p.HasChildren())
            {
                foreach (Project subProject in p.subProjecten)
                {
                    Level subLevelProject = ProjectNaarLevel(subProject);
                    l.subLevels.Add(subLevelProject);
                }
            }

            return l;
        }

    }
}
