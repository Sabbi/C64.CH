namespace C64.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);

        bool VerifyHashedPassword(string password, string hashedPassword);
    }
}