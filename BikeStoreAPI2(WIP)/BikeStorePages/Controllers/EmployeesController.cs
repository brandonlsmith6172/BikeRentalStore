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
    public class EmployeesController : Controller
    {
        string baseurl = "https://localhost:44311/api/";

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            List<Employee> employeeList = new List<Employee>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Employees"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employeeList = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);
                }
                foreach (var item in employeeList)
                {
                    using (var response = await httpClient.GetAsync($"{baseurl}Locations/{item.LocationID}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        item.Location = JsonConvert.DeserializeObject<Location>(apiResponse);

                    }
                }
            }
            return View(employeeList);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync($"Employees/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    Employee employee = JsonConvert.DeserializeObject<Employee>(response);
                    using (res = await client.GetAsync($"{baseurl}Locations/{employee.LocationID}"))
                    {
                        string apiResponse = await res.Content.ReadAsStringAsync();
                        employee.Location = JsonConvert.DeserializeObject<Location>(apiResponse);

                    }
                    return View(employee);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Employees/Create
        public async Task<IActionResult> CreateAsync()
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

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            TempData["Message"] = string.Empty;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{baseurl}Employees");
                //HTTP POST
                var postTask = httpClient.PostAsJsonAsync<Employee>("Employees", employee);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Employee has been created.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Message"] = "Employee has not been created.";
            return View(employee);
        }

        // GET: Employees/Edit/5
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
            Employee employee = new Employee();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Employees/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<Employee>(apiResponse);
                }
            }
            ViewData["LocationId"] = new SelectList(locationList, "Id", "City", employee.LocationID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,EmployeeId,LocationId")] Employee employee)
        {
            TempData["Message"] = string.Empty;
            if (employee.EmpID > 0 || employee != null)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{baseurl}Employees");
                    HttpResponseMessage res = await httpClient.PutAsJsonAsync($"Employees/{employee.EmpID}", employee);

                    if (res.IsSuccessStatusCode)
                    {
                        TempData["Message"] = $"Employee has been saved.";
                        return RedirectToAction("Index");
                    }
                    TempData["Message"] = $"Employee has not been saved.";
                    return View(employee);
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
            ViewData["LocationId"] = new SelectList(locationList, "Id", "City", employee.LocationID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["Message"] = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                HttpResponseMessage res = await client.DeleteAsync($"Employees/{id}");
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Employee deleted.";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "Employee not deleted.";
                return RedirectToAction("Index");
            }
        }
    }
}
