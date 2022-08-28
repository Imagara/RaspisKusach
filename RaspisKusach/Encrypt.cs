using System.Linq;
namespace RaspisKusach
{
    public class Encrypt
    {
        public static string GetHash(string password)
        {
            using (var hash = System.Security.Cryptography.SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
    }
}
