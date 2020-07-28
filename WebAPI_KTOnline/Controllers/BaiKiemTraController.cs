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
    public class BaiKiemTraController : ApiController
    {
        // GET: api/BaiKiemTra
        public IEnumerable<BaiKiemTra> Get()
        {
            List<BaiKiemTra> listdanhsach = BaiKiemTra.DsachBaiKiemTra();
            return listdanhsach;
        }

        // GET: api/BaiKiemTra/5
        public IEnumerable<BaiKiemTra> Get(string mabaikt)
        {

            BaiKiemTra bkt = new BaiKiemTra();
            bkt = bkt.baikt(mabaikt);
            yield return bkt;
        }

        // POST: api/BaiKiemTra
        public BaiKiemTra Post([FromBody]BaiKiemTra Test)
        {
            BaiKiemTra bkt = new BaiKiemTra();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string mabaikt = BaiKiemTra.layMaBKT ();
            string malophp = "";
            string sQuery_getMonHoc = string.Format("SELECT MaLopHP FROM LopHocPhan WHERE TenLopHP = N'{0}'", Test.maLopHocPhan);
            SqlCommand com = new SqlCommand(sQuery_getMonHoc, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                malophp = dr.GetString(0);
            }
            dr.Close();
            if (bkt.kiemtra(mabaikt))
            {
                int result = BaiKiemTra.AddBaiKT(Test, mabaikt, malophp);
                bkt = bkt.baikt(mabaikt);
                if (result > 0)
                {
                    return bkt;
                }
            }
            else
            {
                return bkt;
            }
            conn.Close();
            return bkt;
        }

        // PUT: api/BaiKiemTra/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BaiKiemTra/5
        public void Delete(int id)
        {
        }
    }
}
