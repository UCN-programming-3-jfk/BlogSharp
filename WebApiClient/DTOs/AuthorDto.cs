using System.ComponentModel.DataAnnotations;

namespace WebApiClient.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set;}
        [Required]
        public string BlogTitle { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}