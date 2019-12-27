using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrontTest.Models;
using FrontTest.HTTPHelpers;
using System.Net.Http;
using Newtonsoft.Json;


namespace FrontTest.Controllers
{
	public class ProductsController : Controller
	{
		ProductApi _api = new ProductApi();

		public async Task<IActionResult> Index()
		{
			List<Product> products = new List<Product>();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync("api/product/getallproducts");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				products = JsonConvert.DeserializeObject<List<Product>>(result);
			}
			return View(products);

		}

		public async Task<IActionResult> Details(int Id)
		{
			var product = new Product();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync($"api/product/GetProduct/{Id}");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				product = JsonConvert.DeserializeObject<Product>(result);
			}
			return View(product);

		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Product product)
		{
			HttpClient client = _api.Initial();

			var postTask = client.PostAsJsonAsync<Product>("api/product/createProduct", product);
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
			var product = new Product();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.DeleteAsync($"api/product/DeleteProduct/{Id}");

			return RedirectToAction("Index");

		}

		public async Task<IActionResult> Edit(int Id)
		{
			var product = new Product();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync($"api/product/GetProduct/{Id}");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				product = JsonConvert.DeserializeObject<Product>(result);
			}
			return View(product);

		}


		[HttpPost]
		public async Task<IActionResult> Edit(Product product)
		{
			HttpClient client = _api.Initial();

			HttpResponseMessage response = await client.PutAsJsonAsync(
				$"api/product/UpdateProduct/{product.Id}", product);
			response.EnsureSuccessStatusCode();

			product = await response.Content.ReadAsAsync<Product>();

		
			return RedirectToAction("Index");
		}


		[HttpPost]
		public async Task<IActionResult> Add(int Id , int userid)
		{
			var order = new Order();
			order.ProductId = Id;
			order.UserId = userid;
			order.Quantity = 1;
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.PostAsJsonAsync<Order>("api/order/CreateOrder", order);

			return RedirectToAction("Index");

		}










	}
}
