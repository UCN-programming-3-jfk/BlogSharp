using System.ComponentModel.DataAnnotations;

namespace WebApiClient.DTOs
{

    /// <summary>
    /// This class defines the Author DTO with validation attributes for the GUI
    /// </summary>
    public class AuthorDto
    {
        public int Id { get; set;}
        [Required]

        public string BlogTitle { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}