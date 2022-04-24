using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace EnrichAutoTest.Framework.HandlingInputTest
{
    public static class Extensions
    {
        public static T DeepClone<T>(this T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }
    }

    public class ExcelFileReader : AbstractFileReader
    {
        _Application excel = new _Excel.Application();
        const string pathInputTest = @"..\..\..\Framework\InputFile\Input.xlsx";
        Workbook wb;
        Worksheet ws;
        List<Testspec> listspec = new List<Testspec>();

        public Worksheet GetWorksheet
        {
            get { return ws; }
            set { ws = value; }
        }

        public Workbook GetWorkbook
        {
            get { return wb; }
            set { wb = value; }
        }

        //public ExcelFileReader()
        //{
        //}

        #region CheckFunction
        bool IsTeststep(string content)
        {
            if (content == "Test step")
                return true;
            else
                return false;
        }

        bool IsTestspec(string content)
        {
            if (content == "Test spec")
                return true;
            else
                return false;
        }

        bool IsTestId(string content)
        {
            if (content == "TestId")
                return true;
            else
                return false;
        }

        bool IsDescription(string content)
        {
            if (content == "Description")
                return true;
            else
                return false;
        }
        #endregion

        public override void OpenFile()
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = Path.Combine(sCurrentDirectory, pathInputTest);
            string sFilePath = Path.GetFullPath(sFile);
            wb = excel.Workbooks.Open(sFilePath);
            ws = (Worksheet)wb.Worksheets[1];
        }
        public override bool VerifySpec()
        {
            var testSpec = (string)(ws.Cells[1, 1] as _Excel.Range).Value;
            var description = (string)(ws.Cells[2, 2] as _Excel.Range).Value;
            var testId = (string)(ws.Cells[2, 3] as _Excel.Range).Value;
            var testStep = (string)(ws.Cells[2, 4] as _Excel.Range).Value;
            if (IsTestspec(testSpec) && IsDescription(description) && IsTestId(testId) && IsTeststep(testStep))
                return true;
            else
                return false;
        }

        public override List<Testspec> GetData()
        {
            int i = 0;
            int j = 0;
            bool checkRow = false;
            bool checkColumn = false;
            var listString = new List<string>();
            while (!checkColumn)
            {
                if ((ws.Cells[2 + i + 1, 2 + j] as _Excel.Range).Value != null)
                {
                    while (!checkRow)
                    {
                        if ((ws.Cells[2 + i + 1, 2 + j] as _Excel.Range).Value != null)
                        {
                            listString.Add((string)(ws.Cells[2 + i + 1, 2 + j] as _Excel.Range).Value);
                            j++;
                        }
                        else
                        {
                            listspec.Add(new Testspec(listString.DeepClone()));
                            listString.Clear();
                            j = 0;
                            i++;
                            checkRow = true;
                        }
                    }
                    checkRow = false;
                }
                else
                {
                    checkColumn = true;
                }
            }

            return listspec;
        }

        public override void CloseFile()
        {
            wb.Close();
        }
    }
}
