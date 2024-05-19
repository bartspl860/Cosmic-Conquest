using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Http
{
    public class JWTGenerator
    {
        public static JWTGenerator Builder()
        {
            return new JWTGenerator();
        }

        private string base64Secret;
        public JWTGenerator Secret(string secret)
        {
            this.base64Secret = secret;
            return this;
        }

        private UserScoreTokenPayload payload;
        public JWTGenerator Payload(UserScoreTokenPayload payload)
        {
            this.payload = payload;
            return this;
        }

        private int expirationUnixEpochTime;
        public JWTGenerator Expiration(DateTimeOffset dateTime)
        {
            this.expirationUnixEpochTime = (int)dateTime.ToUnixTimeSeconds();
            return this;
        }

        public string Generate()
        {
            this.payload.exp = this.expirationUnixEpochTime;
            string payloadJson = JsonUtility.ToJson(this.payload);
            
            return CreateJWT(payloadJson);
        }

        protected JWTGenerator() { }

        private string CreateJWT(string payloadJson)
        {
            string header = Base64UrlEncode("{\"alg\": \"HS256\", \"typ\": \"JWT\"}");
            string payloadEncoded = Base64UrlEncode(payloadJson);
            string signature = CreateSignature(header, payloadEncoded, base64Secret);

            return $"{header}.{payloadEncoded}.{signature}";
        }

        private string CreateSignature(string header, string payload, string base64Secret)
        {
            string data = $"{header}.{payload}";
            byte[] key = Convert.FromBase64String(base64Secret);
            using (var hmac = new HMACSHA256(key))
            {
                byte[] signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Base64UrlEncode(signatureBytes);
            }
        }

        private string Base64UrlEncode(string input)
        {
            var bytesToEncode = Encoding.UTF8.GetBytes(input);
            return Base64UrlEncode(bytesToEncode);
        }

        private string Base64UrlEncode(byte[] bytesToEncode)
        {
            string encoded = Convert.ToBase64String(bytesToEncode)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
            return encoded;
        }
    }
}