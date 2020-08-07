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
    public class CTLopHocPhanController : ApiController
    {
        // GET: api/CTLopHocPhan
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CTLopHocPhan/5
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet]
        [Route("api/chi-tiet-lop-hoc-phan")]
        public IEnumerable<SinhVien> Get_CTLHP(string malophocphan)
        {
            //danh sach chi tiet commit
            List<SinhVien> listdanhsach = SinhVien.DsachSV_LopHocPhan(malophocphan);
            return listdanhsach;
        }

        // POST: api/CTLopHocPhan
        [HttpPost]
        [Route("api/Them-sinh-vien-vao-lop-hoc-phan")]
        public CTLopHocPhan Post_ctlophocphan([FromUri]string malophocphan, string masinhvien)
        {
            CTLopHocPhan sv = new CTLopHocPhan();
            sv = CTLopHocPhan.AddCTLopHocPhan(malophocphan, masinhvien);
            return sv;
        }
        [HttpPost]
        [Route("api/Them-danh-sach-sinh-vien-vao-lop-hoc-phan")]
        public void Post([FromUri] string malop)
        {
            SqlConnection conn = DataProvider.Connect();
            string malophocphan = "";
            
            conn.Open();
            string sQuery1 = "sselect top 1 MaLopHP from LopHocPhan order by MaLopHP desc";
            SqlCommand com1 = new SqlCommand(sQuery1, conn);
            SqlDataReader dr1 = com1.ExecuteReader();
            while (dr1.Read())
            {
                malophocphan = dr1.GetString(0);
            }
            dr1.Close();
            conn.Close();
            CTLopHocPhan.AddDSachCTLopHocPhan(malophocphan, malop);
        }

        // PUT: api/CTLopHocPhan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CTLopHocPhan/5
        public void Delete(int id)
        {
        }
    }
}
