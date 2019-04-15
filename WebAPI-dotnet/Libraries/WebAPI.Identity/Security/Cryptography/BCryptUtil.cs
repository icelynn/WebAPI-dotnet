using System;
using System.Diagnostics;
using BCrypt.Net;

namespace WebAPI.Identity.Security.Cryptography
{
    /// <summary>
    /// Password hash utility using BCrypt
    /// </summary>
    public class BCryptUtil
    {
        /// <value>生成token所使用的疊代參數，驗證時也會使用相同的time and space complexity。</value>
        private static readonly byte _cost = 13;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BCryptUtil()
        {

        }

        /// <summary>
        /// Hash password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password, _cost);

            return passwordHash;
        }

        /// <summary>
        /// Verify the input password.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool Verify(string input, string passwordHash)
        {
            bool output = BCrypt.Net.BCrypt.Verify(input, passwordHash);

            return output;
        }
    }
}
