using Microsoft.AspNetCore.Mvc;
using ApiBook.Models;
using Newtonsoft.Json;
using System.Text;

namespace ApiBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private new readonly string Url = "https://fakerestapi.azurewebsites.net/api/v1/Books";

        [HttpGet("GetBooks")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var result = string.Empty;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(Url);
                result = await response.Content.ReadAsStringAsync();
            }

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> GetBooksId(int id)
        {
            var result = string.Empty;

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri($"{Url}/{id}");

                var response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return BadRequest();

                result = await response.Content.ReadAsStringAsync();
            }

            return Ok(result);
        }

        [HttpPost("Books")]
        public async Task<ActionResult<List<Book>>> PostBook([FromBody] Book book)
        {
            var result = string.Empty;

            using (var httpClient = new HttpClient())
            {

                var json = JsonConvert.SerializeObject(book);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(Url, content);

                if (!response.IsSuccessStatusCode)
                    return BadRequest();

                result = await response.Content.ReadAsStringAsync();
            }

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<List<Book>>> PutBook(int id, [FromBody] Book book)
        {
            var result = string.Empty;

            using (var httpClient = new HttpClient())
            {
                var uri = $"{Url}/{id}";

                var json = JsonConvert.SerializeObject(book);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsJsonAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                    return BadRequest();

                result = await response.Content.ReadAsStringAsync();
            }

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(int id)
        {
            var result = string.Empty;

            using (var httpClient = new HttpClient())
            {
                var request = $"{Url}/{id}";

                var response = await httpClient.DeleteAsync(request);

                if (!response.IsSuccessStatusCode)
                    return BadRequest();

                result = await response.Content.ReadAsStringAsync();
            }

            return Ok(result);
        }
    }
}
