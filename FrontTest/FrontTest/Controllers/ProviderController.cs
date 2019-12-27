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
    public class ProviderController : Controller
    {
		ProductApi _api = new ProductApi();

		public async Task<IActionResult> Index()
		{
			List<Provider> provider = new List<Provider>();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync("api/provider/getallproviders");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				provider = JsonConvert.DeserializeObject<List<Provider>>(result);
			}
			return View(provider);

		}




		public async Task<IActionResult> Details(int Id)
		{
			var provider = new Provider();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync($"api/provider/GetProvider/{Id}");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				provider = JsonConvert.DeserializeObject<Provider>(result);
			}
			return View(provider);

		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Provider provider)
		{
			HttpClient client = _api.Initial();

			var postTask = client.PostAsJsonAsync<Provider>("api/provider/CreateProvider", provider);
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
			var provider = new Provider();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.DeleteAsync($"api/provider/DeleteProvider/{Id}");

			return RedirectToAction("Index");

		}

		public async Task<IActionResult> Edit(int Id)
		{
			var provider = new Provider();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync($"api/provider/GetProvider/{Id}");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				provider = JsonConvert.DeserializeObject<Provider>(result);
			}
			return View(provider);

		}


		[HttpPost]
		public async Task<IActionResult> Edit(Provider provider)
		{
			HttpClient client = _api.Initial();

			HttpResponseMessage response = await client.PutAsJsonAsync(
				$"api/provider/UpdateProvider/{provider.Id}", provider);
			response.EnsureSuccessStatusCode();

			provider = await response.Content.ReadAsAsync<Provider>();


			return RedirectToAction("Index");
		}




	}
}