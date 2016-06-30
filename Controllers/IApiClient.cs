using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Http;

namespace PromatoWebApp.Controllers
{
    public class ApiOntvanger : Controller
    {
        public async Task<ActionResult> Index()
        {
            string apiUrl = "http://localhost:29639/Api/cirkel";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if(response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                }

            }

            return View();
        }
    }
}