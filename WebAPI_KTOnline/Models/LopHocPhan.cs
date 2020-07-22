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
            sQuery.Append("SELECT LHP.MaLopHP, LHP.TenLopHP, GV.TenGV, MH.TenMonHoc, LHP.MaLop, LHP.TrangThai ");
            sQuery.Append("FROM LopHocPhan LHP ");
            sQuery.Append("INNER JOIN GiangVien GV ON LHP.MaGV = GV.MaGV ");
            sQuery.Append("INNER JOIN MonHoc MH ON LHP.MaMonHoc = MH.MaMonHoc ");
            sQuery.Append("WHERE LHP.TrangThai = 1");
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