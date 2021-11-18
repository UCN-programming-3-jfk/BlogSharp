namespace DataAccess.Model
{
    /// <summary>
    /// This class models the basic elements of the Author class
    /// The password field has been left out to enforce correct usage 
    /// of the AuthorRepositori methods for creating/updating password.
    /// </summary>
    public class Author
    {
        public int Id { get; set;}
        public string BlogTitle { get; set; }
        public string Email { get; set; }
    }
}