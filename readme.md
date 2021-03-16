# EntityFramework Core 5
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Relational
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.Data.SqlClient




- Database First Approach
	-  MIgrations Projects where Database is Production-Ready only Application is migrated to newest technologies

>dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Company;Integrated Security=SSPI" Microsoft.EntityFrameworkCore.SqlServer -o Models





- Code-First Approach
	- App from scratch
	- Define classes and establish relationships across them and then generate Database Schemas

	- Generate Migrations
		- Using Scaffolder to generate Object Scripts based on Model classes known by DbContext
		- Command to Migration generation
dotnet ef migrations add firstMigration -c CS_EFCore_Code_First.Models.DXCDbContext

		- Command to Generate Database / Update Database base after migrations
dotnet ef database update -c CS_EFCore_Code_First.Models.DXCDbContext

Pseduo Coding For EFCore

Assumptions, ctx is an instance of DbContext and DbSet<Emp> is emps

-  To find all records
var res = ctx.emps.ToList(); // sync call

// Use Microsoft.EntityFrameworkCore for ToListAsync()
var res = await cts.emps.ToListAsync(); // async call

- TO Add a new Record

1. create a new Emp instance and set its values
var emp =enw Emp();
2. add this instance in DbSet<Emp>
var entity = ctx.emps.Add(emp); 
-- commit transactions
ctx.SaveChanges();
OR for Async

var res = await ctx.emps.AddAsync(emps);

-- res is a 'Task' object for the Type Entity Task<Entity>
-- read the newly created entity

ver entity = res.Entity; // return newly created record
ctx.SaveChangesAsync();

-- To Read Record based on Primary Key

var res = ctx.emps.Find(id);  // where 'id' is Primary Key value

OR using Async

var res = await ctx.emps.FindAsync(id);

-- TO Delete Record
1. Search record by Id and then pass the result to Remove method
ctx.emps.Remove(record); // no async delete is present
ctx.SaveChages(); OR cts.SaveChangesAsync();

-- To Update Record
1. Search reord base on id
2. call Update<T>()method
ctx.Update<Emp>(record);
ctx.SaveChages(); OR cts.SaveChangesAsync();






Features
- Flexible Entity Mapping
	- Private Property Mapping with DbTable those are having public Read / Write
- One-To-Many (EF Core 2.x to current release)
	- Primary Key and Foreign Key using Fluent APIs
		- Write the code for relationships
- Many-To-Many Relationship (EF Core 5)
	- Fluent APIs with HasMany<T> method
- Table-per-Type (TPT) mapping
	- Abstract Base Entity having various dericed class for Domain Model Creations
	- Inform the FluentAPI to create a Union of the derived classes for which DbSet<T> is define 
		and cast this untion to the parent class
		- this will generate a single table maped with the Abstratc base class and will create a union 
			of all properties of derive classes into a single table as columns		
		- This creates a 'discrinator' column that will identity the current row belong to which derivation
		

- Split Queries
	- Improvise the 'Include()' operator
		- Upto EF Core 3.x, the Include() was compilted into JOIN Queries (LEFT JOIN)
		-  With EF Core 5, the the Include() operator generates the 
			seperate SQL Statement on  Database with INNER Join to fetch only applied database
	- The Include() operator can use the filter expression to filter data from table based on expression 	


DbConbtext
	- Class used to map Entites to Tables
		- Uses DbSet<T> for Mapping Class of Name T with Table of name T (with optional plurization)
	- Manage Transactions
	- Manage the relationships across tables using Entity Relationships

- ASP.NET Core Web App DI Improvements w.r.t. EF Core 5, i.e. DbConetxtFactory


