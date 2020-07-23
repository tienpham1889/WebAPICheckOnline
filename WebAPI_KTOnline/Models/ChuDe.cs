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
            sQuery.Append("select CD.MaCD, CD.TenCD, MH.TenMonHoc,GV.TenGV ");
            sQuery.Append("from ChuDe CD ");
            sQuery.Append("inner join GiangVien GV On CD.MaGV = GV.MaGV ");
            sQuery.Append("INNER JOIN MonHoc MH ON CD.MaMonHoc = MH.MaMonHoc ");
            sQuery.Append("WHERE CD.TrangThai = 1");
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
            conn.Close();
            return cd;
        }
        public bool kiemtra(string machude)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from ChuDe where MaCD = '{0}'", machude);
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
    }
}