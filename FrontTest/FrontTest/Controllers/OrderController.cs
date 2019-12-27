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
    public class OrderController : Controller
    {
		ProductApi _api = new ProductApi();

		public async Task<IActionResult> Index()
		{
			List<Order> orders = new List<Order>();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync("api/order/getallorders");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				orders = JsonConvert.DeserializeObject<List<Order>>(result);
			}
			return View(orders);

		}
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Order order)
		{
			HttpClient client = _api.Initial();

			var postTask = client.PostAsJsonAsync<Order>("api/order/createOrder", order);
			postTask.Wait();

			var result = postTask.Result;
			if (result.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return View();

		}

		public async Task<IActionResult> Details(int Id)
		{
			var order = new Order();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync($"api/order/GetOrder/{Id}");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				order = JsonConvert.DeserializeObject<Order>(result);
			}
			return View(order);

		}

		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
		{
			var order = new Order();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.DeleteAsync($"api/order/DeleteOrder/{Id}");

			return RedirectToAction("Index");

		}

		public async Task<IActionResult> Edit(int Id)
		{
			var order = new Order();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync($"api/order/GetOrder/{Id}");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				order = JsonConvert.DeserializeObject<Order>(result);
			}
			return View(order);

		}


		[HttpPost]
		public async Task<IActionResult> Edit(Order order)
		{
			HttpClient client = _api.Initial();

			HttpResponseMessage response = await client.PutAsJsonAsync(
				$"api/order/UpdateOrder/{order.Id}", order);
			response.EnsureSuccessStatusCode();

			order = await response.Content.ReadAsAsync<Order>();


			return RedirectToAction("Index");
		}

	}
}