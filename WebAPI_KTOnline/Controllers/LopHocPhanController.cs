﻿using System;
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
    public class LopHocPhanController : ApiController
    {
        // GET: api/LopHocPhan
        public IEnumerable<LopHocPhan> Get()
        {
            List<LopHocPhan> listdanhsach = LopHocPhan.DsachLop();
            return listdanhsach;
        }
        [HttpGet]
        [Route("api/LopHocPhan-TheoMonhoc")]
        public IEnumerable<LopHocPhan> Get_lophocphan(string tenmh)
        {
            string mamh = "";
            SqlConnection conn = DataProvider.Connect();
            conn.Open();
            string sQuery = string.Format("SELECT MaMonHoc FROM MonHoc WHERE TenMonHoc = N'{0}'", tenmh);
            SqlCommand com = new SqlCommand(sQuery, conn);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                mamh = dr.GetString(0);
            }
            conn.Close();
            List<LopHocPhan> listdanhsach = LopHocPhan.DsachLHP_Theomonhoc(mamh);
            return listdanhsach;
        }
        // GET: api/LopHocPhan/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LopHocPhan
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LopHocPhan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LopHocPhan/5
        public void Delete(int id)
        {
        }
    }
}
