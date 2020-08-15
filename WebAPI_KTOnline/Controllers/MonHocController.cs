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
    public class MonHocController : ApiController
    {
        // GET: api/MonHoc
        public IEnumerable<MonHoc> Get()
        {
            List<MonHoc> listdanhsach = MonHoc.DsachMonHoc();
            //string ma = MonHoc.layma();
            return listdanhsach;
        }
        [HttpGet]
        [Route("api/Dsach-Mon-Hoc-cua-GV")]
        public IEnumerable<MonHoc> Get_dsach_theogv(string magv)
        {
            List<MonHoc> listdanhsach = MonHoc.DsachMonHoc_theogiaovien(magv);
            return listdanhsach;
        }
        [HttpGet]
        [Route("api/Dsach-Mon-Hoc-cua-SV")]
        public IEnumerable<MonHoc> Get_dsach_theosv(string maSinhVien)
        {
            List<MonHoc> listdanhsach = MonHoc.DsachMonHoc_cuasinhvien(maSinhVien);
            return listdanhsach;
        }
        [HttpGet]
        [Route("api/Dsach-Mon-Hoc-thuoc-chu-de-GV-tao")]
        public IEnumerable<MonHoc> Get_dsach_theocd(string magv)
        {
            List<MonHoc> listdanhsach = MonHoc.DsachMonHoc_cuachude_theogv(magv);
            return listdanhsach;
        }
        // GET: api/MonHoc/5
        public string Get(int id)
        {
            return "value";
        }
        // POST: api/MonHoc
        public MonHoc Post([FromBody]MonHoc monhoc)
        {
            MonHoc mh = new MonHoc();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string mamonhoc = MonHoc.layMaMH();
            //test thu branch
            if (mh.kiemtra(monhoc.tenMonhoc))
            {
                String sQuery = "INSERT INTO [dbo].[MonHoc]([MaMonHoc],[TenMonHoc],[SoTinChi],[SoTiet],[TrangThai])VALUES(@manh,@tenmh,@sotinchi,@sotiet,@trangthai)";
                SqlCommand insertcommand = new SqlCommand(sQuery, conn);
                insertcommand.Parameters.AddWithValue("@manh", mamonhoc);
                insertcommand.Parameters.AddWithValue("@tenmh", monhoc.tenMonhoc);
                insertcommand.Parameters.AddWithValue("@sotinchi", monhoc.soTinChi);
                insertcommand.Parameters.AddWithValue("@sotiet", monhoc.soTiet);
                insertcommand.Parameters.AddWithValue("@trangthai", monhoc.trangThai);
                int result = insertcommand.ExecuteNonQuery();
                conn.Close();
                mh = mh.mh(mamonhoc);
                if (result > 0)
                {
                    return mh;
                }
            }
            else
            {
                return mh;
            }
            return mh;
        }
        [Route("api/update-mon-hoc")]
        [HttpPost]
        public MonHoc Postupdate([FromBody]MonHoc monhoc)
        {
            MonHoc mh = new MonHoc();
            int result = MonHoc.UpdateMonHoc(monhoc);
            mh = mh.mh(monhoc.maMonHoc);
            if (result > 0)
            {
                return mh;
            }
            return mh;
        }
        [Route("api/delete-mon-hoc")]
        [HttpPost]
        public MonHoc Postdelete([FromBody]MonHoc monhoc)
        {
            MonHoc monHoc = new MonHoc();
            int result = MonHoc.DeleteMonHoc(monhoc);
            monHoc = monHoc.mh(monhoc.maMonHoc);
            if (result > 0)
            {
                return monHoc;
            }
            return monHoc;
        }

        // PUT: api/MonHoc/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MonHoc/5
        public void Delete(int id)
        {
        }
    }
}
