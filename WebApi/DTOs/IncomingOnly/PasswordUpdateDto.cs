namespace WebApi.DTOs.IncomingOnly
{
    public class PasswordUpdateDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}