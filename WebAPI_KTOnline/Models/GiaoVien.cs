using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI_KTOnline.Models
{
    public class GiaoVien
    {
        string magv;
        string tengv;
        string gioitinh;
        string diachi;
        string sdt;
        string Email;
        string passsword;
        int trangthai;
        string isadmin;
        public static List<GiaoVien> ListGV = DsachGV();

        public GiaoVien()
        {
            magv = "";
            tengv = "";
            gioitinh = "";
            diachi = "";
            sdt = "";
            Email = "";
            passsword = "";
            trangthai = 0;
            isadmin = "";
        }

        public string maGiaoVien { get => magv; set => magv = value; }
        public string tenGiaoVien { get => tengv; set => tengv = value; }
        public string gioiTinh { get => gioitinh; set => gioitinh = value; }
        public string diaChi { get => diachi; set => diachi = value; }
        public string soDienThoai { get => sdt; set => sdt = value; }
        public string email { get => Email; set => Email = value; }
        public string matKhau { get => passsword; set => passsword = value; }
        public int trangThai { get => trangthai; set => trangthai = value; }
        public string isAdmin { get => isadmin; set => isadmin = value; }
        public static List<GiaoVien> DsachGV()
        {
            List<GiaoVien> list = new List<GiaoVien>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = "Select * from GiangVien";
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                GiaoVien gv = new GiaoVien();
                gv.magv = dr.GetString(0);
                gv.tengv = dr.GetString(1);
                gv.gioitinh = dr.GetString(2);
                gv.diachi = dr.GetString(3);
                gv.sdt = dr.GetString(4);
                gv.email = dr.GetString(5);
                gv.passsword = dr.GetString(6);
                gv.isadmin = dr.GetString(7);
                gv.trangthai = dr.GetInt32(8);
                list.Add(gv);
            }
            conn.Close();
            return list;
        }
        public GiaoVien gv(string magv)
        {
            GiaoVien gv = new GiaoVien();

            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from GiangVien where MaGV = '{0}'", magv);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                gv.magv = dr.GetString(0);
                gv.tengv = dr.GetString(1);
                gv.gioitinh = dr.GetString(2);
                gv.diachi = dr.GetString(3);
                gv.sdt = dr.GetString(4);
                gv.email = dr.GetString(5);
                gv.passsword = dr.GetString(6);
                gv.isadmin = dr.GetString(7);
                gv.trangThai = dr.GetInt32(8);
            }
            conn.Close();
            return gv;
        }
        public bool kiemtra(string magv)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from GiangVien where MaGV = '{0}' ", magv);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                count++;

            }
            if (count > 0)
            {
                return false;
            }
            dr.Close();
            conn.Close();
            return true;
        }
    }
}