using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI_KTOnline.Models
{
    public class CTBaiKT
    {
        string mabaikt;
        string macauhoi;
        int STT;
        public CTBaiKT()
        {
            mabaikt = "";
            macauhoi = "";
            STT = 0;
        }

        public string maBaiKiemTra { get => mabaikt; set => mabaikt = value; }
        public string maCauHoi { get => macauhoi; set => macauhoi = value; }
        public int stt { get => stt; set => stt = value; }
        public static List<CTBaiKT> Dsach()
        {
            List<CTBaiKT> list = new List<CTBaiKT>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = "Select * from CTBaiKT";
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                CTBaiKT CTBKT = new CTBaiKT();
                CTBKT.mabaikt = dr.GetString(0);
                CTBKT.macauhoi = dr.GetString(1);
                CTBKT.STT = dr.GetInt32(2);
                list.Add(CTBKT);
            }
            conn.Close();
            return list;
        }

        public static void AddCTBKT(string mabatkt, string macauhoi, int stt)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            try
            {
                String sQuery = "INSERT INTO [dbo].[CTBaiKT]([MaBaiKT],[MaCauHoi],[STT])VALUES(@mabaikt,@mach,@stt)";
                SqlCommand insertcommand = new SqlCommand(sQuery, conn);
                insertcommand.Parameters.AddWithValue("@mabaikt", mabatkt);
                insertcommand.Parameters.AddWithValue("@mach", macauhoi);
                insertcommand.Parameters.AddWithValue("@stt", stt);
                int result=insertcommand.ExecuteNonQuery();
                conn.Close();
            }catch(Exception e)
            {
                //no thing 
            }
            
        }
        public static bool kiemtra(string mabaikt, string mach)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from CTBaiKT where MaBaiKT = '{0}' and MaCauHoi = '{1}'", mabaikt, mach);
            SqlCommand comm = new SqlCommand(sQuery, conn);
            SqlDataReader dr = comm.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                count++;

            }
            dr.Close();
            conn.Close();
            if (count > 0)
            {
                return false;
            }
            return true;
        }
    }
}