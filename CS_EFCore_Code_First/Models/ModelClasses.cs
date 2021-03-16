using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EFCore_Code_First.Models
{
	public class Category
	{
		[Key]
		public int CategoryRowId { get; set; }
		public string CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int BasePrice { get; set; }
		public ICollection<Product> Products { get; set; }
	}

	public class Product
	{
		[Key]
		public int ProductRowId { get; set; }
		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public string Manufacturer { get; set; }
		public int Price { get; set; }
		public int CategoryRowId { get; set; }
		public Category Category { get; set; }
	}



}
