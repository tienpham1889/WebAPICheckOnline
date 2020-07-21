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
    public class LopController : ApiController
    {
        // GET: api/Lop
        public IEnumerable<Lop> Get()
        {
            List<Lop> listdanhsach = Lop.DsachLop();
            return listdanhsach;
        }

        // GET: api/Lop/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Lop
        public Lop Post([FromBody]Lop lop)
        {
            Lop l = new Lop();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            if (l.kiemtra(lop.maLop))
            {
                String sQuery = "INSERT INTO [dbo].[Lop]([MaLop],[TenLop],[SoLuongSV],[TrangThai])VALUES(@malop,@tenlop,@soluong,@trangthai)";
                SqlCommand insertcommand = new SqlCommand(sQuery, conn);
                insertcommand.Parameters.AddWithValue("@malop", lop.maLop);
                insertcommand.Parameters.AddWithValue("@tenlop", lop.tenLop);
                insertcommand.Parameters.AddWithValue("@soluong", lop.soLuongSinhVien);
                insertcommand.Parameters.AddWithValue("@trangthai", lop.trangThai);
                int result = insertcommand.ExecuteNonQuery();
                conn.Close();
                l = l.lop(lop.maLop);
                if (result > 0)
                {
                    return l;
                }
            }
            else
            {
                return l;
            }
            return l;
        }

        // PUT: api/Lop/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Lop/5
        public void Delete(int id)
        {
        }
    }
}
