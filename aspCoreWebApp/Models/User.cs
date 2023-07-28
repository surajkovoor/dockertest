using System.ComponentModel.DataAnnotations;

namespace aspCoreWebApp.Models
{
    public class User
    {
        public int PersonId { get; set; }
        [Required]
        public string? name { get; set; }
        [Required]
        public string? email { get; set; }
        [Required]
        public string? contact { get; set; }
        [Required]
        public string? password { get; set; }
    }
}
