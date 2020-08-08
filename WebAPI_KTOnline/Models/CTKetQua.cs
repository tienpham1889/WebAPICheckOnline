using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace WebAPI_KTOnline.Models
{
    public class CTKetQua
    {
        string masinhvien;
        string mabaikiemtra;
        string macauhoi;
        string dapan;

        public CTKetQua()
        {
            maSinhVien = "";
            maBaiKiemTra = "";
            maCauHoi = "";
            dapAn = "";
        }

        public string maSinhVien { get => masinhvien; set => masinhvien = value; }
        public string maBaiKiemTra { get => mabaikiemtra; set => mabaikiemtra = value; }
        public string maCauHoi { get => macauhoi; set => macauhoi = value; }
        public string dapAn { get => dapan; set => dapan = value; }

        public static void AddCTKQ(string masinhvien, string mabaikiemtra)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            try
            {
                string sQuery = string.Format("INSERT INTO CTKetQua (MaSV,MaBaiKT,MaCauHoi) SELECT '{0}', '{1}', MaCauHoi From CTBaiKT where MaBaiKT ='{1}'", masinhvien, mabaikiemtra);
                SqlCommand insertcommand_cauhoi = new SqlCommand(sQuery, conn);
                int result= insertcommand_cauhoi.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                //no thing 
            }

        }
        public static bool kiemtra(string masinhvien, string mabaikiemtra, string macauhoi)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from CTKetQua where MaSV = '{0}' and MaBaiKT = '{1}' and MaCauHoi = '{2}'", masinhvien, mabaikiemtra,macauhoi);
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
        public static void AddPhuongAn(string[] mangPhuongAn, string masinhvien, string mabaikiemtra)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            try
            {
                //string sQuery = string.Format("UPDATE CTKetQua SET DapAn = '{0}' SELECT '{1}', '{2}', MaCauHoi From CTBaiKT where MaBaiKT ='{2}'", mangPhuongAn[1], masinhvien, mabaikiemtra);
                //SqlCommand insertcommand = new SqlCommand(sQuery, conn);
                //insertcommand.Parameters.AddWithValue("@mach", mach);
                //int result = insertcommand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                //no thing 
            }

        }
    }
}