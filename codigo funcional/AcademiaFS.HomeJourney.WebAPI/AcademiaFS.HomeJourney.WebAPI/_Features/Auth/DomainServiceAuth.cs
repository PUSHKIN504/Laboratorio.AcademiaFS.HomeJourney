using System.Security.Cryptography;
using System.Text;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Auth
{
    public class DomainServiceAuth
    {

        public bool ValidatePassword(string inputPassword, byte[] storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(inputPassword);
                var inputHash = sha256.ComputeHash(inputBytes);
                return inputHash.SequenceEqual(storedHash);
            }
        }
    }
}
