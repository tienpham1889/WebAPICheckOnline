﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebAPI_KTOnline.Models;
using System.Text;

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
            string sQuery = string.Format("select * from CTKetQua where MaSV = '{0}' and MaBaiKT = '{1}'", masinhvien, mabaikt);
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
        public static void UPDATE_Diem(string mabaikt, string masinhvien)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            float so_diem_tren_mot_cau = 0;
            int soCauDung = 0;
            decimal tongDiem = 0;
            try
            {
                string sQuery_lenght = string.Format("Select * from CTKetQua where MaBaiKT = '{0}'", mabaikt);
                SqlCommand comm = new SqlCommand(sQuery_lenght, conn);
                SqlDataReader dr = comm.ExecuteReader();
                int soCau = 0;
                while (dr.Read())
                {
                    soCau++;

                }
                dr.Close();
                so_diem_tren_mot_cau = 10 / (float)soCau;
                StringBuilder sQuery_cauDung = new StringBuilder();
                sQuery_cauDung.Append("select KQ.MaCauHoi, KQ.DapAn, CH.MaCauHoi, CH.DapAn ");
                sQuery_cauDung.Append("from CTKetQua KQ ");
                sQuery_cauDung.Append("INNER JOIN CauHoi CH ON KQ.MaCauHoi = CH.MaCauHoi ");
                sQuery_cauDung.AppendFormat("WHERE KQ.MaBaiKT = '{0}' and kq.DapAn = CH.DapAn", mabaikt);
                SqlCommand comm2 = new SqlCommand(sQuery_cauDung.ToString(), conn);
                SqlDataReader dr2 = comm2.ExecuteReader();
                while (dr2.Read())
                {
                    soCauDung++;

                }
                dr2.Close();
                tongDiem = Convert.ToDecimal(soCauDung * so_diem_tren_mot_cau);
                string sQuery = string.Format("UPDATE [KetQua] SET Diem = {0} FROM KetQua where MaSV = '{1}' and MaBaiKT = '{2}' ", tongDiem, masinhvien, mabaikt);
                SqlCommand updatecommand = new SqlCommand(sQuery, conn);
                int result = updatecommand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                //no thing 
            }

        }
    }
}