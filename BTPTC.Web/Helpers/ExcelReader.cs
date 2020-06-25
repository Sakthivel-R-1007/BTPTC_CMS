using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BTPTC.Web.Helpers
{
    public class ExcelReader
    {

        private string _filePath;
        private string _fileType;
        public ExcelReader(string filePath, string fileType)
        {
            _filePath = filePath;
            _fileType = fileType;
        }

        public DataTable GetCSVData()
        {
            string csvData = System.IO.File.ReadAllText(_filePath);

            DataTable result = new DataTable();

            string[] data = csvData.Split('\n');

            if (data != null && data.Length > 0)
            {
                foreach (string row in data[0].Split(','))
                {
                    result.Columns.Add(row.Split('\r')[0]);
                }

                //Execute a loop over the rows.
                foreach (string row in data.Skip(1))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        int i = 0;
                        DataRow dr = result.NewRow();
                        foreach (string rowData in RemoveDoubleQuotedDataCommas(row).Split(','))
                        {
                            dr[i] = Regex.Unescape(rowData.Replace('~', ',').Split('\r')[0]);

                            i++;
                        }
                        if (dr[0] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr[0])))
                        {
                            result.Rows.Add(dr);
                        }
                    }
                }
            }

            return result.Rows.Count > 0 ? result : null;
        }

        public DataTable GetXSLXData()
        {
            IExcelDataReader iExcelDataReader = null;
            FileStream oStream = System.IO.File.Open(_filePath, FileMode.Open, FileAccess.Read);
            string strFileType = Path.GetExtension(_filePath).ToString().ToLower();
            if (strFileType == ".xls")
                iExcelDataReader = ExcelReaderFactory.CreateBinaryReader(oStream);
            else if (strFileType == ".xlsx")
                iExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(oStream);
            iExcelDataReader.IsFirstRowAsColumnNames = true;
            DataSet ds = new DataSet();
            ds = iExcelDataReader.AsDataSet();
            iExcelDataReader.Close();
            DataTable DT = ds.Tables!=null && ds.Tables.Count>0? ds.Tables[0]:new DataTable();
            return DT;
        }

        public DataTable Get()
        {
            DataTable result = null;
            string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            if (_fileType == "xls")
            {
                conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (_fileType == "xlsx")
            {
                conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            conString = string.Format(conString, _filePath);

            if (File.Exists(_filePath))
            {
                using (OleDbConnection excelConn = new OleDbConnection(conString))
                {
                    excelConn.Open();
                    result = excelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                }
            }
            return result;
        }

        public string RemoveDoubleQuotedDataCommas(string str)
        {
            //all data inside the quoted text
            foreach (Match r in new Regex("\".*?\"").Matches(str))
            {
                string extractedData = r.ToString().Trim('"');
                //processing numbers
                str = str.Replace(r.ToString(), extractedData.Replace(",", (Regex.IsMatch(extractedData, @"^[0-9\.\,]+$")) ? "" : "~"));
            }
            return str;
        }
    }
}