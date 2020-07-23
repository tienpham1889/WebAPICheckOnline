using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebAPI_KTOnline.Models;

namespace WebAPI_KTOnline.Models
{
    public class MonHoc
    {
        string mamonhoc;
        string tenmonhoc;
        int sotinchi;
        int sotiet;
        int trangthai;
        public static List<MonHoc> ListMonHoc = DsachMonHoc();
        public MonHoc()
        {
            mamonhoc = "";
            tenmonhoc = "";
            sotiet = 0;
            sotinchi = 0;
            trangthai = 0;
        }

        public string maMonHoc { get => mamonhoc; set => mamonhoc = value; }
        public string tenMonhoc { get => tenmonhoc; set => tenmonhoc = value; }
        public int soTinchi { get => sotinchi; set => sotinchi = value; }
        public int soTiet { get => sotiet; set => sotiet = value; }
        public int trangThai { get => trangthai; set => trangthai = value; }

        public static List<MonHoc> DsachMonHoc()
        {
            List<MonHoc> list = new List<MonHoc>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = "Select * from MonHoc where TrangThai = 1";
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                MonHoc mh = new MonHoc();
                mh.mamonhoc = dr.GetString(0);
                mh.tenmonhoc = dr.GetString(1);
                mh.sotinchi = dr.GetInt32(2);
                mh.sotiet = dr.GetInt32(3);
                list.Add(mh);
            }
            conn.Close();
            return list;
        }
        public static string layMaMH()
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string mavd = "";
            string sQuery1 = "select top 1 MaMonHoc from MonHoc order by MaMonHoc desc";
            SqlCommand com1 = new SqlCommand(sQuery1, conn);
            SqlDataReader dr1 = com1.ExecuteReader();
            while (dr1.Read())
            {
                mavd = dr1.GetString(0);
            }
            string lucsau = mavd.Substring(2);
            int sott = Convert.ToInt32(lucsau);
            int stt_tieptheo = sott + 1;
            string matieptheo = "MH";
            string masott = stt_tieptheo.ToString();
            while(masott.Length < 6)
            {
                masott = "0" + masott;
            }
            matieptheo += masott;
            return matieptheo;
        }
        public MonHoc mh(string mamh)
        {
            MonHoc mh = new MonHoc();

            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from MonHoc where MaMonHoc = '{0}' and TrangThai = 1 ", mamh);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                mh.mamonhoc = dr.GetString(0);
                mh.tenmonhoc = dr.GetString(1);
                mh.sotinchi = dr.GetInt32(2);
                mh.sotiet = dr.GetInt32(3);
            }
            conn.Close();
            return mh;
        }
        public bool kiemtra(string tenmh)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from MonHoc where TenMonHoc = N'{0}' and TrangThai = 1", tenmh);
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
        public static MonHoc MonhoctheoLopHP(string mamh)
        {
            MonHoc mh = new MonHoc();

            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from MonHoc where TrangThai = 1 and MaMonHoc = '{0}'", mamh);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                mh.mamonhoc = dr.GetString(0);
                mh.tenmonhoc = dr.GetString(1);
                mh.sotinchi = dr.GetInt32(2);
                mh.sotiet = dr.GetInt32(3);
            }
            conn.Close();
            return mh;
        }
    }
}