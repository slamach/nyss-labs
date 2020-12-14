using ParserApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ParserApp.Services
{
    public static class ExcelService
    {
        public static bool GetListFromFile(string dataPath, out List<SecurityThreat> outList)
        {
            outList = new List<SecurityThreat>();
            List<SecurityThreat> resultList = new List<SecurityThreat>();

            Excel.Application xlApplication = null;
            Excel.Workbook xlWorkbook = null;
            try
            {
                string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                xlApplication = new Excel.Application();
                xlWorkbook = xlApplication.Workbooks.Open(Path.Combine(exePath, dataPath));
                Excel.Worksheet xlWorksheet = xlWorkbook.Worksheets["Sheet"];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                for (int i = 3; i <= xlRange.Rows.Count; i++)
                {
                    long id = Int64.Parse(xlRange.Cells[i, 1].Text);
                    string name = xlRange.Cells[i, 2].Text;
                    string description = xlRange.Cells[i, 3].Text;
                    string source = xlRange.Cells[i, 4].Text;
                    string target = xlRange.Cells[i, 5].Text;
                    bool confViolated;

                    if (xlRange.Cells[i, 6].Text == "1")
                    {
                        confViolated = true;
                    }
                    else if (xlRange.Cells[i, 6].Text == "0")
                    {
                        confViolated = false;
                    }
                    else
                    {
                        throw new FormatException("confViolated");
                    }

                    bool integViolated;
                    if (xlRange.Cells[i, 7].Text == "1")
                    {
                        integViolated = true;
                    }
                    else if (xlRange.Cells[i, 7].Text == "0")
                    {
                        integViolated = false;
                    }
                    else
                    {
                        throw new FormatException("integViolated");
                    }

                    bool accessViolated;
                    if (xlRange.Cells[i, 8].Text == "1")
                    {
                        accessViolated = true;
                    }
                    else if (xlRange.Cells[i, 8].Text == "0")
                    {
                        accessViolated = false;
                    }
                    else
                    {
                        throw new FormatException("accessViolated");
                    }

                    resultList.Add(new SecurityThreat(
                        id,
                        name,
                        description,
                        source,
                        target,
                        confViolated,
                        integViolated,
                        accessViolated));
                }
                outList = resultList;
                return true;
            }
            catch (COMException exception)
            {
                // MessageBox.Show(exception.ToString(), "Debug");

            }
            catch (FormatException exception)
            {
                // MessageBox.Show(exception.ToString(), "Debug");
            }
            finally
            {
                if (xlWorkbook != null)
                {
                    xlWorkbook.Close();
                }
                if (xlApplication != null)
                {
                    xlApplication.Quit();
                }
            }

            return false;
        }
    }
}
