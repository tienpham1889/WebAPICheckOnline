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
            //test thu r co r fen
            List<CauHoi> list = new List<CauHoi>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            StringBuilder sQuery = new StringBuilder();
            sQuery.Append("SELECT CH.MaCauHoi, MH.TenMonHoc, CD.TenCD, CH.NoiDung, CH.PhuongAnA, CH.PhuongAnB, CH.PhuongAnC, CH.PhuongAnD, CH.DapAn ");
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
                list.Add(ch);
            }
            conn.Close();
            return list;
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
    }
}