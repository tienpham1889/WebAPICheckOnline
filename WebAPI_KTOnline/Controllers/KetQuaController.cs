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
    public class KetQuaController : ApiController
    {
        // GET: api/KetQua
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/KetQua/5
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet]
        [Route("api/Dsach-Ket-Qua-cua-bai-kiem-tra")]
        public IEnumerable<KetQua> Get_dsach_theoBaiKiemTra(string maBaiKiemTra)
        {
            List<KetQua> listdanhsach = KetQua.DsachKetQua_theobaiKT(maBaiKiemTra);
            return listdanhsach;
        }
        [HttpGet]
        [Route("api/Dsach-Ket-Qua-cua-SV-theo-mon-hoc")]
        public IEnumerable<KetQua> Get_dsach_theoSv(string masinhvien, string mamonhoc)
        {
            List<KetQua> listdanhsach = KetQua.DsachKetQua_theosv(masinhvien, mamonhoc);
            return listdanhsach;
        }
        [HttpGet]
        [Route("api/Dsach-sinh-vien-dang-lam-bai")]
        public IEnumerable<KetQua> Get_dsach_danglam(string mabaikt, string magiaovien)
        {
            List<KetQua> listdanhsach = KetQua.DsachSV_DangKT_theobaikt(mabaikt, magiaovien);
            return listdanhsach;
        }
        [HttpGet]
        [Route("api/Dsach-tat-ca-sinh-vien-dang-lam-bai")]
        public IEnumerable<KetQua> get_all_dsach(string magv)
        {
            List<KetQua> listdanhsach = KetQua.DsachSV_DangKT(magv);
            return listdanhsach;
        }
        // POST: api/KetQua
        public void Post([FromBody]string value)
        {
        }
        [HttpPost]
        [Route("api/Them-KetQua")]
        public KetQua Post([FromUri]string masinhvien, string mabaikiemtra)
        {
            KetQua kq = new KetQua();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            if (kq.kiemtra(masinhvien, mabaikiemtra))
            {
                int result = KetQua.AddKetQua(masinhvien, mabaikiemtra);
                kq = kq.ketQua(masinhvien, mabaikiemtra);
                if (result > 0)
                {
                    return kq;
                }
            }
            else
            {
                return kq; 
            }
            conn.Close();
            return kq;
        }
        [HttpPost]
        [Route("api/Update-diem-trang-thai-kiem-tra")]
        public void Post_diem([FromUri] string mabaiKiemtra, string masinhvien)
        {
            KetQua.UPDATE_Diem(mabaiKiemtra, masinhvien);
            BaiKiemTra.UPDATE_daKT(mabaiKiemtra, masinhvien);
        }

        // PUT: api/KetQua/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/KetQua/5
        public void Delete(int id)
        {
        }
    }
}
