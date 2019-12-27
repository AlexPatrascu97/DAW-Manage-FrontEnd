using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FrontTest.HTTPHelpers;
using FrontTest.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontTest.Controllers
{
    public class EmployeeController : Controller
    {
		ProductApi _api = new ProductApi();

		public async Task<IActionResult> Index()
		{
			List<Employee> products = new List<Employee>();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync("api/employee/getallemployees");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				products = JsonConvert.DeserializeObject<List<Employee>>(result);
			}
			return View(products);

		}



		public async Task<IActionResult> Details(int Id)
		{
			var employee = new Employee();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync($"api/employee/GetEmployee/{Id}");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				employee = JsonConvert.DeserializeObject<Employee>(result);
			}
			return View(employee);

		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Employee employee)
		{
			HttpClient client = _api.Initial();

			var postTask = client.PostAsJsonAsync<Employee>("api/employee/createemployee", employee);
			postTask.Wait();

			var result = postTask.Result;
			if (result.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
		{
			var employee = new Employee();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.DeleteAsync($"api/employee/DeleteEmployee/{Id}");

			return RedirectToAction("Index");

		}

		public async Task<IActionResult> Edit(int Id)
		{
			var employee = new Employee();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync($"api/employee/GetEmployee/{Id}");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				employee = JsonConvert.DeserializeObject<Employee>(result);
			}
			return View(employee);

		}


		[HttpPost]
		public async Task<IActionResult> Edit(Employee employee)
		{
			HttpClient client = _api.Initial();

			HttpResponseMessage response = await client.PutAsJsonAsync(
				$"api/employee/UpdateEmployee/{employee.Id}", employee);
			response.EnsureSuccessStatusCode();

			employee = await response.Content.ReadAsAsync<Employee>();


			return RedirectToAction("Index");
		}


	}
}