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
    public class BaiKiemTra
    {
        string mabaikt;
        string tenbaikt;
        string KeyBaiKT;
        int thoigianlam;
        DateTime NgayTao;
        string MaGV;
        string MaLHP;
        int TrangThai;
        public BaiKiemTra()
        {
            mabaikt = "";
            tenbaikt = "";
            KeyBaiKT = "";
            thoigianlam = 0;
            DateTime now = DateTime.Now;
            NgayTao = now;
            MaGV = "";
            MaLHP = "";
            TrangThai = 0;

        }

        public string maBaiKiemTra { get => mabaikt; set => mabaikt = value; }
        public string tenBaiKiemTra { get => tenbaikt; set => tenbaikt = value; }
        public string pin { get => KeyBaiKT; set => KeyBaiKT = value; }
        public int thoiGianLam { get => thoigianlam; set => thoigianlam = value; }
        public DateTime ngayTao { get => NgayTao; set => NgayTao = value; }
        public string maGiaoVien { get => MaGV; set => MaGV = value; }
        public string maLopHocPhan { get => MaLHP; set => MaLHP = value; }
        public int trangThai { get => TrangThai; set => TrangThai = value; }
        public static List<BaiKiemTra> DsachBaiKiemTra()
        {
            List<BaiKiemTra> list = new List<BaiKiemTra>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("SELECT BKT.MaBaiKT, BKT.TenBaiKT, BKT.KeyBaiKT, BKT.ThoiGianLam, BKT.NgayTao, GV.TenGV, LHP.TenLopHP, BKT.TrangThai ");
            sQuery.Append("FROM BaiKiemTra BKT  ");
            sQuery.Append("INNER JOIN GiangVien GV ON BKT.MaGV = GV.MaGV  ");
            sQuery.Append("INNER JOIN LopHocPhan LHP ON BKT.MaLopHP = LHP.MaLopHP ");
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                int sophutlam = Convert.ToInt32(dr.GetInt32(3))/60;
                BaiKiemTra bkt = new BaiKiemTra();
                bkt.mabaikt = dr.GetString(0);
                bkt.tenbaikt = dr.GetString(1);
                bkt.KeyBaiKT = dr.GetString(2);
                bkt.thoigianlam = sophutlam;
                bkt.NgayTao = dr.GetDateTime(4);
                bkt.MaGV = dr.GetString(5);
                bkt.MaLHP = dr.GetString(6);
                bkt.TrangThai = dr.GetInt32(7);
                list.Add(bkt);
            }
            conn.Close();
            return list;
        }
        public BaiKiemTra baikt(string mabkt)
        {
            BaiKiemTra baikt = new BaiKiemTra();

            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from BaiKiemTra where MaBaiKT = '{0}'", mabkt);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                int sophutlam = Convert.ToInt32(dr.GetInt32(3)) / 60;
                baikt.mabaikt = dr.GetString(0);
                baikt.tenbaikt = dr.GetString(1);
                baikt.KeyBaiKT = dr.GetString(2);
                baikt.thoigianlam = sophutlam;
                baikt.NgayTao = dr.GetDateTime(4);
                baikt.MaGV = dr.GetString(5);
                baikt.MaLHP = dr.GetString(6);
                baikt.TrangThai = dr.GetInt32(7);
            }
            dr.Close();
            conn.Close();
            return baikt;
        }
        public static int AddBaiKT(BaiKiemTra newTest, string mabaikt, string malophp)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            int thoigian = Convert.ToInt32(newTest.thoigianlam) * 60;
            String sQuery = "INSERT INTO [dbo].[BaiKiemTra]([MaBaiKT],[TenBaiKT],[KeyBaiKT],[ThoiGianLam],[NgayTao],[MaGV],[MaLopHP],[TrangThai])VALUES(@mabaikt,@tenbaikt,@pin,@time,@date,@magv,@malhp,@trangthai)";
            SqlCommand insertcommand = new SqlCommand(sQuery, conn);
            insertcommand.Parameters.AddWithValue("@mabaikt", mabaikt);
            insertcommand.Parameters.AddWithValue("@tenbaikt", newTest.tenBaiKiemTra);
            insertcommand.Parameters.AddWithValue("@pin", newTest.pin);
            insertcommand.Parameters.AddWithValue("@time", thoigian);
            insertcommand.Parameters.AddWithValue("@date", DateTime.Now);
            insertcommand.Parameters.AddWithValue("@magv", newTest.maGiaoVien);
            insertcommand.Parameters.AddWithValue("@malhp", malophp);
            insertcommand.Parameters.AddWithValue("@trangthai", 1);
            int result = insertcommand.ExecuteNonQuery();
            conn.Close();
            return result;

        }
        public  bool kiemtra(string mabaikt)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from BaiKiemTra where MaBaiKT = '{0}'", mabaikt);
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
        public static string layMaBKT()
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string mavd = "";
            string matieptheo = "BKT";
            string sQuery1 = "select top 1 MaBaiKT from BaiKiemTra order by MaBaiKT desc";
            SqlCommand com1 = new SqlCommand(sQuery1, conn);

                SqlDataReader dr1 = com1.ExecuteReader();
                while (dr1.Read())
                {
                    mavd = dr1.GetString(0);
                }
                dr1.Close();
                conn.Close();
                string lucsau = mavd.Substring(3);
                int sott = Convert.ToInt32(lucsau);
                int stt_tieptheo = sott + 1;
                string masott = stt_tieptheo.ToString();
                while (masott.Length < 7)
                {
                    masott = "0" + masott;
                }
                matieptheo += masott;
                
         
            return matieptheo;
        }
    }
}