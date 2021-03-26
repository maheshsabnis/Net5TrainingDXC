# EntityFramework Core 5
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Relational
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.Data.SqlClient

# installing dotnet ef
dotnet tool install --global dotnet-ef

- Database First Approach
	-  MIgrations Projects where Database is Production-Ready only Application is migrated to newest technologies

>dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Company;Integrated Security=SSPI" 
 Microsoft.EntityFrameworkCore.SqlServer -o Models -t t1 t2 t3...





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


# Code-First 

- Generating Migrations
dotnet ef migrations add <NAME-OF-MIGRATION> -c <NAMESPACE.DBCONTEXTCLASS>

- APply Migrations to generate Database and Tables

dotnet ef database update -c <NAMESPACE.DBCONTEXTCLASS>



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



# Creating Custom Middleware

Custom Middleware is a method in a class having following rules
The class must be constructor injected with RequestDelegate  delegate
The class must have an Invoke() or InvokeAsync() method that accepts the HttpContext as input parameter
This method contains business logic of middleware and this method will be auto-invoked by the RequestDelegate
The RequestDelegate accept the input parameters as HttpContext, that why it calls the Invoke() or InvokeAsync() method.  
Create a class that contains an extension method for IApplicationBuilder 
The IApplicaitonBuilder contains Use () method to register the Custom Middleware, which is a  class that represents the Custom-Middleware class containing Invoke() or InvokeAsync() method and having RequestDelegate as constructor injected.
Filters Vs Middlewares
Filters are available only for MVC Controllers and APIControllers
Any global logic in ASP.NET Core to be written only for API and MVC Controllers during request processing then write it into filters.
If a global logic to be written for MVC and API Controllers and also for Razor Pages, then write such logic in Middlewares  




# Hands-on-Labs
1. CReate a base Type as Insurance with Following Attributes
	- Id, IssueDate, MaturityDate, PremiumAmount, CustomerName
2. Create a Derive Type from Insurance as follows
	- Health
		- AppliesTo (Info about Dieses)
		- CashLess
	- Auto
		- EngineNo
		- ChesisNo
		- RegisterationNo
Use a Table-Per-Hierarchy Featuture of EF Core 5 to Generate Table for Performing CRUD operations 

# Hands-On Lab API
1. Create a Search API that will accept Search Conditions to Search Records from the Database Tables with following scenarios

- Create Department and Employee Tables


CREATE TABLE [dbo].[Department] (
    [DeptNo]   INT           NOT NULL,
    [DeptName] VARCHAR (200) NOT NULL,
    [Location] VARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([DeptNo] ASC)
);

CREATE TABLE [dbo].[Employee] (
    [EmpNo]       INT           NOT NULL,
    [EmpName]     VARCHAR (200) NOT NULL,
    [Designation] VARCHAR (200) NOT NULL,
    [Salary]      INT           NOT NULL,
    [DeptNo]      INT           NULL,
    PRIMARY KEY CLUSTERED ([EmpNo] ASC),
    FOREIGN KEY ([DeptNo]) REFERENCES [dbo].[Department] ([DeptNo])
);


The API Method will accept Serach COnditions as below
 DeptName= %DeptName% AND Locaiton=%Location%
 DeptName= %DeptName% OR Locaiton=%Location%

 The method should return the Employees Details Along with their Department Details based on the Seacrh Condition


 2. Create a Custom Middleware that will be used to perform following Monitoring Operations on the WEB Method
	- It should log each request in the Database as
		- Requested Controller / Action method
	- It shoould handle the Exception and log the exception into the database (use Database -First Approach) as
		- ExceptionId (Auto Generate)
		- Error Message
		- Error Code (Set it in the code)
	

# Blazor  Application Development

- Identity using ASP.NET Core Identity can be by default configured in Blazor Serevr App but 
	not in Blazor Web Assermbly Apps


1. Blazor Server Application
	- ComponentEndpointConventionBuilder Class that is responsible to serialize the HTML DOM 
		for component back to Browser over the Socket
	- Microsoft.AspNetCore.Components
		- Namespace that contains all component Types for UI Creation, Security, Events and Databinding
		- Provision for the Dependency Injection for objects registered in the Startup class
		- Parser for directives(?)
			- @page, the component container
			- @inject, for dependency injection
			- @using, referring namespace
			- @typepara, for UI Template parameters
	- The 'name' of the compoent will be the 'file-name'.razor used to define then UI of the component
		- the component will be referred for UI rendering based on the filename
			- e.g. MyCOmponent.razor is the component file
				- <MyComponent>


# Programming with Blazor

Blazor Components
- It has UI
- Data Members
- Methods
- COmponents has
	- UI with Data Binding for Value / Editable elements
	- For Intractive UI elements e.g. Buttons
		- Event Binding

	- The '@' synbol aka directive symbol
		- The CSharp property / method execution
- Examples
	- @bind-Value
		- Performs Two-Way binding for UI elements
			- Detects the Default event of the UI element in 'Browser' and then updates the property of the Component  
			object
	- @onclick, the click event	





1. USing Server App
	- Using ASP.NET Core Features for App Development
	- Sharing Data Across Components
		- Pass the data using Route Parameters. This is actually the Route Template for Parameterized Routing
	- What if I have exisitng .NET Class Libraries? Can these be direcly used in the application?
		- Yes on Windows Machine (Proven and Tested)
2. Using WebAssembly App
	- Working with javascript
		- CSharp to JS
		- JS to CShrap
	- state management (Very Importat)
		- Plug-Ins
			- Local State
				- Browser's Storage 
					- Blazored.SessionStorage
``` cs
@inject Blazored.SessionStorage.ISessionStorageService sessionStorage;
```

				- register the Session Storage service in DI container of the applciation
In Program.cs Main method

``` cs
	// adding the session storage
			builder.Services.AddBlazoredSessionStorage();
			// adding the LocalStorage
			builder.Services.AddBlazoredLocalStorage();

```

					- Blazored.LocalStorage
			- Application State managemnt
		- Parent-Child Communication
			- Render the Child Component withing the Scope of the Parent Component
			- IMP POINTS:
				- The Parent Component must physically contain the Component
					- The Child Component is Updated when the Parent pass value to it
				- The Child COmponent MUST have a 'public property' decorated with '[Parameter]' for
					- Accepting Data from Parent
						- This will be a public property to accept and manipulate the data
					- Emitting Data to Parent
						- Use the 'EventCallback<T>' to bubbleup an event from child to parent
							- The class that is typed to 'T' where T is a payload aka event arguments that
								represent the data to be emitted from child to parent
							- When a child component emit an event, it must be subscribed by the
								parent component
					- The Child Component if not rendered independently then need not to have any Route-Template	
						i.e. the @page directive
				- When the COmponent is added into the Project, the file name we assign to the component file 
					will be commited into the project build and the same filename wille be used as then Component 
						(aka CustomComponent) into the application
						(NOTE: If you change the file name then the Visual Studio must be restarted)

	- HTTP Server Access using OpenAPI
		- WEB API to Blazor
	- Routing for Lazy Loading
		- External Razor Libs for Application Modularity
	- RenderFragment for Template
		- Re-Usability of UI for relief from complexity of UI
	- Secure Access to HTTP Services
		- User Based Security
			- Microsoft Identity Platform for AAD
		- Token Based Authentication
	- Unit Testing
		- COmponent Testing
	- Deployment
		- API App Deployment for Cloud
		- On-Premises


# Blazor Hands-on labs

1. Create a Component thst will be used to show the following output
	- The Component must have a Textbox where the end-use will enter the serch value to fetch records of Employees
		(Mandatory to be done today)
	- TestData
		- Search Value many be DeptName="IT"
			- based on this search value data has to be fetched
2. Remove the hard-coding of Department column name from the table that is showing List of Deparments 
	- (Mandatory to be done today)
3. Complete the Blazor Server App for Edit, Delete components. (Later or optional)
4. CReate a Table-Grid Component that will be used as child component with the following behavior
	- It will accept the 'DataSource' property as a ICollection<T> / IEnumerable<T> from its parent (Mandatory)
	- The componet will generate the Table-Grid based on Schema and Data of the received data using DataSource
		- Columns and Rows (Mandatory)
	- The COmponent will have 'CanDelete' property. If this property is set to 'true' by the parent component
		then each row will contains a Delete button. When this button is clicked, the record must be deleted
		by the Parent Component. (Child must emit event to parent with the data) (Mandatory)
	- Optionals exercise (but do it positively)
		- If the Columns Header is clicked then the data must be sorted ascending/sorted descending based on column value
		- The component should accept 'PageSize' as parameter to dipslay number of rows, but if the Total Size of
		 DataSource is greater than 'PageSize' then 'nextPage' and 'previousPage' button should be displayed fo pahinaiton
			- DataSource.Take<>() / Skip()

5. Create a Blazor WebAssembly SPA that will have following operation (Using Tokens)
	- A Register Screen that will be used to register the user
	- A Login Screen Thatb will be used to Login the user and provide access of the Application
	- Note: Unless the user Logs in, he will not able able to perform Followingm Operations
		- Create a Product Repository as
			- Product Name
			- Product Image
				- Upload an imahe using either HttpClient or Write A PAPI method that accept Binary Data
					- Use .NET 5 APIs
			- Product Description
			- Category Name	
			- CreatedBy
			- CreatedDate
	    - The Product can be updated only by the user who created the Product
		- The User can view only those products uploaded by him
		- Provide an Anonymous funcaitonality to search product based on 
			- ProductName
			- CategoryName
		- The searched product infromation will be displayed in the table along with Image per row 
		- Save token in Session Storage on Client
		- Create a ApplState Container object that will store product information for a login user so that
			this state object will be used by
				- ProductListComponent
				- EditProductComponent
					- No HTTP Request to fetch the Product Information for EDIT it must be taken from the state container
