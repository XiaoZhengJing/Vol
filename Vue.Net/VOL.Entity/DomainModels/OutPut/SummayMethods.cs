using Aspose.Cells;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Spire.Pdf.General.Find;
using Spire.Pdf.Graphics;
using Spire.Pdf;
using System;
using System.IO;
using System.Linq;
using System.Drawing;
using Microsoft.Extensions.Logging;
using NPOI.XSSF.UserModel;
using System.Diagnostics;
using SixLabors.ImageSharp.PixelFormats;

namespace VOL.Entity.DomainModels.OutPut
{
    public class SummayMethods
    {
        /// <summary>
        /// 返回国际货币符号 1.人民币   2.美元   3.欧元
        /// </summary>
        /// <param name="priceType"></param>
        /// <returns></returns>
        public static string CurrencySymbol(string priceType)
        {
            switch (priceType)
            {
                case "1":
                    return " ￥";

                case "2":
                    return " $";

                case "3":
                    return " €";
                default: return null;
            }

        }


        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="totalDemand"></param>
        /// <returns></returns>
        public static string Format(string totalDemand, bool isLastTwoDigits)
        {
            if (!string.IsNullOrEmpty(totalDemand))
            {
                var demand = totalDemand.Split('.');
                var formatNumber = FormatNumber(demand[0]);
                if (isLastTwoDigits)
                    formatNumber += "." + demand[1];
                return formatNumber;
            }
            return "";
        }

        /// <summary>
        /// 格式化数字   
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatNumber(string number)
        {
            var numberStr = "";
            string result = Reverse(number);
            for (int i = 0; i < result.Length; i++)
            {
                var str = "";
                if (result.Length - i > 3)
                    str = result.Substring(i, 3) + ",";
                if (result.Length - 3 <= i)
                    str = result.Substring(i, result.Length - i);
                numberStr += str;
                i += 2;
            }
            return Reverse(numberStr);

        }

        /// <summary>
        /// 字符串反转
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Reverse(string number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                char[] array = number.ToCharArray();
                Array.Reverse(array);
                return new string(array);
            }
            return null;
        }


        /// <summary>
        /// 获取数据 SendingPlant
        /// </summary>
        /// <param name="SendingPlantID"></param>
        /// <returns></returns>
        public static string SendingPlant(int? sendingPlantID)
        {
            switch (sendingPlantID)
            {
                case 1:
                    return "30R3";
                case 2:
                    return "40R4";
                case 3:
                    return "50R5";
                default: return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public static string ProjectNameSplit(string projectName)
        {
            if (!string.IsNullOrEmpty(projectName))
            {
                var projectNameList = projectName.Split(',').ToList();
                if (projectNameList.Count > 3 && projectNameList.Count <= 6 && projectName.Length > 23)
                    projectNameList.Insert(3, "\n");
                if (projectNameList.Count > 6)
                {
                    projectNameList.Insert(3, "\n");

                }
                var str = "";
                foreach (var item in projectNameList)
                {
                    if (item != "\n")
                        str += item + ",";
                    if (item == "\n")
                        str += item;
                }
                return str;
            }

            return null;
        }


        /// <summary>
        /// 获取 ProjectName
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public static string ProjectName(string projectID)
        {
            if (!string.IsNullOrEmpty(projectID))
            {
                var projectIDArr = projectID.Split(';');
                var projectName = "";
                foreach (var item in projectIDArr)
                {
                    switch (item)
                    {
                        case "1":

                            if (projectName.Contains("P81B"))
                            {
                                projectName = projectName.Replace("P81B", "P81A/B");
                            }
                            else
                            {
                                projectName += "P81A,";
                            }
                            break;
                        case "2":
                            if (projectName.Contains("P81A"))
                            {
                                projectName = projectName.Replace("P81A", "P81A/B");
                            }
                            else
                            {
                                projectName += "P81B,";
                            }
                            break;
                        case "3":
                            if (projectName.Contains("P82C"))
                            {
                                projectName = projectName.Replace("P82C", "P82A/B/C");
                            }
                            else
                            {
                                projectName += "P82A/B,";
                            }
                            break;
                        case "4":
                            if (projectName.Contains("P82A/B"))
                            {
                                projectName = projectName.Replace("P82A/B", "P82A/B/C");
                            }
                            else
                            {
                                projectName += "P82C,";
                            }
                            break;
                        case "5":
                            projectName += "P83A/B,";
                            break;
                        case "6":
                            if (projectName.Contains("P60B"))
                            {
                                projectName = projectName.Replace("P60B", "P60A/B");
                            }
                            else
                            {
                                projectName += "P60A,";
                            }
                            break;
                        case "7":
                            if (projectName.Contains("P60A"))
                            {
                                projectName = projectName.Replace("P60A", "P60A/B");
                            }
                            else
                            {
                                projectName += "P60B,";
                            }
                            break;
                        case "9":
                            if (projectName.Contains("Denali LP2"))
                            {
                                projectName = projectName.Replace("Denali LP2", "Denali LP1/LP2");
                            }
                            else
                            {
                                projectName += "Denali LP1,";
                            }
                            break;
                        case "10":
                            if (projectName.Contains("Denali LP1"))
                            {
                                projectName = projectName.Replace("Denali LP1", "Denali LP1/LP2");
                            }
                            else
                            {
                                projectName = projectName += "Denali LP2,";
                            }
                            break;
                        case "8":
                            projectName += "Denali HP2,";
                            break;
                        case "11":
                            projectName += "Falcon,";
                            break;
                        case "12":
                            projectName += "T2,";
                            break;
                        case "13":
                            projectName += "T6,";
                            break;
                        case "14":
                            projectName += "T16,";
                            break;
                        case "15":
                            projectName += "P68A,";
                            break;
                        case "16":
                            projectName += "P68B,";
                            break;
                        case "17":
                            projectName += "P68G,";
                            break;
                        default: return null;
                    }
                }

                // (P68A, P68B,P68G),(P68A / P68B,P68A / P68B,P68B / P68G),(P68A / P68B / P68G)
                if (projectName.Contains("P68A") && projectName.Contains("P68B") && !projectName.Contains("P68G"))
                {
                    projectName = projectName.Replace("P68A,", "").Replace("P68B", "P68A/B");
                }
                else if (projectName.Contains("P68A") && projectName.Contains("P68G") && !projectName.Contains("P68B"))
                {
                    projectName = projectName.Replace("P68A,", "").Replace("P68G", "P68A/G");
                }
                else if (projectName.Contains("P68G") && projectName.Contains("P68B") && !projectName.Contains("P68A"))
                {
                    projectName = projectName.Replace("P68B,", "").Replace("P68G", "P68B/G");
                }
                else if (projectName.Contains("P68A") && projectName.Contains("P68B") && projectName.Contains("P68G"))
                {
                    projectName = projectName.Replace("P68A,", "").Replace("P68B,", "").Replace("P68G", "P68A/B/G");
                }



                // (T2, T6,T16),(T2 / T6,T2 / T16,T6 / T16),(T2 / T6 / T16)

                if (projectName.Contains("T16") && projectName.Contains("T6") && !projectName.Contains("T2"))
                {
                    projectName = projectName.Replace("T16,", "").Replace("T6", "T6/16");
                }
                else if (projectName.Contains("T16") && projectName.Contains("T2") && !projectName.Contains("T6"))
                {
                    projectName = projectName.Replace("T16,", "").Replace("T2", "T2/16");
                }
                else if (projectName.Contains("T2") && projectName.Contains("T6") && !projectName.Contains("T16"))
                {
                    projectName = projectName.Replace("T6,", "").Replace("T2", "T2/6");
                }
                else if (projectName.Contains("T6") && projectName.Contains("T16") && projectName.Contains("T2"))
                {
                    projectName = projectName.Replace("T6,", "").Replace("T16,", "").Replace("T2", "T2/6/16");
                }

                return ProjectNameSplit(projectName).TrimEnd(',');
            }
            return null;

        }

        /// <summary>
        ///  获取plantName
        /// </summary>
        /// <param name="plantId"></param>
        /// <returns></returns>
        public static string PlantName(int? plantId)
        {
            var plantName = "";
            if (plantId > 0)
            {
                switch (plantId)
                {
                    case 1:
                        plantName = "Plant1";
                        break;
                    case 2:
                        plantName = "Plant2";
                        break;
                    case 3:
                        plantName = "Plant3";
                        break;
                    default:
                        plantName = "";
                        break;
                }

            }

            return plantName;

        }

        /// <summary>
        /// 
        /// </summary>
        public static bool PDF(Summary summary, string excelFilePath, string noRemoveWatermarkPdfFilePath, string removeWatermarkPdfFilePath)
        {
            try
            {
                if (summary == null)
                    return false;
                //var CurrentPath = Directory.GetCurrentDirectory().Split("Vue.Net")[0]; // C:\Users\z004srhp\Desktop\code\Vue.Net\VOL.WebApi
                //string path = CurrentPath + @"output.xls";
                HSSFWorkbook hssfworkbook = new HSSFWorkbook();
                ISheet Firstsheet = hssfworkbook.CreateSheet("(EN) Summary");
                ISheet Secondsheet = hssfworkbook.CreateSheet("(EN) Series Demands");
                ISheet Thirdsheet = hssfworkbook.CreateSheet("(EN) Service Demands");

                NPOIHelper nPOIHelper = new NPOIHelper();

                #region Firstsheet 

                {
                    int[] colWidth = new int[13];

                    for (int i = 0; i < 13; i++)
                    {
                        colWidth[i] = 10;
                        //设置列宽
                        if ((colWidth[i] + 1) * 256 > 30000)
                        {
                            Firstsheet.SetColumnWidth(i, 10000);
                        }
                        else
                        {
                            Firstsheet.SetColumnWidth(i, (colWidth[i] + 1) * 256);
                        }
                    }

                }
                #endregion

                if (summary.Project != null)
                {
                    #region first Sheet 填充数据

                    // FirstSheet
                    for (int i = 0; i < 34; i++)
                    {
                        IRow dataRow = Firstsheet.CreateRow(i);
                        dataRow.HeightInPoints = 18;
                        #region 第一页 (EN) Summary
                        if (i == 0)
                        {
                            #region 表头及样式
                            {
                                IRow headerRow = Firstsheet.CreateRow(0);
                                headerRow.HeightInPoints = 25;
                                headerRow.CreateCell(0).SetCellValue("Determination of Demand acc. to MFR 190 / 005");

                                ICellStyle headStyle = hssfworkbook.CreateCellStyle();
                                headStyle.Alignment = HorizontalAlignment.Center;
                                NPOI.SS.UserModel.IFont font = hssfworkbook.CreateFont();
                                font.FontHeightInPoints = 10;
                                //  font.Boldweight = 700;
                                headStyle.SetFont(font);
                                headerRow.GetCell(0).CellStyle = headStyle;

                                //合并单元格
                                Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 12));


                            }
                            #endregion
                        }
                        if (i == 1)
                            dataRow.HeightInPoints = 25;



                        if (i == 2)
                        {
                            dataRow.HeightInPoints = 25;
                            #region// 第三行  
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 16, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);

                                // 设置边框
                                if (true)
                                    nPOIHelper.SetBorderStyle(cellStyle, i, j, BorderStyle.Thin);

                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                cell.CellStyle = cellStyle;
                            }

                            //合并单元格
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 0, 8));
                            continue;
                            #endregion
                        }

                        if (i == 3)
                        {
                            #region // 第四行

                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 2)
                                {
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }

                            //合并单元格
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 0, 1));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 2, 3));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 6, 7));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 8, 9));
                            continue;
                            #endregion
                        }

                        if (i == 4)
                        {
                            #region // 第五行

                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 2)
                                {
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }

                            //合并单元格
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 0, 1));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 2, 3));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 6, 7));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 8, 12));
                            continue;
                            #endregion
                        }

                        if (i == 5)
                        {
                            #region // 第六行
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                cell.CellStyle = cellStyle2;
                            }
                            continue;
                            #endregion
                        }

                        if (i == 6)
                        {
                            #region // 第七行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 8)
                                {
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }
                            //合并单元格
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 0, 1));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 2, 5));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 6, 7));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 8, 9));
                            continue;
                            #endregion
                        }

                        if (i == 7)
                        {
                            #region // 第八行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 8)
                                {
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }

                            //合并单元格
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(7, 7, 0, 1));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(7, 7, 2, 5));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(7, 7, 6, 7));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(7, 7, 8, 9));
                            continue;
                            #endregion
                        }

                        if (i == 8)
                        {
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                cell.CellStyle = cellStyle2;
                            }
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(8, 8, 0, 12));
                            continue;
                        }

                        if (i == 9)
                        {
                            #region // 第十行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 2 || j == 8)
                                {
                                    // 靠右
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }
                            //合并单元格
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 9, 0, 1));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 9, 2, 3));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 9, 6, 7));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 9, 8, 9));
                            continue;
                            #endregion
                        }

                        if (i == 10)
                        {
                            #region // 第十一行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 2 || j == 8)
                                {
                                    // 靠右
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }
                            //合并单元格
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(10, 10, 0, 1));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(10, 10, 2, 3));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(10, 10, 6, 7));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(10, 10, 8, 9));
                            continue;
                            #endregion
                        }

                        if (i == 11)
                        {
                            #region // 第十二行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 3)
                                {
                                    // 靠右
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else if (j == 10)
                                {
                                    // 靠右
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }
                            //合并单元格
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(11, 11, 0, 2));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(11, 11, 6, 8));
                            continue;
                            #endregion
                        }

                        if (i == 12)
                        {
                            #region // 第十三行    

                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 2 || j == 8)
                                {
                                    // 靠右
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }
                            //合并单元格
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(12, 12, 0, 1));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(12, 12, 2, 3));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(12, 12, 6, 7));
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(12, 12, 8, 9));
                            continue;
                            #endregion
                        }

                        if (i == 13)
                        {
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                cell.CellStyle = cellStyle2;
                            }
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(13, 13, 0, 12));
                            continue;
                        }

                        if (i == 14)
                        {
                            #region // 第十五行    

                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 11)
                                {
                                    // 靠右
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }
                            continue;
                            #endregion
                        }

                        if (i == 15)
                        {
                            #region // 第十六行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 11)
                                {
                                    // 靠右
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }
                            continue;
                            #endregion
                        }

                        if (i == 16)
                        {
                            #region // 第十七行   

                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 9)
                                {
                                    // 靠右
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 12, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }
                            continue;
                            #endregion
                        }

                        if (i == 18)
                        {
                            #region // 第十九行    

                            for (int j = 0; j < 13; j++)
                            {

                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 4 || j == 7)
                                {
                                    // 靠右
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else if (j == 2)
                                {
                                    // 
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }

                            }
                            continue;
                            #endregion
                        }

                        if (i == 20)
                        {
                            #region // 第二十一行 
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 4 || j == 7)
                                {
                                    // 靠右
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else if (j == 2)
                                {
                                    // 
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }
                            }
                            continue;
                            #endregion
                        }

                        if (i == 22)
                        {
                            #region // 第二十三行  

                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                // 填充单元格内容
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                if (j == 4 || j == 7)
                                {
                                    // 靠右
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else if (j == 2)
                                {
                                    // 
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle2;
                                }

                            }
                            continue;
                            #endregion
                        }

                        if (i == 24)
                        {
                            #region // 第二十五行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, false, BorderStyle.Thin, j, i, true);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                cell.CellStyle = cellStyle2;
                            }
                            continue;
                            #endregion
                        }

                        if (i == 25)
                        {
                            #region // 第二十六行   
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, false, BorderStyle.Thin, j, i, true);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                cell.CellStyle = cellStyle2;
                            }
                            #endregion
                        }

                        if (i == 26)
                        {
                            #region // 第二十七行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, false, BorderStyle.Thin, j, i, true);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                cell.CellStyle = cellStyle2;
                            }
                            continue;
                            #endregion
                        }
                        if (i == 27)
                        {
                            #region // 第二十八行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, false, BorderStyle.Thin, j, i, true);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                cell.CellStyle = cellStyle2;
                            }
                            continue;
                            #endregion
                        }

                        if (i == 28)
                        {
                            #region // 第二十九行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, false, BorderStyle.Thin, j, i, true);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                cell.CellStyle = cellStyle2;
                            }
                            continue;
                            #endregion
                        }

                        if (i == 29)
                        {
                            #region // 第三十行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Left, VerticalAlignment.Center,
                                    false, 700, false, BorderStyle.Thin, j, i, true);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                cell.CellStyle = cellStyle2;
                            }
                            continue;
                            #endregion
                        }

                        if (i == 33)
                        {
                            #region // 第三十四行    
                            for (int j = 0; j < 13; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    false, 700, false, BorderStyle.Thin, j, i, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValue(cell, i, j, summary);
                                cell.CellStyle = cellStyle2;
                            }
                            Firstsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(33, 33, 0, 12));
                            continue;
                            #endregion
                        }

                        #endregion
                    }
                    #endregion
                }

                if (summary.SeriesDemandsList.Any())
                {
                    #region Second Sheet 填充数据



                    //  合并单元格
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 0, 3));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 11, 13));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 14, 17));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 0, 3));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 11, 13));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 14, 17));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 0, 8));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 9, 10));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 11, 15));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 0, 1));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 2, 4));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 5, 6));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 7, 8));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 9, 10));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 11, 13));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 14, 15));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 16, 17));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(21, 21, 0, 17));

                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(8, 8, 9, 10));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 9, 9, 10));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(10, 10, 9, 10));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(11, 11, 9, 10));

                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(12, 12, 9, 10));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(13, 13, 9, 10));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(14, 14, 9, 10));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(15, 15, 9, 10));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(16, 16, 9, 10));
                    Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(17, 17, 9, 10));



                    Secondsheet.SetColumnWidth(0, 2000);
                    Secondsheet.SetColumnWidth(1, 4000);
                    Secondsheet.SetColumnWidth(2, 1200);
                    Secondsheet.SetColumnWidth(3, 1200);
                    Secondsheet.SetColumnWidth(4, 1300);
                    Secondsheet.SetColumnWidth(5, 1200);
                    Secondsheet.SetColumnWidth(6, 2000);
                    Secondsheet.SetColumnWidth(7, 2000);
                    Secondsheet.SetColumnWidth(8, 1000);
                    Secondsheet.SetColumnWidth(9, 2500);
                    Secondsheet.SetColumnWidth(10, 2500);
                    Secondsheet.SetColumnWidth(11, 2000);
                    Secondsheet.SetColumnWidth(12, 2400);
                    Secondsheet.SetColumnWidth(13, 2400);
                    Secondsheet.SetColumnWidth(14, 2400);
                    Secondsheet.SetColumnWidth(15, 2400);
                    Secondsheet.SetColumnWidth(16, 1200);
                    Secondsheet.SetColumnWidth(17, 4000);



                    for (int i = 0; i < 22; i++)
                    {
                        IRow dataRow = Secondsheet.CreateRow(i);
                        dataRow.HeightInPoints = 22;



                        #region 第二页 (EN) Series Demands
                        if (i == 0)
                        {
                            #region     
                            for (int j = 0; j < 18; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i + 100, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForSecond(cell, i + 100, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }
                            //合并单元格
                            Secondsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 18));
                            continue;
                            #endregion
                        }

                        if (i == 2)
                        {
                            #region    
                            for (int j = 0; j < 18; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 11, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 100, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForSecond(cell, i + 100, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }

                            continue;
                            #endregion
                        }

                        if (i == 3)
                        {
                            #region    
                            for (int j = 0; j < 18; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 11, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 100, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForSecond(cell, i + 100, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }

                            continue;
                            #endregion
                        }

                        if (i == 4)
                        {
                            #region    
                            for (int j = 0; j < 18; j++)
                            {

                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 100, false);
                                var cell = dataRow.CreateCell(j);
                                cell.CellStyle = cellStyle;
                            }

                            continue;
                            #endregion
                        }

                        if (i == 5)
                        {
                            #region   
                            for (int j = 0; j < 18; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 100, false);
                                //使用NPOI已经有的颜色创建
                                cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                                cellStyle.FillPattern = FillPattern.SolidForeground;
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForSecond(cell, i + 100, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }
                            continue;
                            #endregion
                        }

                        if (i == 6)
                        {
                            dataRow.HeightInPoints = 60;
                            #region   
                            for (int j = 0; j < 18; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i + 100, false);
                                //使用NPOI已经有的颜色创建
                                cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                                cellStyle.FillPattern = FillPattern.SolidForeground;
                                cellStyle.WrapText = true;//设置换行这个要先设置
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForSecond(cell, i + 100, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }

                            continue;
                            #endregion
                        }

                        if (i == 7)
                        {
                            #region  
                            for (int j = 0; j < 18; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 7, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i + 100, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForSecond(cell, i + 100, j, summary, null);
                                if (!(j == 11 || j == 12 || j == 13 || j == 14))
                                {
                                    cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                                    cellStyle.FillPattern = FillPattern.SolidForeground;
                                }
                                cell.CellStyle = cellStyle;
                            }
                            continue;
                            #endregion
                        }

                        if (i >= 8 && i <= 17)
                        {
                            for (int k = 0; k < summary.SeriesDemandsList.Count; k++)
                            {
                                if (i == (k + 8))
                                {
                                    dataRow.HeightInPoints = 35;
                                    #region   
                                    for (int j = 0; j < 18; j++)
                                    {
                                        ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Center, VerticalAlignment.Center,
                                            false, 700, true, BorderStyle.Thin, j, i + 100, false);
                                        var cell = dataRow.CreateCell(j);
                                        nPOIHelper.SetCellValueForSecond(cell, i + 100, j, summary, summary.SeriesDemandsList[k]);
                                        //  cellStyle.WrapText = true;//设置换行这个要先设置
                                        if (j == 9 || j == 10 || j == 1)
                                        {
                                            cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 7, HorizontalAlignment.Center, VerticalAlignment.Center,
                                            false, 700, true, BorderStyle.Thin, j, i + 100, false);
                                            cell.CellStyle.WrapText = true;//设置换行这个要先设置
                                        }
                                        else
                                        {
                                            cell.CellStyle = cellStyle;
                                        }
                                    }
                                    continue;
                                    #endregion
                                }
                            }

                            if (i >= (summary.SeriesDemandsList.Count + 8) && i <= 17)
                            {
                                #region  
                                for (int j = 0; j < 18; j++)
                                {
                                    ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Center, VerticalAlignment.Center,
                                        false, 700, true, BorderStyle.Thin, j, i + 100, false);
                                    var cell = dataRow.CreateCell(j);
                                    nPOIHelper.SetCellValueForSecond(cell, i + 100, j, summary, null);
                                    cell.CellStyle = cellStyle;
                                }
                                continue;
                                #endregion
                            }
                        }


                        if (i == 18)
                        {
                            dataRow.HeightInPoints = 25;
                            #region 
                            for (int j = 0; j < 18; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 11, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 100, false);
                                //使用NPOI已经有的颜色创建   0,1 ,5,6,9,10,14,15
                                if (j == 0 || j == 1 || j == 5 || j == 6 || j == 9 || j == 10 || j == 14 || j == 15)
                                {
                                    cellStyle2.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                                    cellStyle2.FillPattern = FillPattern.SolidForeground;
                                }
                                cellStyle2.WrapText = true;//设置换行这个要先设置
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForSecond(cell, i + 100, j, summary, null);
                                cell.CellStyle = cellStyle2;
                            }
                            //合并单元格

                            continue;
                            #endregion
                        }

                        if (i == 21)
                        {
                            #region //  
                            for (int j = 0; j < 18; j++)
                            {
                                ICellStyle cellStyle2 = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 11, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    false, 700, false, BorderStyle.Thin, j, i + 100, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForSecond(cell, i + 100, j, summary, null);
                                cell.CellStyle = cellStyle2;
                            }

                            continue;
                            #endregion
                        }

                        #endregion

                    }


                    #endregion
                }

                if (summary.ServiceDemandsList.Any())
                {
                    #region Thirdsheet


                    //  合并单元格
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 16));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 0, 3));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 11, 13));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 14, 16));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 0, 3));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 7, 8));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 11, 13));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 14, 16));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 7, 10));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 0, 1));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 2, 3));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 4, 6));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 7, 10));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 11, 13));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 14, 16));

                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(7, 7, 0, 8));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(7, 7, 9, 10));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(7, 7, 11, 14));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(13, 13, 9, 10));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(14, 14, 9, 10));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(15, 15, 9, 10));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(16, 16, 9, 10));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(17, 17, 9, 10));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(12, 12, 9, 10));

                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(11, 11, 9, 10));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(10, 10, 9, 10));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 9, 9, 10));

                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 0, 1));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 2, 4));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 5, 6));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 7, 8));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 9, 10));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 11, 13));
                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(18, 18, 14, 15));

                    Thirdsheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(21, 21, 0, 16));

                    Thirdsheet.SetColumnWidth(0, 2000);
                    Thirdsheet.SetColumnWidth(1, 4000);
                    Thirdsheet.SetColumnWidth(2, 1100);
                    Thirdsheet.SetColumnWidth(3, 1100);
                    Thirdsheet.SetColumnWidth(4, 1400);
                    Thirdsheet.SetColumnWidth(5, 1400);
                    Thirdsheet.SetColumnWidth(6, 2000);
                    Thirdsheet.SetColumnWidth(7, 2000);
                    Thirdsheet.SetColumnWidth(8, 1000);
                    Thirdsheet.SetColumnWidth(9, 2500);
                    Thirdsheet.SetColumnWidth(10, 2500);
                    Thirdsheet.SetColumnWidth(11, 1600);
                    Thirdsheet.SetColumnWidth(12, 1800);
                    Thirdsheet.SetColumnWidth(13, 2200);
                    Thirdsheet.SetColumnWidth(14, 2600);
                    Thirdsheet.SetColumnWidth(15, 1200);
                    Thirdsheet.SetColumnWidth(16, 5000);

                    for (int i = 0; i < 22; i++)
                    {
                        IRow dataRow = Thirdsheet.CreateRow(i);
                        dataRow.HeightInPoints = 22;

                        #region 第三页 (EN) Series Demands
                        if (i == 0)
                        {
                            #region     
                            for (int j = 0; j < 17; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i + 200, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForThird(cell, i + 200, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }
                            continue;
                            #endregion
                        }

                        if (i == 2)
                        {
                            dataRow.HeightInPoints = 18;
                            #region    
                            for (int j = 0; j < 17; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 11, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 200, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForThird(cell, i + 200, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }
                            continue;
                            #endregion
                        }

                        if (i == 3)
                        {
                            dataRow.HeightInPoints = 18;
                            #region    
                            for (int j = 0; j < 17; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 11, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 200, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForThird(cell, i + 200, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }
                            continue;
                            #endregion
                        }

                        if (i == 4)
                        {
                            dataRow.HeightInPoints = 18;
                            #region   
                            for (int j = 0; j < 17; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 11, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 200, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForThird(cell, i + 200, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }
                            continue;
                            #endregion
                        }

                        if (i == 5)
                        {
                            dataRow.HeightInPoints = 18;
                            dataRow.HeightInPoints = 15;
                            #region   
                            for (int j = 0; j < 17; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 11, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 200, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForThird(cell, i + 200, j, summary, null);
                                if (j == 4)
                                {
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 11, HorizontalAlignment.Right, VerticalAlignment.Center,
                                    true, 700, false, BorderStyle.Thin, j, i + 200, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle;
                                }
                            }
                            continue;
                            #endregion
                        }

                        if (i == 6)
                        {
                            dataRow.HeightInPoints = 18;
                            #region   
                            for (int j = 0; j < 17; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 11, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 200, false);
                                var cell = dataRow.CreateCell(j);
                                cell.CellStyle = cellStyle;
                            }
                            continue;
                            #endregion
                        }

                        if (i == 7)
                        {
                            #region   
                            for (int j = 0; j < 17; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 200, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForThird(cell, i + 200, j, summary, null);
                                //使用NPOI已经有的颜色创建
                                cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                                cellStyle.FillPattern = FillPattern.SolidForeground;
                                if (j == 4)
                                {
                                    cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Center, VerticalAlignment.Center,
                                     true, 700, true, BorderStyle.Thin, j, i + 200, false);
                                }
                                else
                                {
                                    cell.CellStyle = cellStyle;
                                }
                            }
                            continue;
                            #endregion
                        }

                        if (i == 8)
                        {
                            dataRow.HeightInPoints = 60;
                            #region   
                            for (int j = 0; j < 17; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    false, 700, true, BorderStyle.Thin, j, i + 200, false);

                                //使用NPOI已经有的颜色创建
                                cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                                cellStyle.FillPattern = FillPattern.SolidForeground;
                                cellStyle.WrapText = true;//设置换行这个要先设置
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForThird(cell, i + 200, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }
                            continue;
                            #endregion
                        }

                        if (i >= 9 && i <= 17)
                        {
                            for (int k = 0; k < summary.ServiceDemandsList.Count; k++)
                            {
                                if (i == (k + 9))
                                {
                                    dataRow.HeightInPoints = 35;
                                    #region   
                                    for (int j = 0; j < 17; j++)
                                    {
                                        ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Center, VerticalAlignment.Center,
                                   false, 700, true, BorderStyle.Thin, j, i + 200, false);
                                        var cell = dataRow.CreateCell(j);

                                        nPOIHelper.SetCellValueForThird(cell, i + 200, j, summary, summary.ServiceDemandsList[k]);
                                        if (j == 9 || j == 10)
                                        {
                                            cell.CellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 7, HorizontalAlignment.Center, VerticalAlignment.Center,
                                            false, 700, true, BorderStyle.Thin, j, i + 200, false);
                                            cell.CellStyle.WrapText = true;//设置换行这个要先设置
                                        }
                                        else
                                        {
                                            cell.CellStyle = cellStyle;
                                        }
                                    }
                                    continue;
                                    #endregion
                                }
                            }

                            if (i >= (summary.SeriesDemandsList.Count + 9) && i <= 17)
                            {
                                #region  
                                for (int j = 0; j < 17; j++)
                                {
                                    ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 8, HorizontalAlignment.Center, VerticalAlignment.Center,
                                        false, 700, true, BorderStyle.Thin, j, i + 200, false);
                                    var cell = dataRow.CreateCell(j);
                                    nPOIHelper.SetCellValueForThird(cell, i + 200, j, summary, null);
                                    cell.CellStyle = cellStyle;
                                }
                                continue;
                                #endregion
                            }
                        }

                        if (i == 18)
                        {
                            dataRow.HeightInPoints = 25;
                            #region   
                            for (int j = 0; j < 17; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 11, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    true, 700, true, BorderStyle.Thin, j, i + 200, false);
                                //使用NPOI已经有的颜色创建   0,1 ,5,6,9,10,14,15
                                if (j == 0 || j == 1 || j == 5 || j == 6 || j == 9 || j == 10 || j == 14 || j == 15)
                                {
                                    cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                                    cellStyle.FillPattern = FillPattern.SolidForeground;
                                }
                                cellStyle.WrapText = true;//设置换行这个要先设置
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForThird(cell, i + 200, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }
                            continue;
                            #endregion
                        }


                        if (i == 21)
                        {
                            #region   
                            for (int j = 0; j < 17; j++)
                            {
                                ICellStyle cellStyle = nPOIHelper.SetCellFont(hssfworkbook, dataRow, 10, HorizontalAlignment.Center, VerticalAlignment.Center,
                                    false, 700, false, BorderStyle.Thin, j, i + 200, false);
                                var cell = dataRow.CreateCell(j);
                                nPOIHelper.SetCellValueForThird(cell, i + 200, j, summary, null);
                                cell.CellStyle = cellStyle;
                            }
                            continue;
                            #endregion
                        }


                        #endregion

                    }
                    #endregion
                }
                return WirteToFile(hssfworkbook, excelFilePath, noRemoveWatermarkPdfFilePath, removeWatermarkPdfFilePath);

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 将数据写入Excel文件
        /// </summary>
        /// <param name="hssfworkbook">工作簿</param>
        /// <param name="exportPath">文件导出路径</param>
        private static bool WirteToFile(HSSFWorkbook hssfworkbook, string excelFilePath, string noRemoveWatermarkPdfFilePath, string removeWatermarkPdfFilePath)
        {
            FileStream file = null;
            try
            {
                if (!Directory.Exists(excelFilePath.Replace("/output.xls", "")))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(excelFilePath.Replace("/output.xls", ""));
                    directoryInfo.Create();
                }

                if (File.Exists(excelFilePath))
                {
                    //存在 
                    File.Delete(excelFilePath);
                }
                file = new FileStream(excelFilePath, FileMode.Create);
                hssfworkbook.Write(file);

                //ExcelToPDF(summary, excelFilePath, noRemoveWatermarkPdfFilePath, removeWatermarkPdfFilePath);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                file.Dispose();
                hssfworkbook.Close();
            }

        }

        /// <summary>
        /// excel转PDF
        /// </summary>
        /// <param name="exportPath"></param>
        public static void ExcelToPDF(Summary summary, string excelFilePath, string noRemoveWatermarkPdfFilePath, string removeWatermarkPdfFilePath)
        {
            //前面是用NPOI处理导出的数据 using Aspose.Cells;  有水印
            Console.WriteLine("exceltopdf");
            Workbook wb = new Workbook(excelFilePath);

            wb.Save(noRemoveWatermarkPdfFilePath, SaveFormat.Pdf);

            RemoveWatermark(summary, noRemoveWatermarkPdfFilePath, removeWatermarkPdfFilePath);

            //7、打开刚才创建的文件
            //Process process = new Process();
            //ProcessStartInfo processStartInfo = new ProcessStartInfo(removeWatermarkPdfFilePath);
            //process.StartInfo = processStartInfo;
            //process.StartInfo.UseShellExecute = true;
            //process.Start();
            //Console.WriteLine("Success");
        }

        /// <summary>
        /// 去除水印
        /// </summary>
        /// <param name="sourcePDF"></param>
        /// <param name="targetPDF"></param>
        public static void RemoveWatermark(Summary summary, string sourcePDF, string targetPDF)
        {

            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(sourcePDF);

            if (summary.Project.State == "Service Done")
            {
                doc.Pages.RemoveAt(1);
            }

            if (summary.Project.State == "Series Done")
            {
                doc.Pages.RemoveAt(2);
            }
            for (int i = 0; i < doc.Pages.Count; i++)
            {
                PdfPageBase page = doc.Pages[i];
                PdfTextFindCollection collection = page.FindText("Evaluation Only. Created with Aspose.Cells for .NET.Copyright 2003 - 2023 Aspose Pty Ltd.", TextFindParameter.IgnoreCase);

                String newText = "Evaluation Only. Created with Aspose.Cells for .NET.Copyright 2003 - 2023 Aspose Pty Ltd.";
                //Creates a brush
                PdfBrush brush = new PdfSolidBrush(Color.DarkBlue);
                //Defines a font
                PdfTrueTypeFont font = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 12f, FontStyle.Regular));
                RectangleF rec;
                foreach (PdfTextFind find in collection.Finds)
                {
                    // Gets the bound of the found text in page
                    rec = find.Bounds;
                    page.Canvas.DrawRectangle(PdfBrushes.White, rec);
                    page.Canvas.DrawString(newText, font, brush, rec);
                }
            }


            doc.SaveToFile(targetPDF);
            doc.Close();
        }



        /// <summary>
        /// 模版填充数据生成pdf
        /// </summary>
        /// <param name="summary"></param>
        /// <param name="excelFilePath"></param>
        /// <param name="noRemoveWatermarkPdfFilePath"></param>
        /// <param name="removeWatermarkPdfFilePath"></param>
        public static string TeamplateFillDataToPDF(Summary summary, string excelFilePath, string noRemoveWatermarkPdfFilePath, string removeWatermarkPdfFilePath)
        {
            try
            {

                #region  excel中 填充数据

                // 第一步：读取文件流
                IWorkbook workbook;
                using (FileStream stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(stream);
                }
                NPOIHelper nPOIHelper = new NPOIHelper();
                #region FitstSheet 
                ISheet Firstsheet = workbook.GetSheetAt(0);

                for (int i = 0; i < 23; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        if (i == 0 || i == 1 || i == 2 || i == 0 || i == 4 || i == 5 || i == 8 || i == 9 || i == 11 || i == 18 || i == 20 || i == 21 || i == 22)
                        {
                            // 填充单元格内容
                            nPOIHelper.SetFillCellValue(workbook, Firstsheet.GetRow(i).GetCell(j), i, j, summary, null, null);
                        }
                    }
                }
                #endregion

                #region SecondSheet 
                ISheet secondSheet = workbook.GetSheetAt(1);
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 18; j++)
                    {
                        if (i == 0 || i == 15)
                        {
                            // 填充单元格内容
                            nPOIHelper.SetFillCellValue(workbook, secondSheet.GetRow(i).GetCell(j), i + 100, j, summary, null, null);
                        }

                        if (i >= 5 && i <= 14)
                        {
                            for (int k = 0; k < summary.SeriesDemandsList.Count; k++)
                            {
                                if (i == (k + 5))
                                {
                                    #region   
                                    nPOIHelper.SetFillCellValue(workbook, secondSheet.GetRow(i).GetCell(j), i + 100, j, summary, summary.SeriesDemandsList[k], null);
                                    continue;
                                    #endregion
                                }
                            }

                            if (i >= (summary.SeriesDemandsList.Count + 5) && i <= 14)
                            {
                                nPOIHelper.SetFillCellValue(workbook, secondSheet.GetRow(i).GetCell(j), i + 100, j, summary, null, null);
                            }
                        }
                    }
                }
                #endregion

                #region ThirdSheet 
                ISheet thirdSheet = workbook.GetSheetAt(2);
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 17; j++)
                    {
                        if (i == 0 || i == 1 || i == 15)
                        {
                            // 填充单元格内容
                            nPOIHelper.SetFillCellValue(workbook, thirdSheet.GetRow(i).GetCell(j), i + 200, j, summary, null, null);
                        }

                        if (i >= 6 && i <= 15)
                        {
                            for (int k = 0; k < summary.ServiceDemandsList.Count; k++)
                            {
                                if (i == (k + 6))
                                {
                                    #region   
                                    nPOIHelper.SetFillCellValue(workbook, thirdSheet.GetRow(i).GetCell(j), i + 200, j, summary, null, summary.ServiceDemandsList[k]);
                                    continue;
                                    #endregion
                                }
                            }

                            if (i >= (summary.ServiceDemandsList.Count + 6) && i <= 15)
                            {
                                nPOIHelper.SetFillCellValue(workbook, thirdSheet.GetRow(i).GetCell(j), i + 200, j, summary, null, null);
                            }
                        }
                    }
                }
                #endregion

                using (FileStream stream = new FileStream(excelFilePath, FileMode.Create, FileAccess.Write))
                {
                    Console.WriteLine("5");
                    workbook.Write(stream, false);
                    workbook.Close();
                }
                ExcelToPDF(summary, excelFilePath, noRemoveWatermarkPdfFilePath, removeWatermarkPdfFilePath);

                #endregion
                return true + "_" + removeWatermarkPdfFilePath.Split("public")[1];
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
