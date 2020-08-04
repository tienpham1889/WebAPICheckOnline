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
    public class CauHoi
    {
        string macauhoi;
        string noidung;
        string phuongana;
        string phuonganb;
        string phuonganc;
        string phuongand;
        string dapan;
        string mamonhoc;
        string machude;
        int trangthai;
        int Value;
        
        public CauHoi()
        {
            maCauHoi = "";
            noiDung = "";
            phuongAnA = "";
            phuongAnB = "";
            phuongAnC = "";
            phuongAnD = "";
            dapAn = "";
            maMonHoc = "";
            maChuDe = "";
            trangThai = 0;
            Value = 0;
        }
        public static List<CauHoi> ListCH = DsachCauHoi();

        public string maCauHoi { get => macauhoi; set => macauhoi = value; }
        public string noiDung { get => noidung; set => noidung = value; }
        public string phuongAnA { get => phuongana; set => phuongana = value; }
        public string phuongAnB { get => phuonganb; set => phuonganb = value; }
        public string phuongAnC { get => phuonganc; set => phuonganc = value; }
        public string phuongAnD { get => phuongand; set => phuongand = value; }
        public string dapAn { get => dapan; set => dapan = value; }
        public string maMonHoc { get => mamonhoc; set => mamonhoc = value; }
        public string maChuDe { get => machude; set => machude = value; }
        public int trangThai { get => trangthai; set => trangthai = value; }
        public int value { get => Value; set => Value = value; }
        public static List<CauHoi> DsachCauHoi()
        {
            // test xem co chưa
            List<CauHoi> list = new List<CauHoi>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("SELECT CH.MaCauHoi, CH.MaMonHoc, CH.MaCD, CH.NoiDung, CH.PhuongAnA, CH.PhuongAnB, CH.PhuongAnC, CH.PhuongAnD, CH.DapAn, CH.TrangThai ");
            sQuery.Append("FROM CauHoi CH ");
            sQuery.Append("INNER JOIN MonHoc MH ON CH.MaMonHoc = MH.MaMonHoc ");
            sQuery.Append("INNER JOIN ChuDe CD ON CH.MaCD = CD.MaCD ");
            sQuery.Append("WHERE CH.TrangThai = 1");
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                CauHoi ch = new CauHoi();
                ch.macauhoi = dr.GetString(0);
                ch.mamonhoc = dr.GetString(1);
                ch.machude = dr.GetString(2);
                ch.noidung = dr.GetString(3);
                ch.phuongana = dr.GetString(4);
                ch.phuonganb = dr.GetString(5);
                ch.phuonganc = dr.GetString(6);
                ch.phuongand = dr.GetString(7);
                ch.dapan = dr.GetString(8);
                ch.trangthai = dr.GetInt32(9);
                list.Add(ch);
            }
            conn.Close();
            return list;
        }
        public CauHoi cauhoi(string macauhoi)
        {
            CauHoi ch = new CauHoi();

            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from CauHoi where MaCauHoi = '{0}'", macauhoi);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                ch.macauhoi = dr.GetString(0);
                ch.noidung = dr.GetString(1);
                ch.phuongana = dr.GetString(2);
                ch.phuonganb = dr.GetString(3);
                ch.phuonganc = dr.GetString(4);
                ch.phuongand = dr.GetString(5);
                ch.dapan = dr.GetString(6);
                ch.machude = dr.GetString(7);
                ch.trangthai = dr.GetInt32(8);
                ch.mamonhoc = dr.GetString(9);
            }
            dr.Close();
            conn.Close();
            return ch;
        }
        public static List<CauHoi> DsachCauhoi_theodieukien(string mamh, string macd)
        {
            List<CauHoi> list = new List<CauHoi>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from CauHoi where MaMonHoc = '{0}' and MaCD = '{1}'", mamh, macd);
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            int stt = 0;
            while (dr.Read())
            {
                CauHoi ch = new CauHoi();
                stt++;
                ch.macauhoi = dr.GetString(0);
                ch.noidung = dr.GetString(1);
                ch.phuongana = dr.GetString(2);
                ch.phuonganb = dr.GetString(3);
                ch.phuonganc = dr.GetString(4);
                ch.phuongand = dr.GetString(5);
                ch.dapan = dr.GetString(6);
                ch.machude = dr.GetString(7);
                ch.trangthai = dr.GetInt32(8);
                ch.mamonhoc = dr.GetString(9);
                ch.Value = stt;
                list.Add(ch);
            }
            conn.Close();
            return list;
        }
        public static List<CauHoi> DsachCauhoi_DangKT(string mabaikt)
        {
            List<CauHoi> list = new List<CauHoi>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("select CT.STT, CH.NoiDung, CH.PhuongAnA, CH.PhuongAnB, CH.PhuongAnC, CH.PhuongAn ");
            sQuery.Append("from CauHoi CH ");
            sQuery.Append("INNER JOIN CTBaiKT CT ON CT.MaCauHoi = CH.MaCauHoi ");
            sQuery.Append("inner join BaiKiemTra BKT ON CT.MaBaiKT = BKT.MaBaiKT ");
            sQuery.AppendFormat("WHERE BKT.MaBaiKT='{0}' AND BKT.TrangThai = 2 ", mabaikt);
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            int stt = 0;
            while (dr.Read())
            {
                CauHoi ch = new CauHoi();
                ch.noidung = dr.GetString(1);
                ch.phuongana = dr.GetString(2);
                ch.phuonganb = dr.GetString(3);
                ch.phuonganc = dr.GetString(4);
                ch.phuongand = dr.GetString(5);
                ch.Value = dr.GetInt32(0);
                list.Add(ch);
            }
            conn.Close();
            return list;
        }
        public bool kiemtra(string noidung, string mamonhoc)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from CauHoi where NoiDung = N'{0}' and MaMonHoc = '{1}'", noidung, mamonhoc);
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
        public static string layMaCauHoi()
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string mavd = "";
            string sQuery1 = "select top 1 MaCauHoi from CauHoi order by MaCauHoi desc";
            SqlCommand com1 = new SqlCommand(sQuery1, conn);
            SqlDataReader dr1 = com1.ExecuteReader();
            while (dr1.Read())
            {
                mavd = dr1.GetString(0);
            }
            dr1.Close();
            conn.Close();
            string lucsau = mavd.Substring(2);
            int sott = Convert.ToInt32(lucsau);
            int stt_tieptheo = sott + 1;
            string matieptheo = "CH";
            string masott = stt_tieptheo.ToString();
            while (masott.Length < 8)
            {
                masott = "0" + masott;
            }
            matieptheo += masott;
            return matieptheo;
        }
        public static int UpdateCauHoi(CauHoi cauhoi)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            String sQuery = "UPDATE [dbo].[CauHoi] SET [NoiDung] = @noidung, [PhuongAnA] = @phuongana, [PhuongAnB]=@phuonganb, [PhuongAnC]=@phuonganc, [PhuongAnD] =@phuongand, [DapAn] = @dapan, [MaCD] = @machude, [MaMonHoc] = @mamonhoc, [TrangThai] = 1 WHERE [MaCauHoi] = @mach";
            SqlCommand updatecommand = new SqlCommand(sQuery, conn);
            updatecommand.Parameters.AddWithValue("@mach", cauhoi.maCauHoi);
            updatecommand.Parameters.AddWithValue("@noidung", cauhoi.noiDung);
            updatecommand.Parameters.AddWithValue("@phuongana", cauhoi.phuongAnA);
            updatecommand.Parameters.AddWithValue("@phuonganb", cauhoi.phuongAnB);
            updatecommand.Parameters.AddWithValue("@phuonganc", cauhoi.phuongAnC);
            updatecommand.Parameters.AddWithValue("@phuongand", cauhoi.phuongAnD);
            updatecommand.Parameters.AddWithValue("@dapan", cauhoi.dapAn.ToUpper());
            updatecommand.Parameters.AddWithValue("@machude", cauhoi.maChuDe);
            updatecommand.Parameters.AddWithValue("@mamonhoc", cauhoi.maMonHoc);
            int result = updatecommand.ExecuteNonQuery();
            conn.Close();
            return result;
        }
        public static int DeleteCauHoi(CauHoi cauhoi)
        {
            SqlConnection conn = DataProvider.Connect();
            //test chu de
            conn.Open();
            String sQuery = "UPDATE [dbo].[ChuDe] SET [TrangThai] = @trangthai  WHERE [MaCauHoi] = @mach";
            SqlCommand updatecommand = new SqlCommand(sQuery, conn);
            updatecommand.Parameters.AddWithValue("@trangthai", 2);
            updatecommand.Parameters.AddWithValue("@mach", cauhoi.macauhoi);
            int result = updatecommand.ExecuteNonQuery();
            conn.Close();
            return result;
        }
    }
}