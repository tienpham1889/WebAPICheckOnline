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
        [HttpGet]
        [Route("api/ListCauHoi-DangKT")]
        public IEnumerable<CauHoi> Get_cauhoitheodieukien(string mabaikt)
        {
            List<CauHoi> Dsachcauhoi = CauHoi.DsachCauhoi_DangKT(mabaikt);
            return Dsachcauhoi;
        }

        // GET: api/BaiKiemTra/5
        public IEnumerable<BaiKiemTra> Get(string mabaikt)
        {

            BaiKiemTra bkt = new BaiKiemTra();
            bkt = bkt.baikt(mabaikt);
            int trangThai = bkt.trangThai;
            yield return bkt;
        }
        [HttpPost]
        [Route("api/Post-BaiKiemTra")]
        public BaiKiemTra Postupdate_start([FromBody]BaiKiemTra baikiemtra)
        {
            BaiKiemTra baikt = new BaiKiemTra();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            if (!baikt.kiemtra(baikiemtra.pin))
            {
                String sQuery = "UPDATE [dbo].[BaiKiemTra] SET [TrangThai] = @tranthai WHERE [MaBaiKT] = @mabaikt";
                SqlCommand updatecommand = new SqlCommand(sQuery, conn);
                updatecommand.Parameters.AddWithValue("@tranthai", 2);
                updatecommand.Parameters.AddWithValue("@mabaikt", baikiemtra.pin);
                int result = updatecommand.ExecuteNonQuery();
                conn.Close();
                baikt = baikt.baikt(baikiemtra.pin);
                if (result > 0)
                {
                    return baikt;
                }
            }
            else
            {
                return baikt;
            }
            return baikt;
        }

        // POST: api/BaiKiemTra
        public BaiKiemTra Post([FromBody]BaiKiemTra Test)
        {
            BaiKiemTra bkt = new BaiKiemTra();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            if (bkt.kiemtra(Test.pin))
            {
                int result = BaiKiemTra.AddBaiKT(Test, Test.pin, Test.maLopHocPhan);
                bkt = bkt.baikt(Test.pin);
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
