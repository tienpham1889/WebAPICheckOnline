using System;
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
        decimal Diem;
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
        public decimal diem { get => Diem; set => Diem = value; }
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
                kq.diem = dr.GetDecimal(2);
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
                kq.diem = dr.GetDecimal(2);
                kq.trangthai = dr.GetInt32(3);
            }
            dr.Close();
            conn.Close();
            return kq;
        }
        public static int AddKetQua(string masinhvien, string mabaikiemtra)
        {
            SqlConnection conn = DataProvider.Connect();
            int result = 0;
            conn.Open();
            try
            {
                String sQuery = "INSERT INTO [dbo].[KetQua]([MaSV],[MaBaiKT],[Diem],[TrangThai])VALUES(@masv,@mabaikt,@diem,@trangthai)";
                SqlCommand insertcommand = new SqlCommand(sQuery, conn);
                insertcommand.Parameters.AddWithValue("@masv", masinhvien);
                insertcommand.Parameters.AddWithValue("@mabaikt", mabaikiemtra);
                insertcommand.Parameters.AddWithValue("@diem", 0);
                insertcommand.Parameters.AddWithValue("@trangthai", 1);
                result = insertcommand.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception e)
            {
                //not thing
            }
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
            decimal so_diem_tren_mot_cau = 0;
            int soCauDung = 0;
            decimal tongDiem = 0;
            try
            {
                string sQuery_lenght = string.Format("Select * from CTKetQua where MaBaiKT = '{0}' and MaSV = '{1}'", mabaikt, masinhvien);
                SqlCommand comm = new SqlCommand(sQuery_lenght, conn);
                SqlDataReader dr = comm.ExecuteReader();
                int soCau = 0;
                while (dr.Read())
                {
                    soCau++;

                }
                dr.Close();
                so_diem_tren_mot_cau = 10 / (decimal)soCau;
                StringBuilder sQuery_cauDung = new StringBuilder();
                sQuery_cauDung.Append("select distinct KQ.MaCauHoi, KQ.DapAn, CH.MaCauHoi, CH.DapAn ");
                sQuery_cauDung.Append("from CTKetQua KQ ");
                sQuery_cauDung.Append("INNER JOIN CauHoi CH ON KQ.MaCauHoi = CH.MaCauHoi ");
                sQuery_cauDung.AppendFormat("WHERE KQ.MaBaiKT = '{0}' and kq.DapAn = CH.DapAn and KQ.MaSV='{1}'", mabaikt, masinhvien);
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
        public static List<KetQua> DsachKetQua_theogv(string magiaovien)
        {
            List<KetQua> list = new List<KetQua>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("select KQ.MaSV, KQ.MaBaiKT, KQ.Diem, KQ.TrangThai ");
            sQuery.Append("from KetQua KQ  ");
            sQuery.Append("INNER JOIN BaiKiemTra BKT ON KQ.MaBaiKT = BKT.MaBaiKT  ");
            sQuery.Append("INNER JOIN GiangVien GV ON GV.MaGV = BKT.MaGV ");
            sQuery.AppendFormat("WHERE GV.MaGV = '{0}' ", magiaovien);
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                KetQua ketQua = new KetQua();
                ketQua.masinhvien = dr.GetString(0);
                ketQua.mabaikiemtra = dr.GetString(1);
                ketQua.diem = dr.GetDecimal(2);
                ketQua.trangthai = dr.GetInt32(3);
                list.Add(ketQua);
            }
            conn.Close();
            return list;
        }
        public static List<KetQua> DsachKetQua_theosv(string masv)
        {
            List<KetQua> list = new List<KetQua>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("select * from KetQua ");
                sQuery.AppendFormat("WHERE MaSV = '{0}' ", masv);
                SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    KetQua ketQua = new KetQua();
                    ketQua.masinhvien = dr.GetString(0);
                    ketQua.mabaikiemtra = dr.GetString(1);
                    ketQua.diem = dr.GetDecimal(2);
                    ketQua.trangthai = dr.GetInt32(3);
                    list.Add(ketQua);
                }
                conn.Close();
            }
            catch(Exception e)
            {
                //not thing
            }
            
            return list;
        }
        public static List<KetQua> DsachSV_DangKT_theobaikt(string mabaikt, string magv)
        {
            List<KetQua> list = new List<KetQua>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("SELECT KQ.MaSV, KQ.MaBaiKT, KQ.TrangThai ");
            sQuery.Append("FROM KetQua KQ ");
            sQuery.Append("INNER JOIN BaiKiemTra BKT ON KQ.MaBaiKT = BKT.MaBaiKT ");
            sQuery.AppendFormat("WHERE KQ.TrangThai = 1 AND BKT.MaGV = '{0}' and KQ.MaBaiKT = '{1}' AND BKT.TrangThai = 2", magv, mabaikt);
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                KetQua ketQua = new KetQua();
                ketQua.masinhvien = dr.GetString(0);
                ketQua.mabaikiemtra = dr.GetString(1);
                ketQua.trangthai = dr.GetInt32(2);
                list.Add(ketQua);
            }
            conn.Close();
            return list;
        }
        public static List<KetQua> DsachSV_DangKT(string magv)
        {
            List<KetQua> list = new List<KetQua>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("SELECT KQ.MaSV, KQ.MaBaiKT, KQ.TrangThai ");
            sQuery.Append("FROM KetQua KQ ");
            sQuery.Append("INNER JOIN BaiKiemTra BKT ON KQ.MaBaiKT = BKT.MaBaiKT ");
            sQuery.AppendFormat("WHERE KQ.TrangThai = 1 AND BKT.MaGV = '{0}' AND BKT.TrangThai = 2", magv);
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                KetQua ketQua = new KetQua();
                ketQua.masinhvien = dr.GetString(0);
                ketQua.mabaikiemtra = dr.GetString(1);
                ketQua.trangthai = dr.GetInt32(2);
                list.Add(ketQua);
            }
            conn.Close();
            return list;
        }
    }
}