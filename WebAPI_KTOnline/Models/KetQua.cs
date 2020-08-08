using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebAPI_KTOnline.Models;

namespace WebAPI_KTOnline.Models
{
    public class KetQua
    {
        string masinhvien;
        string mabaikiemtra;
        int Diem;
        int trangthai;

        public KetQua()
        {
            maSinhVien = "";
            maBaiKiemTra = "";
            diem = 0;
            trangThai = 0;

        }

        public string maSinhVien { get => masinhvien; set => masinhvien = value; }
        public string maBaiKiemTra { get => mabaikiemtra; set => mabaikiemtra = value; }
        public int diem { get => Diem; set => Diem = value; }
        public int trangThai { get => trangthai; set => trangthai = value; }

        public static List<KetQua> DsachKetQua()
        {
            List<KetQua> list = new List<KetQua>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = "SELECT * FROM KetQua";
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                KetQua kq = new KetQua();
                kq.masinhvien = dr.GetString(0);
                kq.mabaikiemtra = dr.GetString(1);
                kq.diem = dr.GetInt32(2);
                kq.trangthai = dr.GetInt32(3);
                list.Add(kq);
            }
            conn.Close();
            return list;
        }
        public KetQua ketQua(string masinhvien, string mabaikiemtra)
        {
            KetQua kq = new KetQua();

            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from KetQua where MaSV = '{0}' and MaBaiKT = '{1}'", masinhvien, mabaikiemtra);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                kq.masinhvien = dr.GetString(0);
                kq.mabaikiemtra = dr.GetString(1);
                kq.diem = dr.GetInt32(2);
                kq.trangthai = dr.GetInt32(3);
            }
            dr.Close();
            conn.Close();
            return kq;
        }
        public static int AddKetQua(string masinhvien, string mabaikiemtra)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            String sQuery = "INSERT INTO [dbo].[KetQua]([MaSV],[MaBaiKT],[Diem],[TrangThai])VALUES(@masv,@mabaikt,@diem,@trangthai)";
            SqlCommand insertcommand = new SqlCommand(sQuery, conn);
            insertcommand.Parameters.AddWithValue("@masv", masinhvien);
            insertcommand.Parameters.AddWithValue("@mabaikt", mabaikiemtra);
            insertcommand.Parameters.AddWithValue("@diem", 0);
            insertcommand.Parameters.AddWithValue("@trangthai", 1);
            int result = insertcommand.ExecuteNonQuery();
            conn.Close();
            return result;

        }
        public bool kiemtra(string masinhvien, string mabaikt)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from KetQua where MaSV = '{0}' and MaBaiKT = '{1}'", masinhvien, mabaikt);
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