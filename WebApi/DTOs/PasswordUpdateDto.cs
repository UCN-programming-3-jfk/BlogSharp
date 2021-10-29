namespace WebApi.DTOs
{
    public class PasswordUpdateDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}