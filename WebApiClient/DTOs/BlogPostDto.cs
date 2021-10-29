using System;

namespace WebApiClient.DTOs
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime PostCreationDate { get; set; }
    }
}