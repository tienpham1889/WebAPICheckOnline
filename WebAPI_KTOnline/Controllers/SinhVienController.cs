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

        // POST: api/SinhVien
        public SinhVien Post([FromBody]SinhVien sinhvien)
        {
            SinhVien sv = new SinhVien();
            SqlConnection conn = DataProvider.Connect();
            string gioitinh = "";
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
                String sQuery = "INSERT INTO [dbo].[SinhVien]([MaSV],[TenSV],[GioiTinh],[DiaChi],[SDT],[Email],[Passsword],[Malop],[TrangThai])VALUES(@masv,@tensv,@gioitinh,@diachi,@sdt,@email,@matkhau,@malop,@trangthai)";
                SqlCommand insert_SVcommand = new SqlCommand(sQuery, conn);
                insert_SVcommand.Parameters.AddWithValue("@masv", sinhvien.maSinhVien);
                insert_SVcommand.Parameters.AddWithValue("@tensv", sinhvien.tenSinhVien);
                insert_SVcommand.Parameters.AddWithValue("@gioitinh", gioitinh);
                insert_SVcommand.Parameters.AddWithValue("@diachi", sinhvien.diaChi);
                insert_SVcommand.Parameters.AddWithValue("@sdt", sinhvien.soDienThoai);
                insert_SVcommand.Parameters.AddWithValue("@email", sinhvien.email);
                insert_SVcommand.Parameters.AddWithValue("@matkhau", StringProc.MD5Hash(sinhvien.matKhau));
                insert_SVcommand.Parameters.AddWithValue("@malop", sinhvien.maLop);
                insert_SVcommand.Parameters.AddWithValue("@trangthai", trangthai);
                int result = insert_SVcommand.ExecuteNonQuery();
                conn.Close();
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
