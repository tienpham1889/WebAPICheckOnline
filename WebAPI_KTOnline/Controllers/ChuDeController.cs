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
    public class ChuDeController : ApiController
    {
        // GET: api/ChuDe
        public IEnumerable<ChuDe> Get()
        {
            List<ChuDe> listdanhsach = ChuDe.DsachChuDe();
            return listdanhsach;
        }

        // GET: api/ChuDe/5
        public IEnumerable<ChuDe> Get(string machude)
        {

            ChuDe cd = new ChuDe();
            cd = cd.cd(machude);
            yield return cd;
        }
        [HttpGet]
        [Route("api/chude-theomonhoc")]
        public IEnumerable<ChuDe> Get_chude(string tenmonhoc)
        {
            string mamh ="";
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("select DISTINCT  CD.MaMonHoc ");
            sQuery.Append("FROM ChuDe CD ");
            sQuery.Append("inner join MonHoc MH on CD.MaMonHoc = MH.MaMonHoc ");
            sQuery.AppendFormat("WHERE MH.TenMonHoc = N'{0}'",tenmonhoc);
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                mamh = dr.GetString(0);
            }
            conn.Close();
            List<ChuDe> Dsachtheomonhoc = ChuDe.DsachCD_theomonhoc(mamh);
            return Dsachtheomonhoc;
            
        }
        // POST: api/ChuDe
        public ChuDe Post([FromBody]ChuDe chude)
        {
            ChuDe cd = new ChuDe();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            if (cd.kiemtra(chude.maChuDe))
            {
                String sQuery = "INSERT INTO [dbo].[ChuDe]([MaCD],[TenCD],[MaMonHoc],[TrangThai],[MaGV])VALUES(@macd,@tencd,@mamonhoc,@trangthai,@magv)";
                SqlCommand insertcommand = new SqlCommand(sQuery, conn);
                insertcommand.Parameters.AddWithValue("@macd", chude.maChuDe);
                insertcommand.Parameters.AddWithValue("@tencd",chude.tenChuDe);
                insertcommand.Parameters.AddWithValue("@mamonhoc", chude.maMonHoc);
                insertcommand.Parameters.AddWithValue("@trangthai", chude.trangThai);
                insertcommand.Parameters.AddWithValue("@magv", chude.maGiaoVien);
                int result = insertcommand.ExecuteNonQuery();
                conn.Close();
                cd = cd.cd(chude.maChuDe);
                if (result > 0)
                {
                    return cd;
                }
            }
            else
            {
                return cd;
            }
            return cd;
        }

        // PUT: api/ChuDe/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ChuDe/5
        public void Delete(int id)
        {
        }
    }
}
