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
        public static List<MonHoc> ListMonHoc = DsachMonHoc();
        public MonHoc()
        {
            Mamonhoc = "";
            Tenmonhoc = "";
            Sotiet = 0;
            Sotinchi = 0;
        }

        public string Mamonhoc { get => mamonhoc; set => mamonhoc = value; }
        public string Tenmonhoc { get => tenmonhoc; set => tenmonhoc = value; }
        public int Sotinchi { get => sotinchi; set => sotinchi = value; }
        public int Sotiet { get => sotiet; set => sotiet = value; }

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
                list.Add(mh);
            }
            conn.Close();
            return list;
        }
    }
}