﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_KTOnline.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WebAPI_KTOnline.Controllers
{
    public class ChuDeController : ApiController
    {
        // GET: api/ChuDe
        public IEnumerable<ChuDe> Get()
        {
            List<ChuDe> listdanhsach = ChuDe.DsachChuDe();
            return listdanhsach;
        }

        // GET: api/ChuDe/5
        public IEnumerable<ChuDe> Get(string machude)
        {

            ChuDe cd = new ChuDe();
            cd = cd.cd(machude);
            yield return cd;
        }
        [HttpGet]
        [Route("api/chude-theomonhoc")]
        public IEnumerable<ChuDe> Get_chude(string mamonhoc)
        {
            List<ChuDe> Dsachtheomonhoc = ChuDe.DsachCD_theomonhoc(mamonhoc);
            return Dsachtheomonhoc;
            
        }
        
        // POST: api/ChuDe
        public ChuDe Post([FromBody]ChuDe chude)
        {
            ChuDe cd = new ChuDe();
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string macd = ChuDe.layMaCD();
            if (cd.kiemtra(chude.tenChuDe, chude.maMonHoc))
            {
                
                String sQuery = "INSERT INTO [dbo].[ChuDe]([MaCD],[TenCD],[MaMonHoc],[TrangThai],[MaGV])VALUES(@macd,@tencd,@mamonhoc,@trangthai,@magv)";
                SqlCommand insert_toppiccommand = new SqlCommand(sQuery, conn);
                insert_toppiccommand.Parameters.AddWithValue("@macd", macd);
                insert_toppiccommand.Parameters.AddWithValue("@tencd",chude.tenChuDe);
                insert_toppiccommand.Parameters.AddWithValue("@mamonhoc", chude.maChuDe);
                insert_toppiccommand.Parameters.AddWithValue("@trangthai", chude.trangThai);
                insert_toppiccommand.Parameters.AddWithValue("@magv", chude.maGiaoVien);
                int result = insert_toppiccommand.ExecuteNonQuery();
                
                cd = cd.cd(macd);
                if (result > 0)
                {
                    return cd;
                }
            }
            
            else
            {
                return cd;
            }
            conn.Close();
            return cd;
        }

        // PUT: api/ChuDe/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ChuDe/5
        public void Delete(int id)
        {
        }
    }
}
