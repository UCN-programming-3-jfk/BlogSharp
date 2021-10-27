namespace DataAccess.Authentication
{
public static class BCryptTool
{
    private static string GetRandomSalt() => BCrypt.Net.BCrypt.GenerateSalt(12);

    public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());

    public static bool ValidatePassword(string password, string correctHash) => BCrypt.Net.BCrypt.Verify(password, correctHash);
}
}