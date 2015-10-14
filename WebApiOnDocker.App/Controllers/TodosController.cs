using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DotNetOnDocker.Model;
using Microsoft.Data.Entity;

namespace WebApiOnDocker.App.Controllers
{
	[Route("[controller]")]
	public class TodosController : ApiController
	{
		private readonly MyContext _ctx;

		public TodosController()
		{
			_ctx = new MyContext();
		}

		[HttpGet]
		public async Task<IEnumerable<Todo>> Get()
		{
			return await _ctx.Todos.ToListAsync();
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<Todo> Get(int id)
		{
			return await _ctx.Todos.FirstOrDefaultAsync(p => p.Id == id);
		}

		[HttpPost]
		public async Task<HttpResponseMessage> Post(Todo todo)
		{
			_ctx.Todos.Add(todo);
			await _ctx.SaveChangesAsync();
			return Request.CreateResponse(HttpStatusCode.Created);
		}

		[HttpPut]
		[Route("{id}")]
		public async Task Put(int id, Todo todo)
		{
			var oldTodo = await Get(id);
			oldTodo.Task = todo.Task;
			oldTodo.IsCompleted = todo.IsCompleted;

			await _ctx.SaveChangesAsync();
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task Delete(int id)
		{
			var toRemove = await Get(id);
			_ctx.Todos.Remove(toRemove);

			await _ctx.SaveChangesAsync();
		}
	}
}
