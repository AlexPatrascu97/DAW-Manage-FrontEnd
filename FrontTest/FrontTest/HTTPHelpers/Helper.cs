using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontTest.HTTPHelpers
{
	public class ProductApi
	{
		public HttpClient Initial()
		{
			var client = new HttpClient();
			client.BaseAddress = new Uri("https://localhost:44360/");
			return client;

		}
	}
}
