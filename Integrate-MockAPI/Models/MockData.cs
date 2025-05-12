using System;
using System.ComponentModel.DataAnnotations;

namespace Integrate_MockAPI.Models;

public class MockData
{
    public string? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data is required")]
        public object Data { get; set; } = null!;

}
