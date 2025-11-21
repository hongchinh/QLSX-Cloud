using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ReportAPINet.Models;
using System.IO;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace ReportAPINet.Controllers
{
    public class ReportsController : Controller
    {

        private ReportDocument docReport;
        private ReportDocument SubDocReport;


        public string id = "";
        public string token = "";
        public string type = "";
        public string tungay = "";
        public string denngay = "";

        public bool glbConnectSA = true;
        public string databases = "";
        public string ReportFileName = "";
        public string makhoid = "";
        public string mdvsd = "";
        public string UserId = "";
        public string lphieu = "";
        public string tennhanvien = "";
        public string idids = "";
        public string ids = "";
        public string ngay = "";
        public string nhom = "";
        public ActionResult Index()
        {
            ConfigureCrystalReports(false);
            return View();
        }

        public ActionResult Viewer()
        {

            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"].ToString();
            }
            if (Request.QueryString["ReportFileName"] != null)
            {
                ReportFileName = Request.QueryString["ReportFileName"].ToString();
            }


            if (Request.QueryString["makhoid"] != null)
            {
                makhoid = Request.QueryString["makhoid"].ToString();
            }
            if (Request.QueryString["mdvsd"] != null)
            {
                mdvsd = Request.QueryString["mdvsd"].ToString();
            }
            if (Request.QueryString["userid"] != null)
            {
                UserId = Request.QueryString["userid"].ToString();
            }
            if (Request.QueryString["tungay"] != null)
            {
                tungay = Request.QueryString["tungay"].ToString();
            }
            if (Request.QueryString["denngay"] != null)
            {
                denngay = Request.QueryString["denngay"].ToString();
            }
            if (Request.QueryString["lphieu"] != null)
            {
                lphieu = Request.QueryString["lphieu"].ToString();
            }
            if (Request.QueryString["idids"] != null)
            {
                idids = Request.QueryString["idids"].ToString();
            }
            if (Request.QueryString["nhom"] != null)
            {
                nhom = Request.QueryString["nhom"].ToString();
            }
            if (Request.QueryString["tennhanvien"] != null)
            {
                tennhanvien = Request.QueryString["tennhanvien"].ToString();
            }
            if (Request.QueryString["ids"] != null)
            {
                ids = Request.QueryString["ids"].ToString();
            }
            if (Request.QueryString["ngay"] != null)
            {
                ngay = Request.QueryString["ngay"].ToString();
            }
            ConfigureCrystalReports(false);
            Stream s = docReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");


        }

        public ExcelResult XMLViewer()
        {
            token = Request.QueryString["token"].ToString();
            id = Request.QueryString["id"].ToString();
            type = Request.QueryString["type"].ToString();
            ReportFileName = Request.QueryString["ReportFileName"].ToString();
            if (Request.QueryString["makhoid"] != null)
            {
                makhoid = Request.QueryString["makhoid"].ToString();
            }
            if (Request.QueryString["mdvsd"] != null)
            {
                mdvsd = Request.QueryString["mdvsd"].ToString();
            }
            var isValid = TokenManager.ValidAccessToken(token);
            if (!isValid) Redirect("/notfound");

            XMLExport.ChuyenDanhMucToExcelXML("GetSoDuLoaiTien", ReportFileName, int.Parse(mdvsd));

            return new ExcelResult
            {
                FileName = ReportFileName + ".xml",
                Path = "~/XMLFiles/Temps/" + ReportFileName + ".xml"
            };

        }
        public void ConfigureCrystalReports(bool InPhieuTrucTiepOK)
        {
            databases = System.Configuration.ConfigurationManager.AppSettings.Get("DatabaseName");
            ConnectionInfo myConnectionInfo = new ConnectionInfo();
            myConnectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings.Get("DatabaseName");
            myConnectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings.Get("ServerDB");
            string connetsq = System.Configuration.ConfigurationManager.AppSettings.Get("TypeConnect");
            if (connetsq == "sa")
            {
                glbConnectSA = true;
                myConnectionInfo.IntegratedSecurity = false;
                myConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
                myConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings.Get("UserSa");
            }
            else
            {
                myConnectionInfo.IntegratedSecurity = true;
            }
            docReport = new ReportDocument();

            string reportPath = "~/Reports/" + ReportFileName + ".rpt";
            docReport.Load(Server.MapPath(reportPath));
            SetDBLogonForReport(myConnectionInfo, docReport);
            SetDBLogonForSubreports(myConnectionInfo, docReport);
            SetCurrentValuesForParameterField(docReport);
        }
        private void SetDBLogonForReport(CrystalDecisions.Shared.ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {

                TableLogOnInfo tableLogonInfo = table.LogOnInfo;

                //tableLogonInfo.TableName = tableLogonInfo.TableName.Replace(";", "_phukien;");
                //if (!tableLogonInfo.TableName.ToLower().Contains("coquan"))
                //{

                //    tableLogonInfo.TableName = tableLogonInfo.TableName.Replace(";", "_phukien;");
                //}

                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
                table.Location = databases + ".dbo." + table.Name;

                //if (table.Name.ToLower().Contains("coquan"))
                //{
                //    table.Location = databases + ".dbo." + table.Name;
                //}
                //else
                //{
                //    table.Location = databases + ".dbo." + table.Name.Replace(";", "_phukien;");
                //}

            }

        }
        private void SetDBLogonForSubreports(CrystalDecisions.Shared.ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {

            Sections sections = reportDocument.ReportDefinition.Sections;

            foreach (Section section in sections)
            {

                ReportObjects reportObjects = section.ReportObjects;

                foreach (ReportObject reportObject in reportObjects)
                {

                    if (reportObject.Kind == ReportObjectKind.SubreportObject)
                    {
                        SubreportObject subreportObject = (SubreportObject)reportObject;
                        ReportDocument subReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName);
                        SetDBLogonForReport(connectionInfo, subReportDocument);
                    }
                }

            }

        }


        private void SetCurrentValuesForParameterField(ReportDocument myReportDocument)
        {

            ParameterFieldDefinitions myParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
            foreach (ParameterFieldDefinition myParameterFieldDefinition in myParameterFieldDefinitions)
            {
                string txtsubname = myParameterFieldDefinition.ReportName;
                ParameterValues currentParameterValues = new ParameterValues();
                ParameterDiscreteValue myParameterDiscreteValue = new ParameterDiscreteValue();
                switch (myParameterFieldDefinition.ParameterFieldName)
                {
                    case "@hien":
                        myParameterDiscreteValue.Value = false;
                        break;
                    case "@date1":
                        myParameterDiscreteValue.Value = tungay;
                        break;
                    case "@date2":
                        myParameterDiscreteValue.Value = denngay;
                        break;
                    case "@mdvsd":
                        myParameterDiscreteValue.Value = mdvsd;
                        break;
                    case "@lphieu":
                        myParameterDiscreteValue.Value = lphieu;
                        break;
                    case "@id":
                        myParameterDiscreteValue.Value = id;
                        break;
                    case "@UserId":
                        myParameterDiscreteValue.Value = UserId;
                        break;
                    case "@tmptblOK":
                        myParameterDiscreteValue.Value = "ZZZBAOCAO";
                        break;
                    case "@nhom":
                        myParameterDiscreteValue.Value = nhom;
                        break;

                    case "@tennhanvien":
                        myParameterDiscreteValue.Value = tennhanvien;
                        break;
                    case "@idids":
                        myParameterDiscreteValue.Value = idids;
                        break;
                    case "@ids":
                        myParameterDiscreteValue.Value = ids;
                        break; ;
                    case "@ngay":
                        myParameterDiscreteValue.Value = ngay;
                        break;

                }
                currentParameterValues.Add(myParameterDiscreteValue);
                myParameterFieldDefinition.ApplyCurrentValues(currentParameterValues);
            }
        }
    }
}
