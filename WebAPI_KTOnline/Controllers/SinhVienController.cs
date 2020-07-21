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
    public class SinhVienController : ApiController
    {
        // GET: api/SinhVien
        public IEnumerable<SinhVien> Get()
        {
            List<SinhVien> listdanhsach = SinhVien.DsachSV();
            return listdanhsach;
        }

        // GET: api/SinhVien/5
        public IEnumerable<SinhVien> Get(string masv)
        {
            SinhVien sv = new SinhVien();
            sv = sv.sv(masv);
            yield return sv;
        }

        // POST: api/SinhVien
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SinhVien/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SinhVien/5
        public void Delete(int id)
        {
        }
    }
}
