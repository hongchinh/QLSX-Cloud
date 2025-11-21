using QLSX.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;
using System;
using SaleAPI.Interfaces;
using QLSX.Shared.Models.Request;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : Controller
    {
        IConfiguration _configuration;
        private readonly ITenantProvider _tenantProvider;
        public ThongKeController(IConfiguration configuration, ITenantProvider tenantProvider)
        {
            _configuration = configuration;
            _tenantProvider = tenantProvider;
        }
        [HttpGet("thongkedoanhthuthang/{nam}")]
        public async Task<ActionResult<List<ThongKeDoanhThu>>> thongkedoanhthuthang(int nam)
        {
            SqlParameter[] param = {
                new SqlParameter("@mdvsd", _tenantProvider.TenantId),
                    new SqlParameter("@loai", "xuat"),
                    new SqlParameter("@nam", nam)
                };
            string connectionString = _configuration.GetConnectionString("CRMConnectStrings");
            var data = ExcuteSQLStoreProceduce(connectionString, "ThongKeDoanhThu", param);
            var lstResult = new List<ThongKeDoanhThu>();
            foreach (DataRow item in data.Tables[0].Rows)
            {
                ThongKeDoanhThu thongKeDoanhThu = new ThongKeDoanhThu();
                thongKeDoanhThu.Id = int.Parse(item["id"].ToString());
                thongKeDoanhThu.ColumnName = item["ColumnName"].ToString();
                thongKeDoanhThu.Value = double.Parse(item["value"].ToString());
                lstResult.Add(thongKeDoanhThu);
            }
            // var str = JsonConvert.SerializeObject(data);
            return Ok(lstResult);
        }

        [HttpGet("thongkethutien/{nam}")]
        public async Task<ActionResult<List<ThongKeDoanhThu>>> thongkethutien(int nam)
        {
            SqlParameter[] param = {
                new SqlParameter("@mdvsd", _tenantProvider.TenantId),
                    new SqlParameter("@loai", "thu"),
                    new SqlParameter("@nam", nam)
                };
            string connectionString = _configuration.GetConnectionString("CRMConnectStrings");
            var data = ExcuteSQLStoreProceduce(connectionString, "ThongKeThuTien", param);
            var lstResult = new List<ThongKeDoanhThu>();
            foreach (DataRow item in data.Tables[0].Rows)
            {
                ThongKeDoanhThu thongKeDoanhThu = new ThongKeDoanhThu();
                thongKeDoanhThu.Id = int.Parse(item["id"].ToString());
                thongKeDoanhThu.ColumnName = item["ColumnName"].ToString();
                thongKeDoanhThu.Value = double.Parse(item["value"].ToString());
                lstResult.Add(thongKeDoanhThu);
            }
            // var str = JsonConvert.SerializeObject(data);
            return Ok(lstResult);
        }
        [HttpGet("ThongKeDoanhThuTheoNV/{nam}")]
        public async Task<ActionResult<List<ThongKeDoanhThuTheoNV>>> ThongKeDoanhThuTheoNV(int nam)
        {
            SqlParameter[] param = {
                new SqlParameter("@mdvsd", _tenantProvider.TenantId),
                    new SqlParameter("@loai", "xuat"),
                    new SqlParameter("@nam", nam)
                };
            string connectionString = _configuration.GetConnectionString("CRMConnectStrings");
            var data = ExcuteSQLStoreProceduce(connectionString, "ThongKeDoanhThuTheoNhanVien", param);
            var lstResult = new List<ThongKeDoanhThuTheoNV>();
            foreach (DataRow item in data.Tables[0].Rows)
            {
                ThongKeDoanhThuTheoNV thongKeDoanhThu = new ThongKeDoanhThuTheoNV();
                thongKeDoanhThu.Id = int.Parse(item["id"].ToString());
                thongKeDoanhThu.UserName = item["UserName"].ToString();
                thongKeDoanhThu.ColumnName = item["ColumnName"].ToString();
                thongKeDoanhThu.Value = double.Parse(item["value"].ToString());
                lstResult.Add(thongKeDoanhThu);
            }
            return Ok(lstResult);
        }
        [HttpGet("thongkeloaitien/{todate}")]
        public async Task<ActionResult<List<ThongKeDoanhThuTheoNV>>> ThongKeCacLoaiQuy(int todate)
        {
            SqlParameter[] param = {
                new SqlParameter("@mdvsd", _tenantProvider.TenantId),
                    new SqlParameter("@date2", string.Format("{0:MM/dd/yyyy}", DateTime.ParseExact(todate.ToString(),"yyyyMMdd",null)))
                };
            string connectionString = _configuration.GetConnectionString("CRMConnectStrings");
            var data = ExcuteSQLStoreProceduce(connectionString, "ThongKeCacLoaiQuy", param);
            var lstResult = new List<ThongKeLoaiTien>();
            foreach (DataRow item in data.Tables[0].Rows)
            {
                ThongKeLoaiTien thongKeDoanhThu = new ThongKeLoaiTien();
                thongKeDoanhThu.TenLoaiTien = item["TenLoaiTien"].ToString();
                thongKeDoanhThu.SoDuCuoi = double.Parse(item["SoDuCuoi"].ToString());
                lstResult.Add(thongKeDoanhThu);
            }
            return Ok(lstResult);
        }

        [HttpGet("tonghopdongtien")]
        public async Task<ActionResult<List<TongHopDongTienModel>>> TongHopDongTien(TongHopDongTienRequest request)
        {
            SqlParameter[] param = {
                new SqlParameter("@mdvsd", _tenantProvider.TenantId),
                    new SqlParameter("@hien", false),
                    new SqlParameter("@date1", request.date1),
                    new SqlParameter("@date2", request.date2),
                    new SqlParameter("@tmptblOK", "ZZZTEMPABC")
                };
            string connectionString = _configuration.GetConnectionString("CRMConnectStrings");
            var data = ExcuteSQLStoreProceduce(connectionString, "TongHopDongTien", param);
            var lstResult = new List<TongHopDongTienModel>();
            foreach (DataRow item in data.Tables[0].Rows)
            {
                TongHopDongTienModel thongKeDoanhThu = new TongHopDongTienModel();
                thongKeDoanhThu.Stt = int.Parse(item["Stt"].ToString());
                thongKeDoanhThu.TenLoaiTien = item["TENLOAITIEN"].ToString();
                thongKeDoanhThu.SoDuDau = double.Parse((item["SODUDAUKY"] ?? 0).ToString());
                thongKeDoanhThu.SoTienThu = double.Parse((item["SOTIENTHU"] ?? 0).ToString());
                thongKeDoanhThu.SoTienChi = double.Parse((item["SOTIENCHI"] ?? 0).ToString());
                thongKeDoanhThu.SoDuCuoiKy = double.Parse((item["SODUCUOIKY"] ?? 0).ToString());
                lstResult.Add(thongKeDoanhThu);
            }
            return Ok(lstResult);
        }
        private DataSet ExcuteSQLStoreProceduce(string connectstring, string storeName, SqlParameter[] paramters)
        {
            DataSet ds = new DataSet("data");
            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                SqlCommand sqlComm = new SqlCommand(storeName, conn);
                sqlComm.Parameters.AddRange(paramters);


                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }
            return ds;
        }

    }
}
