using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_KTOnline.Models;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI_KTOnline.Controllers
{
    public class CauHoiController : ApiController
    {
        // GET: api/CauHoi
        public IEnumerable<CauHoi> Get()
        {
            List<CauHoi> listdanhsach = CauHoi.DsachCauHoi();
            return listdanhsach;
        }

        // GET: api/CauHoi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CauHoi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CauHoi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CauHoi/5
        public void Delete(int id)
        {
        }
    }
}
