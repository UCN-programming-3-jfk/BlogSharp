namespace WebApiClient.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set;}
        public string BlogTitle { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}