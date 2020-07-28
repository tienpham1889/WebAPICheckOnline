using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_KTOnline.Models;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI_KTOnline.Controllers
{
    public class CTBKTController : ApiController
    {
        // GET: api/CTBKT
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CTBKT/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CTBKT
        public List<CTBaiKT> Post([FromBody]CauHoi[] listdata)
        {
            List<CTBaiKT> list = CTBaiKT.Dsach();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            int STT = 0;
            string mabaikt = "";
            string sQuery1 = "select top 1 MaBaiKT from BaiKiemTra order by MaBaiKT desc";
            SqlCommand com1 = new SqlCommand(sQuery1, conn);
            SqlDataReader dr1 = com1.ExecuteReader();
            while (dr1.Read())
            {
                mabaikt = dr1.GetString(0);
            }
            dr1.Close();
            for (int i = 0; i < listdata.Length; i++)
            {
                STT++;
                string mach = listdata[i].maCauHoi;
                if (CTBaiKT.kiemtra(mabaikt, mach))
                {
                    CTBaiKT.AddCTBKT(mabaikt, mach,STT);
                }
                else
                {
                    //nothing
                }
            }
            conn.Close();
            return list;
        }

        // PUT: api/CTBKT/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CTBKT/5
        public void Delete(int id)
        {
        }
    }
}
