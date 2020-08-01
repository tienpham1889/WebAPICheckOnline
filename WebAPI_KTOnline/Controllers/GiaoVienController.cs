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
            if (Convert.ToInt32(giaovien.gioiTinh) == 0)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
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
                insert_SVcommand.Parameters.AddWithValue("@isadmin", giaovien.isAdmin);
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

        [Route("api/update-giao-vien")]
        [HttpPost]
        public GiaoVien Postupdate([FromBody]GiaoVien giaovien)
        {
            GiaoVien gv = new GiaoVien();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string gioitinh = "";
            if (Convert.ToInt32(giaovien.gioiTinh) == 0)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
            }
            String sQuery = "UPDATE [dbo].[GiangVien] SET [TenGV] = @tengv, [GioiTinh] = @gioitinh, [DiaChi]=@diachi, [SDT]=@sdt, [Email] =@email, [Passsword] = @pass, [isAdmin] = @isAdmin WHERE [MaGV] = @magv";
            SqlCommand updatecommand = new SqlCommand(sQuery, conn);
            updatecommand.Parameters.AddWithValue("@tengv", giaovien.tenGiaoVien.Trim());
            updatecommand.Parameters.AddWithValue("@gioitinh", gioitinh);
            updatecommand.Parameters.AddWithValue("@diachi", giaovien.diaChi.Trim());
            updatecommand.Parameters.AddWithValue("@sdt", giaovien.soDienThoai.Trim());
            updatecommand.Parameters.AddWithValue("@email", giaovien.email.Trim());
            updatecommand.Parameters.AddWithValue("@pass", StringProc.MD5Hash(giaovien.matKhau)); ;
            updatecommand.Parameters.AddWithValue("@isAdmin",giaovien.isAdmin );
            updatecommand.Parameters.AddWithValue("@magv", giaovien.maGiaoVien);
            int result = updatecommand.ExecuteNonQuery();
            conn.Close();
            gv = gv.gv(giaovien.maGiaoVien);
            if (result > 0)
            {
                return gv;
            }
            return gv;
        }
        [Route("api/delete-giao-vien")]
        [HttpPost]
        public GiaoVien Postdelete([FromBody]GiaoVien giaoVien)
        {
            GiaoVien gv = new GiaoVien();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            String sQuery = "UPDATE [dbo].[GiangVien] SET [TrangThai] = @trangthai  WHERE [MaGV] = @magv";
            SqlCommand updatecommand = new SqlCommand(sQuery, conn);
            updatecommand.Parameters.AddWithValue("@trangthai", 2);
            updatecommand.Parameters.AddWithValue("@magv", giaoVien.maGiaoVien);
            int result = updatecommand.ExecuteNonQuery();
            conn.Close();
            giaoVien = giaoVien.gv(giaoVien.maGiaoVien);
            if (result > 0)
            {
                return giaoVien;
            }
            return giaoVien;
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
