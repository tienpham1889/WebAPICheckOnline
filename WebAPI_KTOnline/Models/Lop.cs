using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI_KTOnline.Models
{
    public class Lop
    {
        string Malop;
        string TenLop;
        int Soluongsinhvien;
        int Tranhthai;

        public Lop()
        {
            Malop = "";
            TenLop = "";
            Soluongsinhvien = 0;
            Tranhthai = 0;
        }

        public string maLop { get => Malop; set => Malop = value; }
        public string tenLop { get => TenLop; set => TenLop = value; }
        public int soLuongSinhVien { get => Soluongsinhvien; set => Soluongsinhvien = value; }
        public int trangThai { get => Tranhthai; set => Tranhthai = value; }

        public static List<Lop> DsachLop()
        {
            List<Lop> list = new List<Lop>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = "Select * from Lop";
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                Lop lop = new Lop();
                lop.Malop = dr.GetString(0);
                lop.TenLop = dr.GetString(1);
                lop.Soluongsinhvien = dr.GetInt32(2);
                lop.Tranhthai = dr.GetInt32(3);
                list.Add(lop);
            }
            conn.Close();
            return list;
        }
        public Lop lop(string malop)
        {
            Lop lop = new Lop();

            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from Lop where MaLop = '{0}'", malop);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                lop.maLop = dr.GetString(0);
                lop.tenLop = dr.GetString(1);
                lop.soLuongSinhVien = dr.GetInt32(2);
                lop.trangThai = dr.GetInt32(3);
            }
            conn.Close();
            return lop;
        }
        public bool kiemtra(string malop)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from Lop where MaLop = '{0}'", malop);
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
            return true;
        }
    }
}