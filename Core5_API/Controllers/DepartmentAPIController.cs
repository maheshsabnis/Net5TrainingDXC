using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core5_API.Models;
using Core5_API.Services;
namespace Core5_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentAPIController : ControllerBase
	{
		private readonly IService<Department, int> deptServ;
		public DepartmentAPIController(IService<Department, int> serv)
		{
			deptServ = serv;
		}

		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{
			var result = await deptServ.GetAsync();
			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsync(int id)
		{
			var result = await deptServ.GetAsync(id);
			return Ok(result);
		}
	}
}
