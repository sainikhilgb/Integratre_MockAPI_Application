using System;
using System.ComponentModel.DataAnnotations;

namespace Integrate_MockAPI.Models;

public class MockDataCreate_UpdateRequest
{
    [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Dictionary<string, object> Data { get; set; } = new();
}
