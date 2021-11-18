namespace WebApi.DTOs
{

    /// <summary>
    /// The DTO equivalent of the Author model object.
    /// The DTOs are implemented to ensure that 
    /// the propagation of any data on the Author object
    /// is by design and not just because someone added a property
    /// to the Author model class.
    /// </summary>
    public class AuthorDto
    {
        public int Id { get; set;}
        public string BlogTitle { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}