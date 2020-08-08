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
            int result = 0;
            try
            {
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
                result = insertcommand.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception e)
            {
                //not thing
            }
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
        public static List<BaiKiemTra> DsachBaiKiemTra_theogv(string magiaovien)
        {
            List<BaiKiemTra> list = new List<BaiKiemTra>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("SELECT BKT.MaBaiKT, BKT.TenBaiKT, BKT.KeyBaiKT, BKT.ThoiGianLam, BKT.NgayTao, GV.TenGV, LHP.TenLopHP, BKT.TrangThai ");
            sQuery.Append("FROM BaiKiemTra BKT  ");
            sQuery.Append("INNER JOIN GiangVien GV ON BKT.MaGV = GV.MaGV  ");
            sQuery.Append("INNER JOIN LopHocPhan LHP ON BKT.MaLopHP = LHP.MaLopHP ");
            sQuery.AppendFormat("WHERE BKT.MaGV = '{0}' ",magiaovien);
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                int sophutlam = Convert.ToInt32(dr.GetInt32(3)) / 60;
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
        public static void UPDATE_daKT(string mabaikiemtra, string masinhvien)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            int soLuongDaKiemTra = 0;
            //string sQuery_lenght = string.Format("Select * from CTKetQua where MaBaiKT = '{0}'", mabaikt);
            //SqlCommand comm = new SqlCommand(sQuery_lenght, conn);
            //SqlDataReader dr = comm.ExecuteReader();
            //int soCau = 0;
            //while (dr.Read())
            //{
            //    soCau++;

            //}
            //dr.Close();
            try
            {
                string sQuery = string.Format("UPDATE [KetQua] SET TrangThai = 3 FROM BaiKiemTra where MaBaiKT = '{0}' and MaSV='{1}'", mabaikiemtra, masinhvien);
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