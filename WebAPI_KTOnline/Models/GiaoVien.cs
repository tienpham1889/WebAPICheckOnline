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
        string email;
        string passsword;
        public static List<GiaoVien> ListGV = DsachGV();

        public GiaoVien()
        {
            Magv = "";
            Tengv = "";
            Gioitinh = "";
            Diachi = "";
            Sdt = "";
            Email = "";
            Passsword = "";
        }

        public string Magv { get => magv; set => magv = value; }
        public string Tengv { get => tengv; set => tengv = value; }
        public string Gioitinh { get => gioitinh; set => gioitinh = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string Email { get => email; set => email = value; }
        public string Passsword { get => passsword; set => passsword = value; }
        public static List<GiaoVien> DsachGV()
        {
            List<GiaoVien> list = new List<GiaoVien>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = "Select * from GiangVien where TrangThai = 1";
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
                list.Add(gv);
            }
            conn.Close();
            return list;
        }
        public GiaoVien gv(string magv)
        {
            GiaoVien gv = new GiaoVien();

            string ChuoiKetNoi = @"Data Source=MR-TIEN\SQLEXPRESS;Initial Catalog =DBCheckOnline;Integrated Security = True";
            SqlConnection conn = new SqlConnection(ChuoiKetNoi);
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
            }
            conn.Close();
            return gv;
        }
    }
}