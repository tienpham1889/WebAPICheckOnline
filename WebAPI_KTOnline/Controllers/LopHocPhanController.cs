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
    public class LopHocPhanController : ApiController
    {
        // GET: api/LopHocPhan
        public IEnumerable<LopHocPhan> Get()
        {
            List<LopHocPhan> listdanhsach = LopHocPhan.DsachLop();
            return listdanhsach;
        }

        // GET: api/LopHocPhan/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LopHocPhan
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LopHocPhan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LopHocPhan/5
        public void Delete(int id)
        {
        }
    }
}
