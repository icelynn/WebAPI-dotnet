using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebAPI.Identity.Security
{
    /// <summary>
    /// Handle the token generation function
    /// </summary>
    public class JwtAuthUtil
    {
        private static readonly string _secret = "StuDIUOsGarBAnzO";
        private readonly Guid _guid = Guid.Empty;
        private readonly string _insurer = String.Empty;
        private static readonly RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        private static string _accessToken = String.Empty;
        private Dictionary<string, object> _claim;

        /// <summary>
        /// Constructor for the situation that guid are the only input argument.
        /// </summary>
        /// <remarks>WARNING: This method is only for test!</remarks>
        /// <param name="guid"></param>
        public JwtAuthUtil(string guid)
        {
            this._guid = new Guid(guid);
            this._insurer = String.Empty;
            GenerateToken();
        }

        /// <summary>
        /// Standard constructor for token generation.
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="insurer"></param>
        public JwtAuthUtil(string guid, string insurer)
        {
            this._guid = new Guid(guid);
            this._insurer = insurer;
            GenerateToken();
        }

        /// <summary>
        /// Start generating token using user guid and insurer id.
        /// </summary>
        /// <returns></returns>
        public string GenerateToken()
        {
            _claim = new Dictionary<string, object>();
            //Random rnd = new Random(Guid.NewGuid().GetHashCode());

            // Generating salt
            string salt = GenerateSalt();

            // Payload
            _claim.Add("UserGUID", _guid);
            _claim.Add("salt", salt);
            if (!String.Equals(_insurer, String.Empty))
            {
                _claim.Add("Insurer", _insurer);
            }
            _claim.Add("Issued at", DateTime.UtcNow);
            _claim.Add("Exp", DateTime.UtcNow.AddSeconds(Convert.ToInt32("90")));

            var payload = _claim;
            _accessToken = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(_secret), JwsAlgorithm.HS256);
            return _accessToken;
            
        }

        /// <summary>
        /// For token to mix up and randomize the order of the payload.
        /// </summary>
        /// <returns></returns>
        private string GenerateSalt()
        {
            byte[] randomNum = new byte[25];
            rngCsp.GetBytes(randomNum);
            string hexString = String.Empty;

            if (randomNum != null)
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < randomNum.Length; i++)
                {
                    builder.Append(randomNum[i].ToString("X2"));
                }
                hexString = builder.ToString();
            }

            return hexString;
        }
    }

}