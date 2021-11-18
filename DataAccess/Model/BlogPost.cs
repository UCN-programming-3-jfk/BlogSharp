using System;

namespace DataAccess.Model
{
    /// <summary>
    /// This class models the basic elements of a BlogPost
    /// and closely resembles a row in the BlogPost table in the database.
    /// </summary>
    public class BlogPost
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime PostCreationDate { get; set; }
    }
}