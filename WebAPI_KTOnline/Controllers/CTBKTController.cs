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
        [HttpGet]
        [Route("api/Danh-sach-cau-hoi-cua-bai-kiem-tra")]
        public IEnumerable<CauHoi> Get_cauhoi(string mabaikiemtra)
        {
            List<CauHoi> listdanhsach = CauHoi.DsachCauhoi_CuaBaiKT(mabaikiemtra);
            return listdanhsach;
        }

        // POST: api/CTBKT
        [Route("api/CTBKT/{mabkt}")]
        public void Post([FromBody]CauHoi[] listdata, [FromUri] string mabkt)
        {
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            int STT = 0;
            for (int i = 0; i < listdata.Length; i++)
            {
                STT = STT +1;
                string mach = listdata[i].maCauHoi;
                if (CTBaiKT.kiemtra(mabkt, mach))
                {
                    CTBaiKT.AddCTBKT(mabkt, mach,STT);
                }
                else
                {
                    //nothing
                }
            }
            conn.Close();
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
