using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Identity.Security;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Token controller only for token generation.
    /// </summary>
    /// <remarks>TODO: 將輸入進的保險公司代碼標準化以避免錯誤。</remarks>
    public class TokenController : ApiController
    {
        /// <summary>
        /// This post method is used for sales to process the insurance application affair.
        /// </summary>
        /// <remarks>This method is based on Jose.JWT token generation.</remarks>
        /// <example>
        ///    e.g. RequestBody:  [application/json]
        ///    <code>
        ///         {
        ///             "UserGUID": "AAAA0000-3AEA-0000-AAAA-0000AAAAAAAA",
        ///             "Insurer": "AAA"
        ///         }
        /// </code>
        /// </example>
        /// <param name="info"></param>
        /// <returns>The token generated based on the userGUID and inusrerID</returns>
        // POST api/token
        [HttpPost]
        public HttpResponseMessage GenerateToken([FromBody] TokenInfo info)
        {
            if (info == null ||  String.IsNullOrEmpty(info.UserGUID) || String.IsNullOrEmpty(info.Insurer))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "All Fields are required.");

            try
            {
                // Send UserGUID and insurerCode to generate the token
                JwtAuthUtil jwt = new JwtAuthUtil(info.UserGUID, info.Insurer);
                string access_token = jwt.GenerateToken();

                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        access_token = access_token
                    });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// WARNING: Only for test!
        //// GET api/token
        //[HttpGet]
        //public HttpResponseMessage OneTokenGen()
        //{
        //    try
        //    {
        //        JwtAuthUtil jwt = new JwtAuthUtil(new Guid().ToString(), "KKK");
        //        string access_token = jwt.GenerateToken();

        //        return Request.CreateResponse(HttpStatusCode.OK,
        //            new
        //            {
        //                access_token = access_token
        //            });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}
    }
}
