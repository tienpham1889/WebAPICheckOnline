using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WebAPI_KTOnline.Models
{
    public class LopHocPhan
    {
        string MalopHP;
        string TenlopHP;
        string Magv;
        string Mamonhoc;
        string Malop;
        int Trangthai;

        public LopHocPhan()
        {
            MalopHP = "";
            TenlopHP = "";
            Magv = "";
            Mamonhoc = "";
            Malop = "";
            Trangthai = 0;
        }

        public string maLopHocPhan { get => MalopHP; set => MalopHP = value; }
        public string tenLopHocPhan { get => TenlopHP; set => TenlopHP = value; }
        public string maGiaoVien { get => Magv; set => Magv = value; }
        public string maMonHoc { get => Mamonhoc; set => Mamonhoc = value; }
        public string maLop { get => Malop; set => Malop = value; }
        public int trangThai { get => Trangthai; set => Trangthai = value; }
        public static List<LopHocPhan> DsachLop()
        {
            List<LopHocPhan> list = new List<LopHocPhan>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("SELECT LHP.MaLopHP, LHP.TenLopHP, GV.MaGV, MH.MaMonHoc, LHP.MaLop, LHP.TrangThai ");
            sQuery.Append("FROM LopHocPhan LHP ");
            sQuery.Append("INNER JOIN GiangVien GV ON LHP.MaGV = GV.MaGV ");
            sQuery.Append("INNER JOIN MonHoc MH ON LHP.MaMonHoc = MH.MaMonHoc ");
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                LopHocPhan lophp = new LopHocPhan();
                lophp.MalopHP = dr.GetString(0);
                lophp.TenlopHP = dr.GetString(1);
                lophp.Magv = dr.GetString(2);
                lophp.Mamonhoc = dr.GetString(3);
                lophp.Malop = dr.GetString(4);
                lophp.Trangthai = dr.GetInt32(5);
                list.Add(lophp);
            }
            conn.Close();
            return list;
        }
        public LopHocPhan lhp(string malhp)
        {
            LopHocPhan lophp = new LopHocPhan();

            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from LopHocPhan where MaLopHP = '{0}'", malhp);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                lophp.MalopHP = dr.GetString(0);
                lophp.TenlopHP = dr.GetString(1);
                lophp.Magv = dr.GetString(2);
                lophp.Mamonhoc = dr.GetString(3);
                lophp.Malop = dr.GetString(4);
                lophp.Trangthai = dr.GetInt32(5);
            }
            dr.Close();
            conn.Close();
            return lophp;
        }
        public static List<LopHocPhan> DsachLHP_Theomonhoc(string mamh)
        {
            List<LopHocPhan> list = new List<LopHocPhan>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from LopHocPhan where MaMonHoc = '{0}'",mamh);
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                LopHocPhan lhp = new LopHocPhan();
                lhp.MalopHP = dr.GetString(0);
                lhp.TenlopHP = dr.GetString(1);
                lhp.Magv = dr.GetString(2);
                lhp.Mamonhoc = dr.GetString(3);
                lhp.Malop = dr.GetString(4);
                lhp.Trangthai = dr.GetInt32(5);
                list.Add(lhp);
            }
            conn.Close();
            return list;
        }
        public bool kiemtra(string tenlophp)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from LopHocPhan where TenLopHP = N'{0}' ", tenlophp);
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
        public static string layLopHocPhan()
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string mavd = "";
            string sQuery1 = "select top 1 MaLopHP from LopHocPhan order by MaLopHP desc";
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
            string matieptheo = "LHP";
            string masott = stt_tieptheo.ToString();
            while (masott.Length < 6)
            {
                masott = "0" + masott;
            }
            matieptheo += masott;
            return matieptheo;
        }
        public static List<LopHocPhan> DsachLopHP_theolop(string malop)
        {
            List<LopHocPhan> list = new List<LopHocPhan>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("SELECT LHP.MaLopHP, LHP.TenLopHP, GV.TenGV, MH.TenMonHoc, LHP.MaLop, LHP.TrangThai ");
            sQuery.Append("FROM LopHocPhan LHP ");
            sQuery.Append("INNER JOIN GiangVien GV ON LHP.MaGV = GV.MaGV ");
            sQuery.Append("INNER JOIN MonHoc MH ON LHP.MaMonHoc = MH.MaMonHoc ");
            sQuery.AppendFormat("WHERE LHP.TrangThai = 1 AND MaLop = '{0}'",malop);
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                LopHocPhan lophp = new LopHocPhan();
                lophp.MalopHP = dr.GetString(0);
                lophp.TenlopHP = dr.GetString(1);
                lophp.Magv = dr.GetString(2);
                lophp.Mamonhoc = dr.GetString(3);
                lophp.Malop = dr.GetString(4);
                lophp.Trangthai = dr.GetInt32(5);
                list.Add(lophp);
            }
            conn.Close();
            return list;
        }
    }
}