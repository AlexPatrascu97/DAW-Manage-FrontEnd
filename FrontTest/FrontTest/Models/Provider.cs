using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontTest.Models
{
	public class Provider
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public int Payment { get; set; }
		public DateTime PayDay { get; set; }
		public bool Payed { get; set; }
	}
}
