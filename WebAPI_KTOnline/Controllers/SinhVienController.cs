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
    public class SinhVienController : ApiController
    {
        // GET: api/SinhVien
        public IEnumerable<SinhVien> Get()
        {
            List<SinhVien> listdanhsach = SinhVien.DsachSV();
            return listdanhsach;
        }

        // GET: api/SinhVien/5
        public IEnumerable<SinhVien> Get(string masv)
        {
            SinhVien sv = new SinhVien();
            sv = sv.sv(masv);
            yield return sv;
        }
        [HttpGet]
        [Route("api/Dsach-Sinh-Vien-theo-Lop")]
        public IEnumerable<SinhVien> Get_dsach_theolop(string malop)
        {
            List<SinhVien> listdanhsach = SinhVien.DsachSV_theolop(malop);
            return listdanhsach;
        }
        [HttpPost]
        [Route("api/Kiem-tra-sinh-vien-vo-lam-bai/{maSinhVien}/{maBaiKiemTra}")]
        public SinhVien PostCheck([FromUri] string maSinhVien, string maBaiKiemTra)
        {
            SinhVien sinhVien = new SinhVien();
            sinhVien = SinhVien.svThuocLopDangKiemTra(maSinhVien, maBaiKiemTra);
            return sinhVien;
        }
        // POST: api/SinhVien
        public SinhVien Post([FromBody]SinhVien sinhvien)
        {
            SinhVien sv = new SinhVien();
            SqlConnection conn = DataProvider.Connect();
            string gioitinh = "";
            int result = 0;
            int trangthai = 1;
            if(Convert.ToInt32(sinhvien.gioiTinh) == 0)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
            }
            conn.Open();
            if (sv.kiemtra(sinhvien.maSinhVien))
            {
                try
                {
                    String sQuery = "INSERT INTO [dbo].[SinhVien]([MaSV],[TenSV],[GioiTinh],[DiaChi],[SDT],[Email],[Passsword],[Malop],[TrangThai])VALUES(@masv,@tensv,@gioitinh,@diachi,@sdt,@email,@matkhau,@malop,@trangthai)";
                    SqlCommand insert_SVcommand = new SqlCommand(sQuery, conn);
                    insert_SVcommand.Parameters.AddWithValue("@masv", sinhvien.maSinhVien.Trim());
                    insert_SVcommand.Parameters.AddWithValue("@tensv", sinhvien.tenSinhVien.Trim());
                    insert_SVcommand.Parameters.AddWithValue("@gioitinh", gioitinh);
                    insert_SVcommand.Parameters.AddWithValue("@diachi", sinhvien.diaChi);
                    insert_SVcommand.Parameters.AddWithValue("@sdt", sinhvien.soDienThoai.Trim());
                    insert_SVcommand.Parameters.AddWithValue("@email", sinhvien.email.Trim());
                    insert_SVcommand.Parameters.AddWithValue("@matkhau", StringProc.MD5Hash(sinhvien.matKhau));
                    insert_SVcommand.Parameters.AddWithValue("@malop", sinhvien.maLop);
                    insert_SVcommand.Parameters.AddWithValue("@trangthai", trangthai);
                    result = insert_SVcommand.ExecuteNonQuery();
                    conn.Close();
                }
                catch(Exception e)
                {
                    // not thing
                }
                sv = sv.sv(sinhvien.maSinhVien);
                if (result > 0)
                {
                    return sv;
                }
            }
            else
            {
                return sv;
            }
            return sv;
        }

        [Route("api/update-sinh-vien")]
        [HttpPost]
        public SinhVien Postupdate([FromBody]SinhVien sinhvien)
        {
            SinhVien sv = new SinhVien();
            string gioitinh = "";
            if (Convert.ToInt32(sinhvien.gioiTinh) == 0)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
            }
            int result = SinhVien.UpdateSinhVien(sinhvien, gioitinh);
            sv = sv.sv(sinhvien.maSinhVien);
            if (result > 0)
            {
                return sv;
            }
            return sv;
        }
        [Route("api/delete-sinh-vien")]
        [HttpPost]
        public SinhVien Postdelete([FromBody]SinhVien sinhvien)
        {
            SinhVien sv = new SinhVien();
            int result = SinhVien.DeleteSinhVien(sinhvien);
            sinhvien = sinhvien.sv(sinhvien.maSinhVien);
            if (result > 0)
            {
                return sinhvien;
            }
            return sinhvien;
        }

        // PUT: api/SinhVien/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SinhVien/5
        public void Delete(int id)
        {
        }
    }
}
