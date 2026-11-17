using System;
using System.Security.Cryptography;
using System.Text;

namespace Game1
{
    public class ClientVerifier
    {
        private static readonly byte[] SECRET_KEY = Encoding.UTF8.GetBytes("NRO_SECRET_KEY_2024_CHANGE_ME");

        public static byte[] CreateToken()
        {
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            byte[] token = new byte[40];

            // 1. Ghi 8 byte timestamp (Big-Endian)
            for (int i = 0; i < 8; i++)
            {
                token[i] = (byte)((timestamp >> (56 - i * 8)) & 0xFF);
            }

            // 2. Tính HMAC-SHA256 của 8 byte timestamp đó
            byte[] tsBytes = new byte[8];
            Array.Copy(token, 0, tsBytes, 0, 8);

            using (HMACSHA256 hmac = new HMACSHA256(SECRET_KEY))
            {
                byte[] hash = hmac.ComputeHash(tsBytes);
                // 3. Copy 32 byte hash vào sau 8 byte timestamp
                Array.Copy(hash, 0, token, 8, 32);
            }

            return token;
        }
    }
}
