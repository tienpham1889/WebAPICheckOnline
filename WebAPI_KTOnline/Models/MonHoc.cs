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
        public int soTinChi { get => sotinchi; set => sotinchi = value; }
        public int soTiet { get => sotiet; set => sotiet = value; }
        public int trangThai { get => trangthai; set => trangthai = value; }

        public static List<MonHoc> DsachMonHoc()
        {
            List<MonHoc> list = new List<MonHoc>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = "Select * from MonHoc";
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                MonHoc mh = new MonHoc();
                mh.mamonhoc = dr.GetString(0);
                mh.tenmonhoc = dr.GetString(1);
                mh.sotinchi = dr.GetInt32(2);
                mh.sotiet = dr.GetInt32(3);
                mh.trangthai = dr.GetInt32(4);
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
            string sQuery = string.Format("select * from MonHoc where MaMonHoc = '{0}'", mamh);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                mh.mamonhoc = dr.GetString(0);
                mh.tenmonhoc = dr.GetString(1);
                mh.sotinchi = dr.GetInt32(2);
                mh.sotiet = dr.GetInt32(3);
                mh.trangthai = dr.GetInt32(4);
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
        public static List<MonHoc> DsachMonHoc_theogiaovien(string magiaovien)
        {
            List<MonHoc> list = new List<MonHoc>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("SELECT MH.MaMonHoc, MH.TenMonHoc, MH.SoTiet, MH.SoTinChi ");
            sQuery.Append("FROM MonHoc MH  ");
            sQuery.Append("INNER JOIN LopHocPhan LHP ON MH.MaMonHoc = LHP.MaMonHoc  ");
            sQuery.AppendFormat("WHERE LHP.MaGV = '{0}' AND MH.TrangThai = 1 ", magiaovien);
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                MonHoc mh = new MonHoc();
                mh.mamonhoc = dr.GetString(0);
                mh.tenmonhoc = dr.GetString(1);
                mh.sotiet = dr.GetInt32(2);
                mh.sotinchi = dr.GetInt32(3);
                list.Add(mh);
            }
            conn.Close();
            return list;
        }
        public static int UpdateMonHoc(MonHoc monhoc)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            String sQuery = "UPDATE [dbo].[MonHoc] SET [TenMonHoc] = @tenmonhoc, [SoTinChi] = @sotinchi, [SoTiet]=@sotiet, [TrangThai] = 1 WHERE [MaMonHoc] = @mamonhoc";
            SqlCommand updatecommand = new SqlCommand(sQuery, conn);
            updatecommand.Parameters.AddWithValue("@tenmonhoc", monhoc.tenMonhoc.Trim());
            updatecommand.Parameters.AddWithValue("@sotinchi", monhoc.soTinChi);
            updatecommand.Parameters.AddWithValue("@sotiet", monhoc.soTiet);
            updatecommand.Parameters.AddWithValue("@mamonhoc", monhoc.maMonHoc);
            int result = updatecommand.ExecuteNonQuery();
            conn.Close();
            return result;
        }
        public static int DeleteMonHoc(MonHoc monhoc)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            String sQuery = "UPDATE [dbo].[MonHoc] SET [TrangThai] = @trangthai  WHERE [MaMonHoc] = @mamonhoc";
            SqlCommand updatecommand = new SqlCommand(sQuery, conn);
            updatecommand.Parameters.AddWithValue("@trangthai", 2);
            updatecommand.Parameters.AddWithValue("@mamonhoc", monhoc.maMonHoc);
            int result = updatecommand.ExecuteNonQuery();
            conn.Close();
            return result;
        }
    }
}