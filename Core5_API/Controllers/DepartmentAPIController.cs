using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core5_API.Models;
using Core5_API.Services;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

/// <summary>
/// Modify Action Methods by applying Custom OperationIds on it so that OpenAPI client can understand it
/// e.g. [HttpGet("/GetAll")]
/// Modify the return type so that they will be included into Contracts those are exposed to client Apps
/// COntracts must be CLR types so that they are exposed to the client Apps
/// On Conntroll class apply the Produces and Consumes attributes with Request/Response Message Formats 
/// </summary>


namespace Core5_API.Controllers
{



	//[Authorize]
	[Route("api/[controller]")]
	// USed to REad the Request Body for Post and Put requests
	// map the JSON data from Bpdy with CLR Object
	// ASP.NET Core 5 helps in building OpenAPI for 
	// creating the proxy stub
	[ApiController]
	[Produces(MediaTypeNames.Application.Json)]
	[Consumes(MediaTypeNames.Application.Json)]
	public class DepartmentAPIController : ControllerBase
	{
		private readonly IService<Department, int> deptServ;
		public DepartmentAPIController(IService<Department, int> serv)
		{
			deptServ = serv;
		}

		[HttpGet("/getall")]
		public async Task<IEnumerable<Department>> GetAsync()
		{
			var result = await deptServ.GetAsync();
			return result;
		}
		[HttpGet("/getone/{id}")]
		public async Task<Department> GetAsync(int id)
		{
			var result = await deptServ.GetAsync(id);
			return result;
		}
		[HttpPost("/createone")]
		public async Task<Department>PostAsync(Department dept)
		{
			if (ModelState.IsValid)
			{
				var result = await deptServ.CreateAsync(dept);
				return result; 
			}
			return null ;
		}
	}
}
