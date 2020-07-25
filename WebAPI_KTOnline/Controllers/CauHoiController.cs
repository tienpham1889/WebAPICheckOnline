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
        public CauHoi Post([FromBody]CauHoi cauhoi)
        {
            CauHoi ch = new CauHoi();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string mach = CauHoi.layMaCauHoi();
            string mamh = "";
            string macd = "";
            int trangthai = 1;
            string sQuery_getMonHoc = string.Format("SELECT MaMonHoc FROM MonHoc WHERE TenMonHoc = N'{0}'", cauhoi.maMonHoc);
            string sQuery_getChuDe = string.Format("SELECT MaCD FROM ChuDe WHERE TenCD = N'{0}'", cauhoi.maChuDe);
            SqlCommand com = new SqlCommand(sQuery_getMonHoc, conn);
            SqlCommand com2 = new SqlCommand(sQuery_getChuDe, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                mamh = dr.GetString(0);
            }
            dr.Close();
            SqlDataReader dr2 = com2.ExecuteReader();
            while (dr2.Read())
            {
                macd = dr2.GetString(0);
            }
            dr2.Close();
            if (ch.kiemtra(cauhoi.noiDung, mamh))
            {

                String sQuery = "INSERT INTO [dbo].[CauHoi]([MaCauHoi],[NoiDung],[PhuongAnA],[PhuongAnB],[PhuongAnC],[PhuongAnD],[DapAn],[MaCD],[TrangThai],[MaMonHoc])VALUES(@mach,@noidung,@phuongana,@phuonganb,@phuonganc,@phuongand,@dapan,@macd,@trangthai,@mamonhoc)";
                SqlCommand insert_CauHoicommand = new SqlCommand(sQuery, conn);
                insert_CauHoicommand.Parameters.AddWithValue("@mach", mach);
                insert_CauHoicommand.Parameters.AddWithValue("@noidung", cauhoi.noiDung);
                insert_CauHoicommand.Parameters.AddWithValue("@phuongana", cauhoi.phuongAnA);
                insert_CauHoicommand.Parameters.AddWithValue("@phuonganb", cauhoi.phuongAnB);
                insert_CauHoicommand.Parameters.AddWithValue("@phuonganc", cauhoi.phuongAnC);
                insert_CauHoicommand.Parameters.AddWithValue("@phuongand", cauhoi.phuongAnD);
                insert_CauHoicommand.Parameters.AddWithValue("@dapan", cauhoi.dapAn);
                insert_CauHoicommand.Parameters.AddWithValue("@macd", macd);
                insert_CauHoicommand.Parameters.AddWithValue("@trangthai", trangthai);
                insert_CauHoicommand.Parameters.AddWithValue("@mamonhoc", mamh);
                int result = insert_CauHoicommand.ExecuteNonQuery();

                ch = ch.cauhoi(mach);
                if (result > 0)
                {
                    return ch;
                }
            }

            else
            {
                return ch;
            }
            conn.Close();
            return ch;
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
