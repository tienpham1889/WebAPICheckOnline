using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_KTOnline.Models;

namespace WebAPI_KTOnline.Controllers
{
    public class MonHocController : ApiController
    {
        // GET: api/MonHoc
        public IEnumerable<MonHoc> Get()
        {
            List<MonHoc> listdanhsach = MonHoc.DsachMonHoc();
            return listdanhsach;
        }

        // GET: api/MonHoc/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MonHoc
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MonHoc/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MonHoc/5
        public void Delete(int id)
        {
        }
    }
}
