using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models
{
    public class Platform
    {
        [Key] //No need for EF Core
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Publisher { get; set; }
        
        [Required]
        public string Cost { get; set; }

    }
}
