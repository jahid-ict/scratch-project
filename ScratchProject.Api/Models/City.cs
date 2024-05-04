using System.ComponentModel.DataAnnotations;

namespace ScratchProject.Api.Models
{
    public class City
    {
        [Key] 
        public Guid Id { get; set; }
        public string? CityName { get; set; }
    }
}
