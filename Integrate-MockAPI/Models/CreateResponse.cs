using System;

namespace Integrate_MockAPI.Models;

public class CreateResponse
{
     public string? Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Dictionary<string, object> Data { get; set; } = new();

        public DateTime? CreatedAt { get; set; }

}
