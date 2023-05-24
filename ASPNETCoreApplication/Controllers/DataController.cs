using ASPNETCoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace ASPNETCoreApplication.Controllers
{
    public class DataController : Controller
    {

        Uri baseAddress = new Uri(" https://localhost:44387/api");
        private readonly HttpClient _client;
        public DataController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public HttpClient Get_client()
        {
            return _client;
        }

        [HttpGet]

        public IActionResult Index(HttpClient _client)
        {
            List<DataViewModel> productList= new List<DataViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/data/Get").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data=respone.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<DataViewModel>>(data);
            }

            return View(productList);
        }
    }
}
    