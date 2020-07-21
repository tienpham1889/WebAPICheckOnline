using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using System.Data.SqlClient;
using WebAPI_KTOnline.Models;

namespace WebAPI_KTOnline.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public SinhVien Post([FromBody] SinhVien sv)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            String sQuery = string.Format("SELECT * FROM SinhVien where MaSV = '{0}' and Passsword = '{1}'", sv.maSinhVien, sv.matKhau);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            int count = 0;
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                count++;
            }
            SinhVien sv2 = new SinhVien();
            if (count > 0)
            {

                return sv.sv(sv.maSinhVien);
            }
            else
            {
                return sv2;
            }

        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
