using Microsoft.AspNetCore.Http;
using PullPilot.Interface;
using System.Security.Cryptography;
using System.Text;


namespace PullPilot.Services
{
    public class HmacValidator : IHmacValidator
    {
        public bool IsSignatureValid(string payload, string signature, string secret)
        {
            var key = Encoding.UTF8.GetBytes(secret);
            var bodyBytes = Encoding.UTF8.GetBytes(payload);

            using var hmac = new HMACSHA256(key);
            var hash = "sha256=" + Convert.ToHexString(hmac.ComputeHash(bodyBytes)).ToLower();

            return hash == signature;
        }
    }
}
