using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_KTOnline.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

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
        [HttpGet]
        [Route("api/Cauhoi-theomonhoc-chude")]
        public IEnumerable<CauHoi> Get_cauhoitheodieukien(string tenmonhoc, string tenchude)
        {
            string mamonhoc = "";
            string machude = "";
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("select  CD.MaMonHoc, CD.MaCD ");
            sQuery.Append("FROM ChuDe CD ");
            sQuery.Append("inner join MonHoc MH on CD.MaMonHoc = MH.MaMonHoc ");
            sQuery.AppendFormat("WHERE MH.TenMonHoc = N'{0}' AND CD.TenCD = N'{1}'", tenmonhoc,tenchude);
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                mamonhoc = dr.GetString(0);
                machude = dr.GetString(1);
            }
            conn.Close();
            List<CauHoi> Dsachcauhoi_monhoc_chude = CauHoi.DsachCauhoi_theodieukien(mamonhoc,machude);
            return Dsachcauhoi_monhoc_chude;
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
