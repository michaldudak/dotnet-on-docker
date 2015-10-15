using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DotNetOnDocker.Model;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace AspNet5OnDocker.App.Controllers
{
	[Route("api/todos")]
	public class TodosController : Controller
	{
		private readonly MyContext _ctx;

		public TodosController(MyContext ctx)
		{
			_ctx = ctx;
		}

		[HttpGet]
		public async Task<IEnumerable<Todo>> Get()
		{
			return await _ctx.Todos.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<Todo> Get(int id)
		{
			return await _ctx.Todos.FirstOrDefaultAsync(p => p.Id == id);
		}

		[HttpPost]
		public async Task Post([FromBody]Todo todo)
		{
			_ctx.Todos.Add(todo);
			await _ctx.SaveChangesAsync();
		}

		[HttpPut("{id}")]
		public async Task Put(int id, [FromBody]Todo todo)
		{
			var oldTodo = await Get(id);
			oldTodo.Task = todo.Task;
			oldTodo.IsCompleted = todo.IsCompleted;

			await _ctx.SaveChangesAsync();
		}

		[HttpDelete("{id}")]
		public async Task Delete(int id)
		{
			var toRemove = await Get(id);
			_ctx.Todos.Remove(toRemove);

			await _ctx.SaveChangesAsync();
		}
	}
}
