using System;
using System.Data;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using System.Data.OleDb;
using System.IO;

namespace presentacion
{
    public class ReadXL
    {
                public static DataTable ImportExcel(string filePath)
        {
            //Create a new DataTable.
            DataTable dt = new DataTable();
            try
            {

                //Open the Excel file in Read Mode using OpenXml.
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, false))
                {
                    //Read the first Sheet from Excel file.
                    Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();

                    //Get the Worksheet instance.
                    Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

                    //Fetch all the rows present in the Worksheet.
                    IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();


                    //Loop through the Worksheet rows.
                    foreach (Row row in rows)
                    {
                        //Use the first row to add columns to DataTable.
                        if (row.RowIndex.Value == 1)
                        {
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                dt.Columns.Add(GetValue(doc, cell));
                            }
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dt.Rows.Add();
                            int i = 0;
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                                i++;
                            }
                        }
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return dt;
            }

        }


        public static string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }

        public static DataTable ReadAsDataTable(string fileName)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fileName, false))
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();

                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        dataTable.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                    }

                    foreach (Row row in rows)
                    {
                        DataRow dataRow = dataTable.NewRow();
                      
                        for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                        {
                            if (spreadSheetDocument == null || row.Descendants<Cell>().ElementAt(i) == null)
                            {
                                string hey = "";
                            }
                            dataRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                        }

                        dataTable.Rows.Add(dataRow);
                    }

                }
                dataTable.Rows.RemoveAt(0);
                int c = dataTable.Rows.Count;
                return dataTable;
            }
            catch (Exception ex)
            {
                return dataTable;
            }
        }

        private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue.InnerXml;
            try
            {
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
                }
                else
                {
                    return value;
                }
            }
            catch (Exception)
            {
                return "";
            }
           
        }

     
    }
}