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
        [HttpGet]
        [Route("api/LopHocPhan-TheoMonhoc")]
        public IEnumerable<LopHocPhan> Get_lophocphan(string tenmh)
        {
            string mamh = "";
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("SELECT MaMonHoc FROM MonHoc WHERE TenMonHoc = N'{0}'", tenmh);
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                mamh = dr.GetString(0);
            }
            conn.Close();
            List<LopHocPhan> listdanhsach = LopHocPhan.DsachLHP_Theomonhoc(mamh);
            return listdanhsach;
        }
        // GET: api/LopHocPhan/5
        public IEnumerable<LopHocPhan> Get(string malophp)
        {
            LopHocPhan lophp = new LopHocPhan();
            lophp = lophp.lhp(malophp);
            yield return lophp;
        }

        // POST: api/LopHocPhan
        public LopHocPhan Post([FromBody]LopHocPhan lopHP)
        {
            LopHocPhan lhp = new LopHocPhan();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string malhp = LopHocPhan.layLopHocPhan();
            string mamh = "";
            string magv = "";
            string malop = "";
            string sQuery_getMonHoc = string.Format("SELECT MaMonHoc FROM MonHoc WHERE TenMonHoc = N'{0}'", lopHP.maMonHoc);
            string sQuery_getGiaoVien = string.Format("SELECT MaGV FROM GiangVien WHERE TenGV = N'{0}'", lopHP.maGiaoVien);
            string sQuery_getLop = string.Format("SELECT MaLop FROM Lop WHERE TenLop = N'{0}'", lopHP.maLop);
            SqlCommand com1 = new SqlCommand(sQuery_getMonHoc, conn);
            SqlCommand com2 = new SqlCommand(sQuery_getGiaoVien, conn);
            SqlCommand com3 = new SqlCommand(sQuery_getLop, conn);
            SqlDataReader dr = com1.ExecuteReader();
            while (dr.Read())
            {
                mamh = dr.GetString(0);
            }
            dr.Close();
            SqlDataReader dr2 = com2.ExecuteReader();
            while (dr2.Read())
            {
                magv = dr2.GetString(0);
            }
            dr2.Close();
            SqlDataReader dr3 = com3.ExecuteReader();
            while (dr3.Read())
            {
                malop = dr3.GetString(0);
            }
            dr3.Close();
            if (lhp.kiemtra(lopHP.tenLopHocPhan))
            {

                String sQuery = "INSERT INTO [dbo].[LopHocPhan]([MaLopHP],[TenLopHP],[MaGV],[MaMonHoc],[MaLop],[TrangThai])VALUES(@malophp,@tenlophp,@magv,@mamonhoc,@malop,@trangthai)";
                SqlCommand insert_LHPcommand = new SqlCommand(sQuery, conn);
                insert_LHPcommand.Parameters.AddWithValue("@malophp", malhp);
                insert_LHPcommand.Parameters.AddWithValue("@tenlophp", lopHP.tenLopHocPhan);
                insert_LHPcommand.Parameters.AddWithValue("@magv", magv);
                insert_LHPcommand.Parameters.AddWithValue("@mamonhoc", mamh);
                insert_LHPcommand.Parameters.AddWithValue("@malop", malop);
                insert_LHPcommand.Parameters.AddWithValue("@trangthai", lopHP.trangThai);
                int result = insert_LHPcommand.ExecuteNonQuery();

                lhp = lhp.lhp(malhp);
                if (result > 0)
                {
                    return lhp;
                }
            }

            else
            {
                return lhp;
            }
            conn.Close();
            return lhp;
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
