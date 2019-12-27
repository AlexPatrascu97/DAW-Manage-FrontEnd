using FrontTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FrontTest.HTTPHelpers
{
	public class MyHttpMethods: IMyHttpMethods
	{
		private HttpClient _client;
		public MyHttpMethods(HttpClient client)
		{
			_client = client;
			_client.BaseAddress = new Uri("https://localhost:44360/");
		}

		public async Task<List<Product>> GetProductAsync() {

			HttpResponseMessage responseMessage = await _client.GetAsync("api/product/getallproducts");

			var httpContent = await responseMessage.Content.ReadAsStringAsync();

			List<Product> myObject = JsonConvert.DeserializeObject<List<Product>>(httpContent);

			return myObject;
			
		}

		


	}
	
}
