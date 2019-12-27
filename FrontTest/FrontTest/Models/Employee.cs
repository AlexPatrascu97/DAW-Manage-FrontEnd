using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontTest.Models
{
	public class Employee
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime StartingDay { get; set; }
		public DateTime? LastDay { get; set; }
		public string Post { get; set; }
		public int Salary { get; set; }
	}
}
