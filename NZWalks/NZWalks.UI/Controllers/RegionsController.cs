using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
               
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7286/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());

                ViewBag.ResponseBody = response;

                
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionVeiwModel model)
        {
            // var client = httpClientFactory.CreateClient();

            // var httpRequestMessage = new HttpRequestMessage()
            // {
            //     Method = HttpMethod.Post,
            //     RequestUri = new Uri("https://localhost:7286/api/regions"),
            //     Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            // };

            //var httpResponseMsg = await client.SendAsync(httpRequestMessage);

            // httpResponseMsg.EnsureSuccessStatusCode();

            // var response = await httpRequestMessage.Content.ReadFromJsonAsync<AddRegionVeiwModel>();

            // if (response != null) 
            // {
            //     return RedirectToAction("Index", "Regions");
            // }

            // return View();

            try
            {
                var client = httpClientFactory.CreateClient();

                var httRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7286/api/regions"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };
                
                var jsonPayload = JsonSerializer.Serialize(model);
                
                var httpResponseMessage = await client.SendAsync(httRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

                if (response is not null)
                {
                    return RedirectToAction("Index", "Regions");
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
                return View();
            

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7286/api/regions/{id.ToString()}");

            if (response is not null) 
            { 
                View(response);
            }

            return View(null);
           
        }

        [HttpPost]
        public async Task<IActionResult>Edit(RegionDTO regionDTO)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7286/api/regions/{regionDTO.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(regionDTO), Encoding.UTF8, "application/json")
            };

            var jsonPayload = JsonSerializer.Serialize(regionDTO);

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if (response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult>Delete(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var httpResponseMessage = await client.DeleteAsync($"https://localhost:7286/api/regions/{id.ToString()}");

            httpResponseMessage.EnsureSuccessStatusCode();
                        
            return RedirectToAction("Index","Regions");
        }



    }
}
