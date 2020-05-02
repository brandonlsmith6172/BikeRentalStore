using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeStoreAPI2_WIP_.Models;
using BikeStoreAPI2_WIP_.Controllers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BikeRental.MVCUI.Controllers
{
    public class LocationsController : Controller
    {
        string baseurl = "https://localhost:44311/api/";

        public async Task<IActionResult> Index()
        {
            List<Location> locationList = new List<Location>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Locations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    locationList = JsonConvert.DeserializeObject<List<Location>>(apiResponse);
                }
            }
            return View(locationList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Location location)
        {
            TempData["Message"] = string.Empty;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{baseurl}Locations");
                //HTTP POST
                var postTask = httpClient.PostAsJsonAsync<Location>("Locations", location);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Location has been created.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Message"] = "Location has not been created.";
            return View(location);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Location location = new Location();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Locations/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    location = JsonConvert.DeserializeObject<Location>(apiResponse);
                }
            }
            return View(location);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Location location)
        {
            TempData["Message"] = string.Empty;
            if (location.LocationID > 0 || location != null)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{baseurl}Locations");
                    HttpResponseMessage res = await httpClient.PutAsJsonAsync($"Locations/{location.LocationID}", location);

                    if (res.IsSuccessStatusCode)
                    {
                        TempData["Message"] = $"Location has been saved.";
                        return RedirectToAction("Index");
                    }
                    TempData["Message"] = $"Location has not been saved.";
                    return View(location);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync($"Locations/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    Location location = JsonConvert.DeserializeObject<Location>(response);
                    return View(location);
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int id)
        {
            TempData["Message"] = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                HttpResponseMessage res = await client.DeleteAsync($"Locations/{id}");
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Location deleted.";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "Location not deleted.";
                return RedirectToAction("Index");
            }
        }
    }
}
