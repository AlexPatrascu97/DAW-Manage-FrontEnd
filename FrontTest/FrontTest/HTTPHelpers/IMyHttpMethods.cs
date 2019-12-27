using FrontTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FrontTest.HTTPHelpers
{
	public interface IMyHttpMethods
	{
		Task<List<Product>> GetProductAsync();
	}
}
