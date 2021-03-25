﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blaz_Wasm_App.Models
{
	public class Category
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
	}

	public class Categories : List<Category>
	{
		public Categories()
		{
			Add(new Category() { CategoryId=1,CategoryName="C1"});
			Add(new Category() { CategoryId = 2, CategoryName = "C2" });
		}
	}

	public class Product
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int CategoryId { get; set; }
	}

	public class Products : List<Product>
	{
		public Products()
		{
			Add(new Product() { ProductId=101, ProductName="P1", CategoryId=1});
			Add(new Product() { ProductId = 102, ProductName = "P2", CategoryId = 2 });
			Add(new Product() { ProductId = 103, ProductName = "P3", CategoryId = 1 });
			Add(new Product() { ProductId = 104, ProductName = "P4", CategoryId = 2 });
		}
	}
}
