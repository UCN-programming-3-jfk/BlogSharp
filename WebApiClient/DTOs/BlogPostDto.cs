using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiClient.DTOs
{
    /// <summary>
    /// This class defines the BlogPost DTO with the validation attributes for the GUI
    /// </summary>
    public class BlogPostDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }

        [Required]
        public string PostTitle { get; set; }

        [Required]
        public string PostContent { get; set; }

        public DateTime PostCreationDate { get; set; }
    }
}