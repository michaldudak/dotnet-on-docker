using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetOnDocker.Model;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace AspNet5OnDocker.Back.Controllers
{
	[Route("[controller]")]
	public class ProjectsController : Controller
	{
		private readonly MyContext _ctx;

		public ProjectsController(MyContext ctx)
		{
			_ctx = ctx;
		}

		[HttpGet]
		public async Task<IEnumerable<Project>> Get()
		{
			return await _ctx.Projects.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<Project> Get(int id)
		{
			return await _ctx.Projects.FirstOrDefaultAsync(p => p.Id == id);
		}

		[HttpPost]
		public async Task Post([FromBody]Project project)
		{
			_ctx.Projects.Add(project);
			await _ctx.SaveChangesAsync();
		}

		[HttpPut("{id}")]
		public async Task Put(int id, Project project)
		{
			var oldProject = await Get(id);
			oldProject.Title = project.Title;
			oldProject.Url = project.Url;

			await _ctx.SaveChangesAsync();
		}

		[HttpDelete("{id}")]
		public async Task Delete(int id)
		{
			var toRemove = await Get(id);
			_ctx.Projects.Remove(toRemove);
		}
	}
}
