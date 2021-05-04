using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Crypto
{
    public sealed class CryptoGrath
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return !computedHash.Where((x, i) => x != passwordHash[i]).Any();
            }
        }

        public string ConvertFromByteIntoString(byte[] bytes)
        { 
            return Convert.ToBase64String(bytes);
        }

        public byte[] ConvertFromStringToByte(string s)
        { 
            return Convert.FromBase64String(s);
        }
    }
}