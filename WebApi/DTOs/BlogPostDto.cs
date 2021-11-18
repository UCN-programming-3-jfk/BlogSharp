using System;

namespace WebApi.DTOs
{
    /// <summary>
    /// The DTO equivalent of the BlogPost model object.
    /// The DTOs are implemented to ensure that 
    /// the propagation of any data on the BlogPost object
    /// is by design and not just because someone added a property
    /// to the BlogPost model class.
    /// </summary>

    public class BlogPostDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime PostCreationDate { get; set; }
    }
}