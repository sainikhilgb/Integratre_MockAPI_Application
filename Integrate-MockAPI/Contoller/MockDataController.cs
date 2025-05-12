using Integrate_MockAPI.Helpers;
using Integrate_MockAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Integrate_MockAPI.Contoller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockDataController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;

        public MockDataController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        // GET method to fetch all objects
        //if name is provided, filters by name
        [HttpGet]
        public async Task<IActionResult> GetObjects([FromQuery] string? name, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // Fetch data from the mock API
            var client = httpClientFactory.CreateClient("MockApi");
            HttpResponseMessage response;

            try
            {
                // Send the GET request to the mock API
                response = await client.GetAsync("objects");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling mock API: {ex.Message}");
            }

            // Handle failed response from the mock API
            if (!response.IsSuccessStatusCode)
                return ApiResponseHandler.HandleApiError(response, "Failed to fetch data from mock API");


            List<MockData>? allObjects;
            try
            {
                allObjects = await response.Content.ReadFromJsonAsync<List<MockData>>();
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Invalid JSON from mock API: {ex.Message}");
            }

            if (allObjects == null)
                return StatusCode(500, "No data returned from mock API");

            if (!string.IsNullOrWhiteSpace(name))
            {
                allObjects = allObjects
                    .Where(o => o.Name != null && o.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            // Apply pagination
            var totalItems = allObjects.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedObjects = allObjects
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Return the paginated data
            return Ok(new
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = pagedObjects
            });
        }

        // POST method to create a new object
        [HttpPost]
        public async Task<IActionResult> CreateObject([FromBody] MockDataCreate_UpdateRequest newObj)
        {
            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Send the POST request to the mock API
            var client = httpClientFactory.CreateClient("MockApi");
            // Prepare the payload
            var payload = new
            {
                name = newObj.Name,
                data = newObj.Data
            };

            HttpResponseMessage response;
            try
            {
                // Send the POST request to the mock API
                response = await client.PostAsJsonAsync("objects", payload);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling mock API: {ex.Message}");
            }
            
            // Handle failed response from the mock API
            if (!response.IsSuccessStatusCode)
                return ApiResponseHandler.HandleApiError(response, "Failed to create object");

            // Deserialize the response into the appropriate object
            var result = await response.Content.ReadFromJsonAsync<CreateResponse>();

            if (result == null)
                return StatusCode(500, "No data returned from mock API");

            // Return the created object
            return CreatedAtAction(nameof(GetObjects), new { id = result.Id }, result);
        }


        // PUT method to update an existing object
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateObject(string id, [FromBody] MockDataCreate_UpdateRequest updateRequest)
        {
            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            // Send the PUT request to the mock API
            var client = httpClientFactory.CreateClient("MockApi");

            // Prepare the payload
            var payload = new
            {
                name = updateRequest.Name,
                data = updateRequest.Data
            };

            HttpResponseMessage response;
            try
            {
               // Send the PUT request to the mock API
                response = await client.PutAsJsonAsync($"objects/{id}", payload);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling mock API: {ex.Message}");
            }

            // Handle failed response from the mock API
            if (!response.IsSuccessStatusCode)
                return ApiResponseHandler.HandleApiError(response, $"Failed to update object with ID '{id}'");

            // Deserialize the response from the mock API
            UpdateResponse? result;
            try
            {
                result = await response.Content.ReadFromJsonAsync<UpdateResponse>();
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Invalid JSON from mock API: {ex.Message}");
            }

            // Return the updated result as the response
            return Ok(result);
        }



        // DELETE method to remove an object by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObject(string id)
        {
            var client = httpClientFactory.CreateClient("MockApi");

            HttpResponseMessage response;
            try
            {
                response = await client.DeleteAsync($"objects/{id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling mock API: {ex.Message}");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound($"Object with ID '{id}' not found.");
            
            // Handle failed response from the mock API
            if (!response.IsSuccessStatusCode)
                return ApiResponseHandler.HandleApiError(response, $"Failed to delete object with ID '{id}'");

            return NoContent();
        }
    }
}
