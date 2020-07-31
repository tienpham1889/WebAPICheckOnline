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
        public GiaoVien Post([FromBody]GiaoVien giaovien)
        {
            GiaoVien gvien = new GiaoVien();
            SqlConnection conn = DataProvider.Connect();
            string gioitinh = "";
            int trangthai = 1;
            string isAdmin = "";
            if (Convert.ToInt32(giaovien.gioiTinh) == 0)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
            }
            if(giaovien.isAdmin == "Giáo Viên")
            {
                isAdmin = "N";
            }
            else
            {
                isAdmin = "Y";
            }
            conn.Open();
            if (gvien.kiemtra(giaovien.maGiaoVien))
            {
                String sQuery = "INSERT INTO [dbo].[GiangVien]([MaGV],[TenGV],[GioiTinh],[DiaChi],[SDT],[Email],[Passsword],[isAdmin],[TrangThai])VALUES(@magv,@tengv,@gioitinh,@diachi,@sdt,@email,@matkhau,@isadmin,@trangthai)";
                SqlCommand insert_SVcommand = new SqlCommand(sQuery, conn);
                insert_SVcommand.Parameters.AddWithValue("@magv", giaovien.maGiaoVien.Trim());
                insert_SVcommand.Parameters.AddWithValue("@tengv", giaovien.tenGiaoVien.Trim());
                insert_SVcommand.Parameters.AddWithValue("@gioitinh", gioitinh);
                insert_SVcommand.Parameters.AddWithValue("@diachi", giaovien.diaChi);
                insert_SVcommand.Parameters.AddWithValue("@sdt", giaovien.soDienThoai.Trim());
                insert_SVcommand.Parameters.AddWithValue("@email", giaovien.email.Trim());
                insert_SVcommand.Parameters.AddWithValue("@matkhau", StringProc.MD5Hash(giaovien.matKhau));
                insert_SVcommand.Parameters.AddWithValue("@isadmin", isAdmin);
                insert_SVcommand.Parameters.AddWithValue("@trangthai", trangthai);
                int result = insert_SVcommand.ExecuteNonQuery();
                conn.Close();
                gvien = gvien.gv(giaovien.maGiaoVien);
                if (result > 0)
                {
                    return gvien;
                }
            }
            else
            {
                return gvien;
            }
            return gvien;
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
