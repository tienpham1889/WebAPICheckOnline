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
        [Route("api/Dsach-Lop-Hoc-Phan-Theo-Lop")]
        public IEnumerable<LopHocPhan> Get_dsach_theolop(string malop)
        {
            List<LopHocPhan> listdanhsach = LopHocPhan.DsachLopHP_theolop(malop);
            return listdanhsach;
        }
        [HttpGet]
        [Route("api/LopHocPhan-TheoMonhoc")]
        public IEnumerable<LopHocPhan> Get_lophocphan(string mamh)
        {
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
            int result = 0;
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string malhp = LopHocPhan.layLopHocPhan();
            if (lhp.kiemtra(malhp))
            {
                try
                {
                    String sQuery = "INSERT INTO [dbo].[LopHocPhan]([MaLopHP],[TenLopHP],[MaGV],[MaMonHoc],[MaLop],[TrangThai])VALUES(@malophp,@tenlophp,@magv,@mamonhoc,@malop,@trangthai)";
                    SqlCommand insert_LHPcommand = new SqlCommand(sQuery, conn);
                    insert_LHPcommand.Parameters.AddWithValue("@malophp", malhp);
                    insert_LHPcommand.Parameters.AddWithValue("@tenlophp", lopHP.tenLopHocPhan);
                    insert_LHPcommand.Parameters.AddWithValue("@magv", lopHP.maGiaoVien);
                    insert_LHPcommand.Parameters.AddWithValue("@mamonhoc", lopHP.maMonHoc);
                    insert_LHPcommand.Parameters.AddWithValue("@malop", lopHP.maLop);
                    insert_LHPcommand.Parameters.AddWithValue("@trangthai", lopHP.trangThai);
                    result = insert_LHPcommand.ExecuteNonQuery();
                    conn.Close();
                }
                catch(Exception e)
                {
                    // not thing
                }
                
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
            
            return lhp;
        }
        [Route("api/update-lop-hoc-phan")]
        [HttpPost]
        public LopHocPhan Postupdate([FromBody]LopHocPhan lopHP)
        {
            LopHocPhan lhp = new LopHocPhan();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            String sQuery = "UPDATE [dbo].[LopHocPhan] SET [TenLopHP] = @tenlophp, [MaGV] = @magv, [MaMonHoc]=@mamh, [MaLop]=@malop, [TrangThai] = 1 WHERE [MaLopHP] = @malophp";
            SqlCommand updatecommand = new SqlCommand(sQuery, conn);
            updatecommand.Parameters.AddWithValue("@tenlophp", lopHP.tenLopHocPhan);
            updatecommand.Parameters.AddWithValue("@magv", lopHP.maGiaoVien);
            updatecommand.Parameters.AddWithValue("@mamh", lopHP.maMonHoc);
            updatecommand.Parameters.AddWithValue("@malop", lopHP.maLop);
            updatecommand.Parameters.AddWithValue("@malophp", lopHP.maLopHocPhan);
            int result = updatecommand.ExecuteNonQuery();
            conn.Close();
            lhp = lhp.lhp(lopHP.maLopHocPhan);
            if (result > 0)
            {
                return lhp;
            }
            return lhp;
        }
        [Route("api/delete-lop-hoc-phan")]
        [HttpPost]
        public LopHocPhan Postdelete([FromBody]LopHocPhan lopHP)
        {
            LopHocPhan lhp = new LopHocPhan();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            String sQuery = "UPDATE [dbo].[LopHocPhan] SET [TrangThai] = @trangthai  WHERE [MaLopHP] = @malophp";
            SqlCommand updatecommand = new SqlCommand(sQuery, conn);
            updatecommand.Parameters.AddWithValue("@trangthai", 2);
            updatecommand.Parameters.AddWithValue("@malophp", lopHP.maLopHocPhan);
            int result = updatecommand.ExecuteNonQuery();
            conn.Close();
            lhp = lhp.lhp(lopHP.maLopHocPhan);
            if (result > 0)
            {
                return lhp;
            }
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
