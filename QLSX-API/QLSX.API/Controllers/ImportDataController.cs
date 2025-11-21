using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using QLSX.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using SaleAPI.Interfaces;
using SaleAPI.Services;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Data;
using System.IO;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml.DataValidation;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImportDataController : ControllerBase
    {
        private readonly CRMDBContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly INhatKyService _nhatKyService;
        private readonly IWebHostEnvironment _env;
        public Microsoft.Extensions.Configuration.IConfiguration _configuration { get; }

        public ImportDataController(CRMDBContext context, ITenantProvider tenantProvider,
            INhatKyService nhatKyService,
            IWebHostEnvironment env,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _nhatKyService = nhatKyService;
            _env = env;
            _configuration = configuration;
        }
        // GET: api/GetCustomerTypes
        [HttpGet("GetColumn/{tablename}")]
        public async Task<ActionResult<List<InformationClumns>>> GetColumn(string tablename)
        {
            string sql = $"select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='{tablename}' order by ORDINAL_POSITION";

            var lst = await _context.InformationClumnss.FromSqlRaw<InformationClumns>(sql).ToListAsync();

            return lst;
        }

        // GET: api/GetCustomerTypes
        [HttpPost("ImportData/{tablename}/{filename}")]
        public async Task<ActionResult<DataTable>> ImportData(string tablename, string filename)
        {
            var path = $"{_env.WebRootPath}\\{filename}";
            DataTable dt = new DataTable();
            var intColumns = await GetColumn(tablename);
            string sqlConn = _configuration.GetConnectionString("CRMConnectStrings");
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM " + tablename + " where 1<0", sqlConn))
            {
                // create the DataSet 
                DataSet dataSet = new DataSet();
                // fill the DataSet using our DataAdapter 
                dataAdapter.Fill(dataSet);
                dt = dataSet.Tables[0];
            }
            //var type = dt.Columns["NAM"].DataType
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var pck = new ExcelPackage())
            {
                ExcelWorksheet worksheet = pck.Workbook.Worksheets[0];

                //check if the worksheet is completely empty
                if (worksheet.Dimension == null)
                {
                    return dt;
                }

                //create a list to hold the column names
                List<string> columnNames = new List<string>();

                //needed to keep track of empty column headers
                int currentColumn = 1;

                //loop all columns in the sheet and add them to the datatable
                //foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                //{
                //    string columnName = cell.Text.Trim();

                //    //check if the previous header was empty and add it if it was
                //    if (cell.Start.Column != currentColumn)
                //    {
                //        columnNames.Add("Header_" + currentColumn);
                //        dt.Columns.Add("Header_" + currentColumn);
                //        currentColumn++;
                //    }

                //    //add the column name to the list to count the duplicates
                //    columnNames.Add(columnName);

                //    //count the duplicate column names and make them unique to avoid the exception
                //    //A column named 'Name' already belongs to this DataTable
                //    int occurrences = columnNames.Count(x => x.Equals(columnName));
                //    if (occurrences > 1)
                //    {
                //        columnName = columnName + "_" + occurrences;
                //    }

                //    //add the column to the datatable
                //    dt.Columns.Add(columnName);

                //    currentColumn++;
                //}

                //start adding the contents of the excel file to the datatable
                for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                {
                    var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                    DataRow newRow = dt.NewRow();

                    //loop all cells in the row
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }

                    dt.Rows.Add(newRow);
                }

            }

            return dt;
        }

        [HttpPost("UploadFile/{tableName}")]
        public DataTable UploadFile(UploadedFile uploadedFile, string tableName)
        {
            var path = $"{_env.WebRootPath}\\ExcelFiles\\{uploadedFile.FileName}";

            DataTable dt = new DataTable(tableName);
            var fs = System.IO.File.Create(path);
            fs.Write(uploadedFile.FileContent, 0, uploadedFile.FileContent.Length);
            fs.Close();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var pck = new ExcelPackage(path))
            {
                ExcelWorksheet worksheet = pck.Workbook.Worksheets[0];

                //check if the worksheet is completely empty
                if (worksheet.Dimension == null)
                {
                    return dt;
                }

                //create a list to hold the column names
                List<string> columnNames = new List<string>();

                //needed to keep track of empty column headers
                int currentColumn = 1;

                //loop all columns in the sheet and add them to the datatable
                foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    string columnName = cell.Text.Trim();
                    if (string.IsNullOrEmpty(columnName.Trim())) break;
                    //check if the previous header was empty and add it if it was
                    if (cell.Start.Column != currentColumn)
                    {
                        columnNames.Add("Header_" + currentColumn);
                        dt.Columns.Add("Header_" + currentColumn);
                        currentColumn++;
                    }

                    //add the column name to the list to count the duplicates
                    columnNames.Add(columnName);

                    //count the duplicate column names and make them unique to avoid the exception
                    //A column named 'Name' already belongs to this DataTable
                    int occurrences = columnNames.Count(x => x.Equals(columnName));
                    if (occurrences > 1)
                    {
                        columnName = columnName + "_" + occurrences;
                    }

                    //add the column to the datatable
                    dt.Columns.Add(columnName);

                    currentColumn++;
                }

                //start adding the contents of the excel file to the datatable
                for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                {
                    var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                    DataRow newRow = dt.NewRow();

                    //loop all cells in the row
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }

                    dt.Rows.Add(newRow);
                }

            }

            return dt;
        }

        [HttpPost("InsertData/{tableName}")]
        public async Task<String> InsertToDataBase(DataTable dt, string tableName)
        {
            // DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
            DataTable dtOriginal = new DataTable();
            var intColumns = await GetColumn(dt.TableName);
            string sqlConn = _configuration.GetConnectionString("CRMConnectStrings");
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM " + tableName + " where 1<0", sqlConn))
            {
                // create the DataSet 
                DataSet dataSet = new DataSet();
                // fill the DataSet using our DataAdapter 
                dataAdapter.Fill(dataSet);
                dtOriginal = dataSet.Tables[0];
            }
            List<string> lstColumnName = new List<string>();
            List<string> lstColumnType = new List<string>();
            List<List<string>> lstData = new List<List<string>>();

            foreach (DataColumn cl in dt.Columns)
            {
                if (cl.ColumnName.ToLower() != "id")
                {
                    lstColumnName.Add(cl.ColumnName);
                    lstColumnType.Add(cl.DataType.Name);
                }
            }
            string sqlSelectColumn, sqlSelectData, sql = "", sqltemp = "";
            sqlSelectColumn = string.Join(",", lstColumnName);
            sqlSelectColumn = "INSERT INTO  " + tableName + " (" + sqlSelectColumn + ") ";

            foreach (DataRow row in dt.Rows)
            {
                List<string> lst = new List<string>();
                foreach (DataColumn cl in dtOriginal.Columns)
                {
                    if (cl.ColumnName.ToLower() != "id")
                    {
                        if (string.IsNullOrEmpty(row[cl.ColumnName]?.ToString()))
                        {
                            lst.Add("NULL");
                        }
                        else
                        {
                            if (cl.DataType.Name == "Int32")
                            {
                                int.TryParse(row[cl.ColumnName].ToString(), out int st);
                                lst.Add(st.ToString().Replace(",", "."));
                            }
                            else if (cl.DataType.Name == "Double")
                            {
                                _ = double.TryParse(row[cl.ColumnName].ToString(), out double st);
                                lst.Add(st.ToString().Replace(",", "."));
                            }
                            else if (cl.DataType.Name == "DateTime")
                            {
                                lst.Add("'" + string.Format("{0:yyyy/MM/dd}", row[cl.ColumnName]) + "'");
                            }
                            else if (cl.DataType.Name == "Boolean")
                            {
                                lst.Add("'" + row[cl.ColumnName].ToString() + "'");
                            }
                            else
                            {
                                lst.Add("N'" + row[cl.ColumnName].ToString() + "'");
                            }
                        }


                    }
                }

                sqlSelectData = string.Join(",", lst);
                sql = sql + sqlSelectColumn + " SELECT " + sqlSelectData + Environment.NewLine;

                lstData.Add(lst);
            }
            try
            {
                _context.Database.ExecuteSqlRaw(sql);

                return "Import thành công.";
            }
            catch (Exception ex)
            {

                return ex.Message + Environment.NewLine + ex.StackTrace;
            }


        }
    }
}
