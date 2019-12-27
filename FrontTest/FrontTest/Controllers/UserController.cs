using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FrontTest.HTTPHelpers;
using FrontTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontTest.Controllers
{
    public class UserController : Controller
    {
		ProductApi _api = new ProductApi();

		public async Task<IActionResult> Index()
		{
			List<User> users = new List<User>();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync("api/user/getallusers");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				users = JsonConvert.DeserializeObject<List<User>>(result);
			}
			return View(users);

		}
		
		public async Task<IActionResult> Details(int Id)
		{
			var user = new User();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync($"api/user/GetUser/{Id}");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				user = JsonConvert.DeserializeObject<User>(result);
			}
			return View(user);

		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(User user)
		{
			HttpClient client = _api.Initial();

			var postTask = client.PostAsJsonAsync<User>("api/user/CreateUser", user);
			postTask.Wait();

			var result = postTask.Result;
			if (result.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return View();
		}

		public ActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public IActionResult SignUp(User user)
		{
			HttpClient client = _api.Initial();

			var postTask = client.PostAsJsonAsync<User>("api/user/CreateUser", user);
			if (user.Role != "Admin" )
			{
				user.Role = "Guest";
			}
			postTask.Wait();

			var result = postTask.Result;
			if (result.IsSuccessStatusCode)
			{
				return View("~/Views/Home/IndexHome.cshtml");
			}

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
		{
			var user = new User();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.DeleteAsync($"api/user/DeleteUser/{Id}");

			return RedirectToAction("Index");

		}

		public async Task<IActionResult> Edit(int Id)
		{
			var user = new User();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync($"api/user/GetUser/{Id}");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				user = JsonConvert.DeserializeObject<User>(result);
			}
			return View(user);

		}


		[HttpPost]
		public async Task<IActionResult> Edit(User user)
		{
			HttpClient client = _api.Initial();

			HttpResponseMessage response = await client.PutAsJsonAsync(
				$"api/user/UpdateUser/{user.Id}", user);
			response.EnsureSuccessStatusCode();

			user = await response.Content.ReadAsAsync<User>();


			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(User model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			List<User> users = new List<User>();
			HttpClient client = _api.Initial();
			HttpResponseMessage res = await client.GetAsync("api/user/getallusers");
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				users = JsonConvert.DeserializeObject<List<User>>(result);
			}
			foreach (var user in users)
			{
				if ((user.Username == model.Username) && (user.Password == model.Password))
				{
					HttpContext.Session.SetString("username", user.Username);
					HttpContext.Session.SetString("role", user.Role);
					HttpContext.Session.SetString("id", user.Id.ToString());
					return View("~/Views/Home/IndexHome.cshtml");

				}
			}
			
			

			return View(model);
		}

		public async Task<ActionResult> LogOut()
		{
			HttpContext.Session.Remove("username");
			HttpContext.Session.Remove("role");
			HttpContext.Session.Remove("id");
			return View("~/Views/Home/IndexHome.cshtml");

			
		}

	}

}

