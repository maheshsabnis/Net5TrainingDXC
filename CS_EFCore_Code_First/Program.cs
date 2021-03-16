using CS_EFCore_Code_First.Models;
using System;
using System.Collections.Generic;

namespace CS_EFCore_Code_First
{
	class Program
	{
		static void Main(string[] args)
		{

			DXCDbContext ctx = new DXCDbContext();

			//List<Order> orders = new List<Order>()
			//{
			//	new Order() {OrderItem="Laptop", Price=231000 },
			//	new Order() {OrderItem="Rouer", Price=10000 }
			//};

			//Customer cust = new Customer();
			//cust.CustomerName = "Mahesh";
			//cust.Orders = orders;

			//ctx.Customers.Add(cust);
			//ctx.Orders.AddRange(orders);


			Movies movie = new Movies()
			{
			  // Id = 9,
			   Name = "No Time For Die",
			   ReleaseYear = 2021,
			   BoxOfficeCollection  =90000,
			   Category = "Spy",
			   PlayDuration = 180
			};
			ctx.ProductionUnits.Add(movie);

			ctx.SaveChanges();
			Console.WriteLine("Data Added  Successfully...");
			Console.ReadLine();
		}
	}
}
