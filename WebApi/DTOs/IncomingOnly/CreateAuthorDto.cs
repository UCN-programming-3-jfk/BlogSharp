namespace WebApi.DTOs.IncomingOnly
{
    public class CreateAuthorDto : AuthorDto
    {
        public string Password { get; set; }
    }
}