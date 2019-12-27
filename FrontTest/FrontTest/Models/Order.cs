using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontTest.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int Quantity { get; set; }

	

		public int UserId { get; set; }
		public User User { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
