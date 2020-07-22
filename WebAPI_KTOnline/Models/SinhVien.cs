using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI_KTOnline.Models
{
    public class SinhVien
    {
        string masv;
        string tensv;
        string gioitinh;
        string diachi;
        string sdt;
        string Email;
        string pass;
        string malop;
        public static List<SinhVien> ListSV = DsachSV();
        public SinhVien()
        {
            masv = "";
            tensv = "";
            gioitinh = "";
            diachi = "";
            sdt = "";
            Email = "";
            pass = "";
            malop = "";
        }

        public string maSinhVien { get => masv; set => masv = value; }
        public string tenSinhVien { get => tensv; set => tensv = value; }
        public string gioiTinh { get => gioitinh; set => gioitinh = value; }
        public string diaChi { get => diachi; set => diachi = value; }
        public string soDienThoai { get => sdt; set => sdt = value; }
        public string email { get => Email; set => Email = value; }
        public string matKhau { get => pass; set => pass = value; }
        public string maLop { get => malop; set => malop = value; }
        public static List<SinhVien> DsachSV()
        {
            List<SinhVien> list = new List<SinhVien>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = "Select * from SinhVien where TrangThai = 1";
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                SinhVien sv = new SinhVien();
                sv.masv = dr.GetString(0);
                sv.tensv = dr.GetString(1);
                sv.gioitinh = dr.GetString(2);
                sv.diachi = dr.GetString(3);
                sv.sdt = dr.GetString(4);
                sv.email = dr.GetString(5);
                sv.pass = dr.GetString(6);
                sv.malop = dr.GetString(7);
                list.Add(sv);
            }
            conn.Close();
            return list;
        }
        public SinhVien sv(string masv)
        {
            SinhVien sv = new SinhVien();

            string ChuoiKetNoi = @"Data Source=MR-TIEN\SQLEXPRESS;Initial Catalog =DBCheckOnline;Integrated Security = True";
            SqlConnection conn = new SqlConnection(ChuoiKetNoi);
            conn.Open();
            string sQuery = string.Format("select * from SinhVien where MaSV = '{0}'", masv);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                sv.masv = dr.GetString(0);
                sv.tensv = dr.GetString(1);
                sv.gioitinh = dr.GetString(2);
                sv.diachi = dr.GetString(3);
                sv.sdt = dr.GetString(4);
                sv.email = dr.GetString(5);
                sv.pass = dr.GetString(6);
                sv.malop = dr.GetString(7);
            }
            conn.Close();
            return sv;
        }
    }
}