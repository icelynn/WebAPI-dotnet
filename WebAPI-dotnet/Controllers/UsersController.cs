using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Identity.Security.Cryptography;
using WebAPI.Models;
using WebAPI.Identity.Security;
using WebAPI_dotnet.Libraries.WebAPI.Models;

namespace WebServices.Controllers
{
    /// <summary>
    /// Controller for User authentication when loging in.
    /// </summary>
    public class UsersController : ApiController
    {
        private UserDbEntities db = new UserDbEntities();

        //// GET: api/Users
        //public IQueryable<User> GetUsers()
        //{
        //    return db.Users;
        //}

        //// GET: api/Users/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult GetUser(Guid id)
        //{
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}

        /// <summary>
        /// Login method in <C>[HttpPost]</C>
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        // [Route("Login")]
        // POST: api/Users/Login
        [HttpPost]
        public async Task<HttpResponseMessage> Login([FromBody] AccountInfo info)
        {
            using (UserDbEntities entities = new UserDbEntities())
            {
                // Search user by ID, and then vaerify the password.
                var entity = await entities.Users.FirstOrDefaultAsync(e => e.UserID == info.UserID);
                if (entity != null)
                {
                    BCryptUtil bCrypt = new BCryptUtil();
                    bool result = bCrypt.Verify(info.Password, entity.PasswordHash);

                    
                    if (result)
                    {
                        JwtAuthUtil jwt = new JwtAuthUtil(entity.GUID.ToString());
                        // token for portal login
                        string accessToken = jwt.GenerateToken();

                        // return necessary information to the front end
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new
                            {
                                guid = entity.GUID.ToString().ToUpper(),
                                lifeQual = entity.SalesLifeQual.ToUpper(),
                                propQual = entity.SalesPropQual.ToUpper(),
                                access_token = accessToken
                            });
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Wrong password!!");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No such user!");
                }
            }
        }

        /// <summary>
        /// Register method.
        /// </summary>
        /// <param name="disposing"></param>
        // POST: api/Users
        //[HttpPost]
        ////[Route("Register")]
        //public async Task<HttpResponseMessage> Register([FromBody] AccountInfo accountInfo)
        //{
        //    using (UserDbEntities entities = new UserDbEntities())
        //    {
        //        var entity = await entities.Users.FirstOrDefaultAsync(e => e.ID == accountInfo.account);
        //        if (entity == null)
        //        {
        //            BCryptUtil bCrypt = new BCryptUtil();
        //            string passwordHash = bCrypt.HashPassword(accountInfo.password);

        //            User user = new User()
        //            {
        //                GUID = Guid.NewGuid(),
        //                ID = accountInfo.account,
        //                PasswordHash = passwordHash
        //            };

        //            entities.Users.Add(user);
        //            await entities.SaveChangesAsync();

        //            var message = Request.CreateResponse(HttpStatusCode.Created, user);
        //            message.Headers.Location = new Uri(Request.RequestUri + user.GUID.ToString());
        //            return message;
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User already exists!");
        //        }
        //    }
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(Guid id)
        {
            return db.Users.Count(e => e.GUID == id) > 0;
        }
    }
}