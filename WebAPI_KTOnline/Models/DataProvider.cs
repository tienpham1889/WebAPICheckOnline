using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI_KTOnline.Models
{
    public class DataProvider
    {
        public static SqlConnection Connect()
        {
            string ChuoiKetNoi = @"Data Source=DESKTOP-319D2UA\SQLEXPRESS;Initial Catalog =DBQSProject;Integrated Security = True";
            SqlConnection conn = new SqlConnection(ChuoiKetNoi);
            return conn;
        }
    }
}