using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeStoreAPI2_WIP_.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BikeRental.MVCUI.Controllers
{
    public class BikesController : Controller
    {
        string baseurl = "https://localhost:44311/api/";

        // GET: Bikes
        public async Task<IActionResult> Index()
        {
            List<Bikes> bicycleList = new List<Bikes>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Bikes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bicycleList = JsonConvert.DeserializeObject<List<Bikes>>(apiResponse);
                }
                foreach (var item in bicycleList)
                {
                    using (var response = await httpClient.GetAsync($"{baseurl}Locations/{item.LocationID}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        item.Location = JsonConvert.DeserializeObject<Location>(apiResponse);

                    }
                }
            }
            return View(bicycleList);
        }

        // GET: Bicycles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync($"Bicycles/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    Bikes bike = JsonConvert.DeserializeObject<Bikes>(response);
                    using (res = await client.GetAsync($"{baseurl}Locations/{bike.LocationID}"))
                    {
                        string apiResponse = await res.Content.ReadAsStringAsync();
                        bike.Location = JsonConvert.DeserializeObject<Location>(apiResponse);

                    }
                    return View(bike);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Bicycles/Create
        public async Task<IActionResult> Create()
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
            ViewData["LocationId"] = new SelectList(locationList, "Id", "City");
            return View();
        }

        // POST: Bicycles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Type,Size,Gender,Brand,LocationId")] Bikes bikes)
        {
            TempData["Message"] = string.Empty;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{baseurl}Bikes");
                //HTTP POST
                var postTask = httpClient.PostAsJsonAsync<Bikes>("Bikes", bikes);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Bikes has been created.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Message"] = "Bikes has not been created.";
            return View(bikes);
        }

        // GET: Bicycles/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            Bikes bicycle = new Bikes();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Bicycles/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bicycle = JsonConvert.DeserializeObject<Bikes>(apiResponse);
                }
            }
            ViewData["LocationId"] = new SelectList(locationList, "Id", "City", bicycle.LocationID);
            return View(bicycle);
        }

        // POST: Bicycles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Size,Gender,Brand,LocationId")] Bikes bikes)
        {
            TempData["Message"] = string.Empty;
            if (bikes.BikeID > 0 || bikes != null)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{baseurl}Bicycles");
                    HttpResponseMessage res = await httpClient.PutAsJsonAsync($"Bicycles/{bikes.BikeID}", bikes);

                    if (res.IsSuccessStatusCode)
                    {
                        TempData["Message"] = $"Bicycle has been saved.";
                        return RedirectToAction("Index");
                    }
                    TempData["Message"] = $"Bicycle has not been saved.";
                    return View(bikes);
                }
            }
            List<Location> locationList = new List<Location>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Locations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    locationList = JsonConvert.DeserializeObject<List<Location>>(apiResponse);
                }
            }
            ViewData["LocationId"] = new SelectList(locationList, "Id", "City", bikes.LocationID);
            return View(bikes);
        }

        // GET: Bicycles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["Message"] = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                HttpResponseMessage res = await client.DeleteAsync($"Bikes/{id}");
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Bike deleted.";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "Bike not deleted.";
                return RedirectToAction("Index");
            }
        }



    }
}
