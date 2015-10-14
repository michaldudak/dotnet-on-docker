using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace AspNet5OnDocker.Front.Controllers
{
	[Route("api/todos")]
	public class TodosController : Controller
	{
		private readonly string _apiUrl;
		private readonly HttpClient _httpClient;

		public TodosController()
		{
			_httpClient = new HttpClient();

			var serviceUrl = "http://backend:50001";
			_apiUrl = serviceUrl + "/todos";
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Get()
		{
			return await _httpClient.GetAsync(_apiUrl);
		}

		[HttpGet("{id}")]
		public async Task<HttpResponseMessage> Get(int id)
		{
			return await _httpClient.GetAsync(_apiUrl + "/" + id);
		}

		[HttpPost]
		public async Task<HttpResponseMessage> Post()
		{
			var content = new StreamContent(Request.Body);
			content.Headers.ContentType = MediaTypeHeaderValue.Parse(Request.Headers["Content-Type"]);
			var response = await _httpClient.PostAsync(_apiUrl, content);
			return response;
		}

		[HttpPut("{id}")]
		public async Task<HttpResponseMessage> Put(int id)
		{
			var content = new StreamContent(Request.Body);
			content.Headers.ContentType = MediaTypeHeaderValue.Parse(Request.Headers["Content-Type"]);
			return await _httpClient.PutAsync(_apiUrl + "/" + id, content);
		}

		[HttpDelete("{id}")]
		public async Task<HttpResponseMessage> Delete(int id)
		{
			return await _httpClient.DeleteAsync(_apiUrl + "/" + id);
		}
	}
}
