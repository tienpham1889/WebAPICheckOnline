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
    public class CTKetQuaController : ApiController
    {
        // GET: api/CTKetQua
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CTKetQua/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CTKetQua
        public void Post([FromBody]string value)
        {
            
        }
        [HttpGet]
        [Route("api/Danh-sach-CT-Ket-Qua")]
        public IEnumerable<CTKetQua> Get_ctkt(string mabaikiemtra, string masinhvien)
        {
            List<CTKetQua> listdanhsach = CTKetQua.Dsach_CTKQ(mabaikiemtra, masinhvien);
            return listdanhsach;
        }
        [HttpPost]
        [Route("api/Them-danh-sach-chi-tiet-ket-qua")]
        public void Post_chitiet([FromUri] string masinhvien, string mabaikt )
        {
             CTKetQua.AddCTKQ(masinhvien, mabaikt);
        }
        [HttpPost]
        [Route("api/Them-dap-an")]
        public void Post_dapan([FromUri] string masinhvien, string mabaikiemtra, string macauhoi, string dapan)
        {
            CTKetQua.UPDATEPhuongAn(masinhvien, mabaikiemtra, macauhoi, dapan);
        }
        
        // PUT: api/CTKetQua/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CTKetQua/5
        public void Delete(int id)
        {
        }
    }
}
