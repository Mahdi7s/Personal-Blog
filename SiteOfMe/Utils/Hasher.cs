
namespace SiteOfMe.Utils
{
    internal static class Hasher
    {
        static int BCryptWorkFactor = 10;

        public static string HashOf(string strToHash)
        {
            return BCrypt.Net.BCrypt.HashPassword(strToHash, BCryptWorkFactor);
        }

        public static bool VerifyPass(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}