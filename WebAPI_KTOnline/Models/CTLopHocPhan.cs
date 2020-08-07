using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace WebAPI_KTOnline.Models
{
    public class CTLopHocPhan
    {
        string malophocphan;
        string masinhvien;


        public CTLopHocPhan()
        {
            maLopHocPhan = "";
            maSinhVien = "";
        }

        public string maLopHocPhan { get => malophocphan; set => malophocphan = value; }
        public string maSinhVien { get => masinhvien; set => masinhvien = value; }
        public static List<CTLopHocPhan> Dsach()
        {
            List<CTLopHocPhan> list = new List<CTLopHocPhan>();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = "Select * from CTLopHocPhan";
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                CTLopHocPhan CTLopHocPhan = new CTLopHocPhan();
                CTLopHocPhan.malophocphan = dr.GetString(0);
                CTLopHocPhan.masinhvien = dr.GetString(1);
                list.Add(CTLopHocPhan);
            }
            conn.Close();
            return list;
        }
        public static void AddCTLopHocPhan(string malophocphan, string malop)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            try
            {
                string sQuery = string.Format("insert into CTLopHocPhan (MaLopHP,MaSV) select '{0}',MaSV From SinhVien where Malop ='{1}'", malophocphan, malop);
                SqlCommand insertcommand = new SqlCommand(sQuery, conn);
                int result = insertcommand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                //no thing 
            }
        }
        public static bool kiemtra(string malophocphan, string masinhvien)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("select * from CTLopHocPhan where MaLopHP = '{0}' and MaSV = '{1}'", malophocphan, masinhvien);
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