using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DotNetOnDocker.Model;
using Microsoft.Data.Entity;

namespace WebApiOnDocker.Front.Controllers
{
	[Route("[controller]")]
	public class ProjectsController : ApiController
	{
		private readonly MyContext _ctx;

		public ProjectsController()
		{
			_ctx = new MyContext();
		}

		[HttpGet]
		public async Task<IEnumerable<Project>> Get()
		{
			return await _ctx.Projects.ToListAsync();
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<Project> Get(int id)
		{
			return await _ctx.Projects.FirstOrDefaultAsync(p => p.Id == id);
		}

		[HttpPost]
		public async Task<HttpResponseMessage> Post(Project project)
		{
			_ctx.Projects.Add(project);
			await _ctx.SaveChangesAsync();
			return Request.CreateResponse(HttpStatusCode.Created);
		}

		[HttpPut]
		[Route("{id}")]
		public async Task Put(int id, Project project)
		{
			var oldProject = await Get(id);
			oldProject.Title = project.Title;
			oldProject.Url = project.Url;

			await _ctx.SaveChangesAsync();
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task Delete(int id)
		{
			var toRemove = await Get(id);
			_ctx.Projects.Remove(toRemove);
		}
	}
}
