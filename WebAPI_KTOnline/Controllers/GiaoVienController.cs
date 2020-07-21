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
    public class GiaoVienController : ApiController
    {
        // GET: api/GiaoVien
        public IEnumerable<GiaoVien> Get()
        {
            List<GiaoVien> listdanhsach = GiaoVien.DsachGV();
            return listdanhsach;
        }

        // GET: api/GiaoVien/5
        public IEnumerable<GiaoVien> Get(string magv)
        {
            GiaoVien gv = new GiaoVien();
            gv = gv.gv(magv);
            yield return gv;
        }

        // POST: api/GiaoVien
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GiaoVien/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GiaoVien/5
        public void Delete(int id)
        {
        }
    }
}
