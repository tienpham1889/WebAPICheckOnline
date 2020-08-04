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
    public class ChuDe
    {
        string macd;
        string tencd;
        string mamonhoc;
        string magiaovien;
        int trangthai;
        public static List<ChuDe> ListCD = DsachChuDe();
        public ChuDe()
        {
            macd = "";
            tencd = "";
            mamonhoc = "";
            magiaovien = "";
            trangThai = 0;
        }

        public string maChuDe { get => macd; set => macd = value; }
        public string tenChuDe { get => tencd; set => tencd = value; }
        public string maMonHoc { get => mamonhoc; set => mamonhoc = value; }
        public string maGiaoVien { get => magiaovien; set => magiaovien = value; }
        public int trangThai { get => trangthai; set => trangthai = value; }

        public static List<ChuDe> DsachChuDe()
        {
            List<ChuDe> list = new List<ChuDe>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("select CD.MaCD, CD.TenCD, MH.MaMonHoc,CD.MaGV, CD.TrangThai ");
            sQuery.Append("from ChuDe CD ");
            sQuery.Append("inner join GiangVien GV On CD.MaGV = GV.MaGV ");
            sQuery.Append("INNER JOIN MonHoc MH ON CD.MaMonHoc = MH.MaMonHoc ");
            sQuery.Append("WHERE CD.TrangThai = 1 and MH.TrangThai = 1");
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                ChuDe cd = new ChuDe();
                cd.macd = dr.GetString(0);
                cd.tencd = dr.GetString(1);
                cd.mamonhoc = dr.GetString(2);
                cd.magiaovien = dr.GetString(3);
                cd.trangthai = dr.GetInt32(4);
                list.Add(cd);
            }
            conn.Close();
            return list;
        }
        public ChuDe cd(string machude)
        {
            ChuDe cd = new ChuDe();

            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from ChuDe where MaCD = '{0}'", machude);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                cd.macd = dr.GetString(0);
                cd.tencd = dr.GetString(1);
                cd.mamonhoc = dr.GetString(2);
                cd.trangthai = dr.GetInt32(3);
                cd.magiaovien = dr.GetString(4);

            }
            dr.Close();
            conn.Close();
            return cd;
        }
        public static List<ChuDe> DsachCD_theomonhoc(string mamh)
        {
            List<ChuDe> list = new List<ChuDe>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            //string sQuery = string.Format("select * from ChuDe where MaMonHoc = '{0}'",mamh);
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("select CD.MaCD, CD.TenCD, MH.TenMonHoc,GV.TenGV ");
            sQuery.Append("from ChuDe CD ");
            sQuery.Append("inner join GiangVien GV On CD.MaGV = GV.MaGV ");
            sQuery.Append("INNER JOIN MonHoc MH ON CD.MaMonHoc = MH.MaMonHoc ");
            sQuery.AppendFormat("WHERE CD.TrangThai = 1 AND CD.MaMonHoc = '{0}'",mamh);
            SqlCommand com = new SqlCommand(sQuery.ToString(), conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                ChuDe cd = new ChuDe();
                cd.macd = dr.GetString(0);
                cd.tencd = dr.GetString(1);
                cd.mamonhoc = dr.GetString(2);
                cd.magiaovien = dr.GetString(3);   
                list.Add(cd);
            }
            conn.Close();
            return list;
        }
        public bool kiemtra(string tenchude, string mamonhoc)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from ChuDe where TenCD = N'{0}' and MaMonHoc = '{1}'", tenchude, mamonhoc);
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
        public static string layMaCD()
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string mavd = "";
            string sQuery1 = "select top 1 MaCD from ChuDe order by MaCD desc";
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
            string matieptheo = "CD";
            string masott = stt_tieptheo.ToString();
            while (masott.Length < 6)
            {
                masott = "0" + masott;
            }
            matieptheo += masott;
            return matieptheo;
        }
        public static int UpdateChuDe(ChuDe chude)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            String sQuery = "UPDATE [dbo].[ChuDe] SET [TenCD] = @tencd, [MaMonHoc] = @mamh, [TrangThai]= 1, [MaGV]=@magv WHERE [MaCD] = @macd";
            SqlCommand updatecommand = new SqlCommand(sQuery, conn);
            updatecommand.Parameters.AddWithValue("@tencd", chude.tenChuDe.Trim());
            updatecommand.Parameters.AddWithValue("@mamh", chude.maMonHoc);
            updatecommand.Parameters.AddWithValue("@magv", chude.maGiaoVien.Trim());
            updatecommand.Parameters.AddWithValue("@macd", chude.maChuDe.Trim());
            int result = updatecommand.ExecuteNonQuery();
            conn.Close();
            return result;
        }
        public static int DeleteChuDe(ChuDe chude)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            String sQuery = "UPDATE [dbo].[ChuDe] SET [TrangThai] = @trangthai  WHERE [MaCD] = @macd";
            SqlCommand updatecommand = new SqlCommand(sQuery, conn);
            updatecommand.Parameters.AddWithValue("@trangthai", 2);
            updatecommand.Parameters.AddWithValue("@macd", chude.maChuDe);
            int result = updatecommand.ExecuteNonQuery();
            conn.Close();
            return result;
        }
    }
}