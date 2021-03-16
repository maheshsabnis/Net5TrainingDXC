using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EFCore_Code_First.Models
{
	public class DXCDbContext: DbContext
	{

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<ProductionUnit> ProductionUnits { get; set; }
		public DbSet<Movies> Movies { get; set; }
		public DbSet<WebSeries> WebSeries { get; set; }
		public DXCDbContext()
		{

		}

		/// <summary>
		/// Conemction MetaInfo
		/// UseSqlServer() an extension method for Discovering Sql Server and Execute Object Scripts
		/// </summary>
		/// <param name="optionsBuilder"></param>

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DXCDb;Integrated Security=SSPI");
			base.OnConfiguring(optionsBuilder);
		}

		/// <summary>
		/// Define the Expected Mapping between Model clases registered using DbSet<T>
		/// and the Database Tables. Generated Object Scripts to Create, Drop tables
		/// Modify Tables, etc
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// define one-to-many relationship across
			// category and product
			// FLuent-API for Defining Relations across types
			modelBuilder.Entity<Product>()
						.HasOne(p=>p.Category) // Product can have only one category
						.WithMany(c=>c.Products) // Onet-to-Many Relatiopship from Category to Product
						.HasForeignKey(p=>p.CategoryRowId); // the name of the ForeignKery Field

			// define many-to-many relationship
			// start reading relationship from the Customer class
			modelBuilder.Entity<Customer>()
						.HasMany(o => o.Orders) // navigation Customer to Order for Many-To-Many
						.WithMany(c => c.Customers) // navigation Order to Customer for Many-to-Many
						.UsingEntity(relation=>relation.ToTable("CustomersOrders")); // flexible mapping
																					 // by generating new Tabale along with Customers and Orders

			// create a seed data
			var movies = new Movies[]
			{
				 new Movies(){ Id=1, Name="Dr.No",ReleaseYear=1963,
					 Category="Spy",BoxOfficeCollection=12222,PlayDuration=150},
				 new Movies(){ Id=2, Name="Golmal",ReleaseYear=1976,
					 Category="Comedy",BoxOfficeCollection=122,PlayDuration=180}
			};
			var series = new WebSeries[]
			{
				new WebSeries(){ Id=3, Name="Ramayan",ReleaseYear=1986,Seasons=2,EpisodPerSeason=100},
				new WebSeries(){ Id=4, Name="House of Cards",ReleaseYear=2005,Seasons=6,EpisodPerSeason=50}
			};

			// define a union
			// Case Movies to ProductionUnit with all its data and then Union it 
			// with WebSeries. This will make sure that the Movies and WebSeries classes are
			// union together so FLuentyAPI will map all using the ProductUnit table
			var productionUnit = movies.Cast<ProductionUnit>()
					.Union(series)
					.ToList();

			// link the data to the model builder
			// model mapping will generate a Single table ProductUnit
			// // having a 'descriminator' column to sagrigate the row for each derive type
			modelBuilder.Entity<Movies>().HasData(movies);
			modelBuilder.Entity<WebSeries>().HasData(series);

			base.OnModelCreating(modelBuilder);
		}
	}
}
