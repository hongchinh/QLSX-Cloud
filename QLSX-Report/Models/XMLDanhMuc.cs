using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace ReportAPINet.Models
{
    public class XMLExport
    {
        public static void ChuyenDanhMucToExcelXML(string storeName,string filename, int mdvsd)
        {
            int J = 0;
            string sourcefile = "";
            string destfile = "";
            bool fmrow = false;
            string crlft = System.Environment.NewLine;// "\r\n";

            string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            sourcefile = HttpContext.Current.Server.MapPath("~/XMLFiles/" + filename + ".xml");
            destfile = HttpContext.Current.Server.MapPath("~/XMLFiles/Temps/" + filename + ".xml");
            string fileContents = null;
            string fileContents1 = "";
            string fileContents2 = "";
            StreamReader myFile = new StreamReader(sourcefile);
            fileContents = myFile.ReadToEnd();
            myFile.Close();

            string ssid = GetHeader();

            if (System.IO.File.Exists(destfile)) System.IO.File.Delete(destfile);
            StreamWriter fileSAVE = new System.IO.StreamWriter(destfile, true);

            J = fileContents.IndexOf("<Styles>");
            if (J > 0) fileContents = fileContents.Insert(J + 8, " " + crlft + ssid);
            J = fileContents.IndexOf("<Table");
            fileContents1 = fileContents.Substring(0, J - 1);

            J = fileContents.IndexOf("<Column");
            fileContents2 = fileContents.Substring(J - 1, fileContents.Length - J + 1);


            fileSAVE.WriteLine(fileContents1);

            string stylesBegin = "<Cell  ss:StyleID=\"LeftBorderNoBold\"><Data ss:Type=\"String\">";
            string stylesEnd = "</Data></Cell>" + crlft;

            DataSet ds = new DataSet();
            SqlDataAdapter da = default(SqlDataAdapter);
            string sql = null;
            int colall = 0;
            int rowall = 0;
            int i = 0;
            string txtData = "";
            string fillDATAStr = "";
            long fillDATANum = 0;
            double fillDATADbl = 0;
            DataTable tb = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                String sqlstr = "[dbo].[" + storeName + "]";

                using (SqlCommand command = new SqlCommand(sqlstr, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@mdvsd", SqlDbType.Int);
                    command.Parameters["@mdvsd"].Value = mdvsd;
                    // add any extra parameters and then:
                    try
                    {
                        connection.Open();
                        SqlDataReader dr = command.ExecuteReader();
                        tb.Load(dr);
                        // process or return dt or dr
                    }
                    catch
                    {

                    }
                }
            }
            rowall = tb.Rows.Count + 100;
            colall = tb.Columns.Count + 10;
            fileSAVE.WriteLine("<Table ss:ExpandedColumnCount=\"" + colall + "\" ss:ExpandedRowCount=\"" + rowall + "\" x:FullColumns=\"1\"   x:FullRows=\"1\">");
            J = fileContents2.IndexOf("</Table>");
            fileContents1 = fileContents2.Substring(0, J - 1);
            fileContents2 = fileContents2.Substring(J - 1, fileContents2.Length - J + 1);
            fileSAVE.Write(fileContents1);

            foreach (DataRow rw in tb.Rows)
            {
                i = 0;
                txtData = "";
                txtData = txtData + "<Row>" + crlft;
                foreach (DataColumn cl in tb.Columns)
                {
                    fillDATADbl = 0;
                    fillDATANum = 0;
                    fillDATAStr = "";
                    switch (rw[i].GetType().Name)
                    {
                        case "Boolean":
                            if (!rw.IsNull(i) && (bool)rw[i] == true) fillDATAStr = "√";
                            txtData = txtData + stylesBegin.Replace("Left", "Center") + fillDATAStr + stylesEnd;
                            break;
                        case "Int64":
                            if (!rw.IsNull(i)) fillDATANum = (long)rw[i];
                            txtData = txtData + stylesBegin.Replace("String", "Number").Replace("Left", "Center") + fillDATANum + stylesEnd;
                            break;
                        case "Int32":
                            if (!rw.IsNull(i)) fillDATANum = (int)rw[i];
                            txtData = txtData + stylesBegin.Replace("String", "Number").Replace("Left", "Center") + fillDATANum + stylesEnd;
                            break;
                        case "Double":
                            if (!rw.IsNull(i)) fillDATADbl = (Double)rw[i];
                            txtData = txtData + stylesBegin.Replace("String", "Number").Replace("LeftBorderNoBold", "FormatNumber") + fillDATADbl + stylesEnd;
                            break;
                        case "String":
                            if (!rw.IsNull(i)) fillDATAStr = (string)rw[i];
                            txtData = txtData + stylesBegin + fillDATAStr + stylesEnd;
                            break;
                        case "DateTime":
                            if (!rw.IsNull(i)) fillDATAStr = String.Format("{0:dd/MM/yyyy}", (DateTime)rw[i]);
                            txtData = txtData + stylesBegin.Replace("Left", "Center") + fillDATAStr + stylesEnd;
                            break;
                        case "DBNull":
                            fillDATAStr = "";
                            txtData = txtData + stylesBegin + fillDATAStr + stylesEnd;
                            break;
                    }

                    i = i + 1;
                }
                txtData = txtData + "</Row>" + crlft;
                fileSAVE.Write(txtData);
            }

            fileContents2.Replace("TEMP.DONVI", "");
            fileContents2.Replace("TEMP.DIACHI", "");
            colall = colall + 20;

            fileSAVE.Write(fileContents2);
            fileSAVE.Close();
        }

        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        public static string GetHeader()
        {
            string ssid = "";
            string crlft = System.Environment.NewLine;// "\r\n";
            ssid = " " + crlft;
            ssid = ssid + "<Style ss:ID=\"CenterBorderNoBold\" > " + crlft;
            ssid = ssid + "<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>" + crlft;
            ssid = ssid + "<Borders> " + crlft;
            ssid = ssid + "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "</Borders>" + crlft;
            ssid = ssid + "<Font ss:FontName=\"Times New Roman\" x:Family=\"Roman\" ss:Bold=\"0\"/>" + crlft;
            ssid = ssid + "<Interior/>" + crlft;
            ssid = ssid + "</Style>" + crlft;


            ssid = ssid + "<Style ss:ID=\"LeftBorderNoBold\" > " + crlft;
            ssid = ssid + "<Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>" + crlft;
            ssid = ssid + "<Borders> " + crlft;
            ssid = ssid + "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "</Borders>" + crlft;
            ssid = ssid + "<Font ss:FontName=\"Times New Roman\" x:Family=\"Roman\" ss:Bold=\"0\"/>" + crlft;
            ssid = ssid + "<Interior/>" + crlft;
            ssid = ssid + "</Style>" + crlft;

            ssid = ssid + "<Style ss:ID=\"RightBorderNoBold\" > " + crlft;
            ssid = ssid + "<Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>" + crlft;
            ssid = ssid + "<Borders> " + crlft;
            ssid = ssid + "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "</Borders>" + crlft;
            ssid = ssid + "<Font ss:FontName=\"Times New Roman\" x:Family=\"Roman\" ss:Bold=\"0\"/>" + crlft;
            ssid = ssid + "<Interior/>" + crlft;
            ssid = ssid + "</Style>" + crlft;


            ssid = ssid + "<Style ss:ID=\"CenterBorderBold\" > " + crlft;
            ssid = ssid + "<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>" + crlft;
            ssid = ssid + "<Borders> " + crlft;
            ssid = ssid + "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "</Borders>" + crlft;
            ssid = ssid + "<Font ss:FontName=\"Times New Roman\" x:Family=\"Roman\" ss:Bold=\"1\"/>" + crlft;
            ssid = ssid + "<Interior/>" + crlft;
            ssid = ssid + "</Style>" + crlft;


            ssid = ssid + "<Style ss:ID=\"LeftBorderBold\" > " + crlft;
            ssid = ssid + "<Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>" + crlft;
            ssid = ssid + "<Borders> " + crlft;
            ssid = ssid + "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "</Borders>" + crlft;
            ssid = ssid + "<Font ss:FontName=\"Times New Roman\" x:Family=\"Roman\" ss:Bold=\"1\"/>" + crlft;
            ssid = ssid + "<Interior/>" + crlft;
            ssid = ssid + "</Style>" + crlft;

            ssid = ssid + "<Style ss:ID=\"RightBorderBold\" > " + crlft;
            ssid = ssid + "<Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>" + crlft;
            ssid = ssid + "<Borders> " + crlft;
            ssid = ssid + "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" + crlft;
            ssid = ssid + "</Borders>" + crlft;
            ssid = ssid + "<Font ss:FontName=\"Times New Roman\" x:Family=\"Roman\" ss:Bold=\"1\"/>" + crlft;
            ssid = ssid + "<Interior/>" + crlft;
            ssid = ssid + "</Style>" + crlft;


            ssid = ssid + "<Style ss:ID=\"FormatNumber\" ss:Parent=\"RightBorderNoBold\">";

            ssid = ssid + "<NumberFormat ss:Format=\"_(* #,##0_);_(* \\(#,##0\\);_(* &quot;-&quot;??_);_(@_)\"/>";

            ssid = ssid + "</Style>";

            return ssid;
        }

    }
}