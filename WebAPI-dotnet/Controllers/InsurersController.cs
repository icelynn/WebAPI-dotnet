using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;
using WebAPI_dotnet.Libraries.WebAPI.Models;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 作為前端 Portal 在業務員的儀表板上生成按鈕時使用。
    /// </summary>
    public class InsurersController : ApiController
    {
        private InsurerDbEntities db = new InsurerDbEntities();

        /// <summary>
        /// 回傳SQL Server上已經記錄過的保險公司資料。
        /// </summary>
        /// <returns>Insurers IEnumerable</returns>
        // GET: api/Insurers
        [HttpGet]
        public IEnumerable<Insurer> Get()
        {
            using (InsurerDbEntities entities = new InsurerDbEntities())
            {
                return entities.Insurers.ToList();
            }
        }

        /// <summary>
        /// Dispose after a api request has been made.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InsurerExists(string id)
        {
            return db.Insurers.Count(e => e.InsurerID == id) > 0;
        }
    }
}