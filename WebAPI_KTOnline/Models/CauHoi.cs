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
        
        public CauHoi()
        {
            Macauhoi = "";
            Noidung = "";
            Phuongana = "";
            Phuonganb = "";
            Phuonganc = "";
            Phuongand = "";
            Dapan = "";
            Mamonhoc = "";
            Machude = "";
        }
        public static List<CauHoi> ListCH = DsachCauHoi();

        public string Macauhoi { get => macauhoi; set => macauhoi = value; }
        public string Noidung { get => noidung; set => noidung = value; }
        public string Phuongana { get => phuongana; set => phuongana = value; }
        public string Phuonganb { get => phuonganb; set => phuonganb = value; }
        public string Phuonganc { get => phuonganc; set => phuonganc = value; }
        public string Phuongand { get => phuongand; set => phuongand = value; }
        public string Dapan { get => dapan; set => dapan = value; }
        public string Mamonhoc { get => mamonhoc; set => mamonhoc = value; }
        public string Machude { get => machude; set => machude = value; }
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
    }
}