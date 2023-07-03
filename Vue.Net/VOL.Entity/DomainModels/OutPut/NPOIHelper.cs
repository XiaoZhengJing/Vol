using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace VOL.Entity.DomainModels.OutPut
{
    public class NPOIHelper
    {

        public IFont font { get; set; }

        /// <summary>
        /// 设置单元格字体大小（FontHeightInPoints），字体样式,位置（HorizontalAlignment），边框
        /// </summary>
        public ICellStyle SetCellFont(HSSFWorkbook hssfworkbook, IRow dataRow, double FontHeightInPoints, HorizontalAlignment horizontalAlignment,
         VerticalAlignment verticalAlignment, bool isBold, short boldweightsize, bool IsBorder, BorderStyle borderStyle, int col, int rowNum, bool isItalic)
        {
            ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
            cellStyle.Alignment = horizontalAlignment;
            cellStyle.VerticalAlignment = verticalAlignment;

            // 为了解决 Maximum number of fonts was exceeded，看源码CreateFont() 方法 ， Int16 MaxValue = 32767
            font = hssfworkbook.CreateFont();
            font.IsItalic = isItalic;
            font.FontHeightInPoints = FontHeightInPoints;

            if (isBold)
                font.Boldweight = boldweightsize;
            cellStyle.SetFont(font);

            // 设置边框
            if (IsBorder)
                SetBorderStyle(cellStyle, rowNum, col, borderStyle);

            return cellStyle;
        }

        /// <summary>
        /// 设置边框
        /// </summary>
        /// <param name="cellStyle"></param>
        /// <param name="rowNum"></param>
        /// <param name="col"></param>
        /// <param name="borderStyle"></param>
        public void SetBorderStyle(ICellStyle cellStyle, int rowNum, int col, BorderStyle borderStyle)
        {
            switch (rowNum)
            {
                case 2:
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 3:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 4:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 5:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 6:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 7:
                    cellStyle.BorderBottom = borderStyle;
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 8:
                    cellStyle.BorderTop = borderStyle;
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 9:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 10:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 11:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 12:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 13:
                    cellStyle.BorderTop = borderStyle;
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 14:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 15:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 16:
                    cellStyle.BorderBottom = borderStyle;
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 12)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 18:
                    if (col == 2 || col == 3 || col == 5 || col == 6 || col == 8 || col == 11 || col == 12)
                        cellStyle.BorderBottom = borderStyle;
                    break;
                case 20:
                    if (col == 2 || col == 3 || col == 5 || col == 6 || col == 8 || col == 11 || col == 12)
                        cellStyle.BorderBottom = borderStyle;
                    break;
                case 22:
                    if (col == 2 || col == 3 || col == 5 || col == 6 || col == 8 || col == 11 || col == 12)
                        cellStyle.BorderBottom = borderStyle;
                    break;
                case 102:
                    cellStyle.BorderTop = borderStyle;
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 17)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 103:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 17)
                        cellStyle.BorderRight = borderStyle;
                    if (col == 5 || col == 7 || col == 10 || col == 14 || col == 15 || col == 16 || col == 17)
                        cellStyle.BorderBottom = borderStyle;
                    break;
                case 104:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 17)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 105:
                    if (col != 9 || col != 10)
                        cellStyle.BorderTop = borderStyle;
                    if (col != 10)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 16 || col == 8 || col == 10 || col == 17)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 106:
                    if (col != 9 && col != 10 && col != 16 && col != 17)
                        cellStyle.BorderTop = borderStyle;
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col != 9)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 107:
                    if (col == 11 || col == 12 || col == 13 || col == 14)
                        cellStyle.BorderTop = borderStyle;
                    if (col != 10)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 17 || col == 8 || col == 10)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 108:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 109:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 110:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 111:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 112:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 113:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 114:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 115:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 116:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 117:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 118:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 202:
                    cellStyle.BorderTop = borderStyle;
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 16)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 203:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 16)
                        cellStyle.BorderRight = borderStyle;
                    if (col == 8 || col == 7 || col == 10 || col == 14 || col == 15 || col == 16 || col == 17)
                        cellStyle.BorderBottom = borderStyle;
                    break;
                case 204:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 16)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 205:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 16)
                        cellStyle.BorderRight = borderStyle;
                    if (col == 2 || col == 3 || col == 7 || col == 8 || col == 9 || col == 10 || col == 16 || col == 15 || col == 14)
                        cellStyle.BorderBottom = borderStyle;
                    break;
                case 206:
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 16)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 207:
                    if (col != 9 || col != 10)
                        cellStyle.BorderTop = borderStyle;
                    if (col != 10)
                        cellStyle.BorderLeft = borderStyle;
                    if (col == 16 || col == 8 || col == 10)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 208:
                    if (col != 9 && col != 10 && col != 15 && col != 16)
                        cellStyle.BorderTop = borderStyle;
                    if (col == 0)
                        cellStyle.BorderLeft = borderStyle;
                    if (col != 9)
                        cellStyle.BorderRight = borderStyle;
                    break;
                case 209:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderTop = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 210:
                    cellStyle.BorderRight = borderStyle;

                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 211:
                    cellStyle.BorderRight = borderStyle;

                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 212:
                    cellStyle.BorderRight = borderStyle;

                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 213:
                    cellStyle.BorderRight = borderStyle;

                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 214:
                    cellStyle.BorderRight = borderStyle;

                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 215:
                    cellStyle.BorderRight = borderStyle;

                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 216:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 217:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
                case 218:
                    cellStyle.BorderRight = borderStyle;
                    cellStyle.BorderLeft = borderStyle;
                    cellStyle.BorderBottom = borderStyle;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hssfworkbook"></param>
        /// <param name="borderStyle"></param>
        public void RemoveBorderStyle(IWorkbook workbook, ICell cell)
        {
            var cellStyle = workbook.CreateCellStyle();
            cellStyle.BorderBottom = BorderStyle.None;
            cell.CellStyle = cellStyle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="cell"></param>
        public void AddBorderStyle(IWorkbook workbook, ICell cell)
        {
            var cellStyle = workbook.CreateCellStyle();
            cellStyle.BorderBottom = BorderStyle.Thin;
            cell.CellStyle = cellStyle;
        }

        /// <summary>
        ///  填充单元格内容
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="rowNum"></param>
        public void SetCellValue(ICell cell, int rowNum, int col, Summary summary)
        {
            switch (rowNum)
            {
                case 2:
                    if (col == 0)
                        cell.SetCellValue("Determination of Demand (temporary/final) for Business Line:");
                    if (col == 9)
                        cell.SetCellValue(summary.Project.BL);
                    break;
                case 3:
                    if (col == 0)
                        cell.SetCellValue("Discontinuation Nr.:");
                    if (col == 2)
                        cell.SetCellValue(summary.Project.PDN);
                    if (col == 6)
                        cell.SetCellValue("Manufacturer:");
                    if (col == 8)
                        cell.SetCellValue(summary.Project.Manufacturer);
                    break;
                case 4:
                    if (col == 0)
                        cell.SetCellValue("Part number:");
                    if (col == 2)
                        cell.SetCellValue(summary.Project.SPN);
                    if (col == 6)
                        cell.SetCellValue("Description");
                    if (col == 8)
                        cell.SetCellValue(summary.Project.ObsoleteMaterialDescription);
                    break;
                case 6:
                    if (col == 0)
                        cell.SetCellValue("Affected BLs:");
                    if (col == 2)
                        cell.SetCellValue(summary.Project.AffectedBLs);
                    if (col == 6)
                        cell.SetCellValue("Stock up start date:");
                    if (col == 8)
                        cell.SetCellValue(summary.Project.StockUpStartdate);
                    break;
                case 7:
                    if (col == 0)
                        cell.SetCellValue("Responsible BL:");
                    if (col == 2)
                        cell.SetCellValue(summary.Project.BL);
                    if (col == 6)
                        cell.SetCellValue("Moving price:");
                    if (col == 8)
                        cell.SetCellValue(summary.Project.Price);
                    break;
                case 8:
                    if (col == 0)
                        cell.SetCellValue("The risk of rejection relates to the entire obsolete component");
                    break;
                case 9:
                    if (col == 0)
                        cell.SetCellValue("Series demand:");
                    if (col == 2)
                        cell.SetCellValue(summary.Project.TotalSeriesDemand);
                    if (col == 6)
                        cell.SetCellValue("Series costs:");
                    if (col == 8)
                        cell.SetCellValue(summary.Project.TotalSeriesCosts);
                    break;
                case 10:
                    if (col == 0)
                        cell.SetCellValue("Service demand:");
                    if (col == 2)
                        cell.SetCellValue(summary.Project.TotalServiceDemand);
                    if (col == 6)
                        cell.SetCellValue("Service costs:");
                    if (col == 8)
                        cell.SetCellValue(summary.Project.TotalServiceCosts);
                    break;
                case 11:
                    if (col == 0)
                        cell.SetCellValue("Customer Service involved:");
                    if (col == 3)
                        cell.SetCellValue("Y");
                    if (col == 6)
                        cell.SetCellValue("Service demands agreed with:");
                    if (col == 10)
                        cell.SetCellValue("CS ML");
                    break;
                case 12:
                    if (col == 0)
                        cell.SetCellValue("Total demand:");
                    if (col == 2)
                        cell.SetCellValue(summary.Project.totalDemand);
                    if (col == 6)
                        cell.SetCellValue("Total costs:");
                    if (col == 8)
                        cell.SetCellValue(summary.Project.totalCosts);
                    break;
                case 13:
                    if (col == 0)
                        cell.SetCellValue("○ The risk of rejection relates to the following obsolete sub-components only");
                    break;
                case 14:
                    if (col == 0)
                        cell.SetCellValue("Part Nr.:");
                    if (col == 2)
                        cell.SetCellValue("Description:");
                    if (col == 6)
                        cell.SetCellValue("Price/Pcs.:");
                    if (col == 8)
                        cell.SetCellValue("Number/Comp.:");
                    if (col == 10)
                        cell.SetCellValue("Costs:");
                    if (col == 11)
                        cell.SetCellValue("0.00€");
                    break;
                case 15:
                    if (col == 0)
                        cell.SetCellValue("Part Nr.:");
                    if (col == 2)
                        cell.SetCellValue("Description:");
                    if (col == 6)
                        cell.SetCellValue("Price/Pcs.:");
                    if (col == 8)
                        cell.SetCellValue("Number/Comp.:");
                    if (col == 10)
                        cell.SetCellValue("Costs:");
                    if (col == 11)
                        cell.SetCellValue("0.00€");
                    break;
                case 16:
                    if (col == 6)
                        cell.SetCellValue("Total costs:");
                    if (col == 9)
                        cell.SetCellValue("0.00€");
                    break;
                case 18:
                    if (col == 0)
                        cell.SetCellValue("Created:");
                    if (col == 1)
                        cell.SetCellValue("Department:");
                    if (col == 2)
                        cell.SetCellValue(summary.Project.CreatederDepartment);
                    if (col == 4)
                        cell.SetCellValue("Name:");
                    if (col == 5)
                        cell.SetCellValue(summary.Project.Createder);
                    if (col == 7)
                        cell.SetCellValue("Date:");
                    if (col == 8)
                        cell.SetCellValue(summary.Project.CreateDate);
                    if (col == 10)
                        cell.SetCellValue("Signature:");
                    break;
                case 20:
                    if (col == 0)
                        cell.SetCellValue("Approval*:");
                    if (col == 1)
                        cell.SetCellValue("Department:");
                    if (col == 2)
                        cell.SetCellValue(summary.Project.ApprovalerDepartment1);
                    if (col == 4)
                        cell.SetCellValue("Name:");
                    if (col == 5)
                        cell.SetCellValue(summary.Project.Approvaler1);
                    if (col == 7)
                        cell.SetCellValue("Date:");
                    if (col == 8)
                        cell.SetCellValue(summary.Project.ApprovalDate1);
                    if (col == 10)
                        cell.SetCellValue("Signature:");
                    break;
                case 22:
                    if (col == 0)
                        cell.SetCellValue("Approval*:");
                    if (col == 1)
                        cell.SetCellValue("Department:");
                    if (col == 2)
                        cell.SetCellValue(summary.Project.ApprovalerDepartment2);
                    if (col == 4)
                        cell.SetCellValue("Name:");
                    if (col == 5)
                        cell.SetCellValue(summary.Project.Approvaler2);
                    if (col == 7)
                        cell.SetCellValue("Date:");
                    if (col == 8)
                        cell.SetCellValue(summary.Project.ApprovalDate2);
                    if (col == 10)
                        cell.SetCellValue("Signature:");
                    break;
                case 24:
                    if (col == 0)
                        cell.SetCellValue("*if necessary please insert additional lines for approval");
                    break;
                case 25:
                    if (col == 0)
                        cell.SetCellValue("imits for approval:");
                    break;
                case 26:
                    if (col == 0)
                        cell.SetCellValue("< 5000 EUR: Coordinator BL");
                    break;
                case 27:
                    if (col == 0)
                        cell.SetCellValue("> 5000 EUR: SCM Management BL");
                    break;
                case 28:
                    if (col == 0)
                        cell.SetCellValue("> 50000 EUR: Business Administration BL");
                    break;
                case 29:
                    if (col == 0)
                        cell.SetCellValue("> 500000 EUR: acc.to directive MOR 2.2 / 11");
                    break;
                case 33:
                    if (col == 0)
                        cell.SetCellValue("11474_SPN10548267_Oscil.SMD 106.25.MHz LVDS 50ppm 2V5_10.01.2023_18:09:44");
                    break;

            }
        }

        /// <summary>
        ///  填充单元格内容
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="rowNum"></param>
        public void SetCellValueForSecond(ICell cell, int rowNum, int col, Summary summary, SeriesDemands seriesDemands)
        {
            switch (rowNum)
            {
                case 100:
                    if (col == 0)
                        cell.SetCellValue("Determination of Demand acc. to MFR 190 / 005");
                    break;
                case 102:
                    if (col == 0)
                        cell.SetCellValue("Determination of Series Demand");
                    if (col == 11)
                        cell.SetCellValue("Description");
                    if (col == 14)
                        cell.SetCellValue(summary.Project.ObsoleteMaterialDescription.Replace("50ppm 2V5", ""));

                    break;
                case 103:
                    if (col == 0)
                        cell.SetCellValue("(temporary/final)");
                    if (col == 4)
                        cell.SetCellValue("BL:");
                    if (col == 5)
                        cell.SetCellValue(summary.Project.BL);
                    if (col == 6)
                        cell.SetCellValue("PDN:");
                    if (col == 7)
                        cell.SetCellValue(summary.Project.PDN);
                    if (col == 9)
                        cell.SetCellValue("SPN:");
                    if (col == 10)
                        cell.SetCellValue(summary.Project.SPN);
                    if (col == 11)
                        cell.SetCellValue("obsolete material:");
                    if (col == 14)
                        cell.SetCellValue("50ppm 2V5");
                    break;
                case 105:
                    if (col == 0)
                        cell.SetCellValue("Obsolescence Tool");
                    if (col == 9)
                        cell.SetCellValue("Project");
                    if (col == 11)
                        cell.SetCellValue("Required quantity");
                    break;
                case 106:
                    if (col == 0)
                        cell.SetCellValue("Counter\n Material");
                    if (col == 1)
                        cell.SetCellValue("Description\r\n    Counter Material");
                    if (col == 2)
                        cell.SetCellValue("Plant");
                    if (col == 3)
                        cell.SetCellValue("sen-\nding\n Plant");
                    if (col == 4)
                        cell.SetCellValue("Custo-\nmer");
                    if (col == 5)
                        cell.SetCellValue("Count\ner \nType");
                    if (col == 6)
                        cell.SetCellValue("on stock\n     from:");
                    if (col == 7)
                        cell.SetCellValue("on stock\nuntil:");
                    if (col == 8)
                        cell.SetCellValue("co-\nver-\nage\ntype");
                    if (col == 11)
                        cell.SetCellValue("Plan rest of current FY");
                    if (col == 12)
                        cell.SetCellValue("Plan 1st\nfollowing FY");
                    if (col == 13)
                        cell.SetCellValue("Plan 2nd\nfollowing FY");
                    if (col == 14)
                        cell.SetCellValue("Plan 3rd\nfollowing FY");
                    if (col == 15)
                        cell.SetCellValue("Sum Plan");
                    if (col == 16)
                        cell.SetCellValue("Com-\npo-nent\nquan-\ntity");
                    if (col == 17)
                        cell.SetCellValue("Total Demand\n =Risk of \nRejection");
                    break;
                case 107:
                    if (col == 11)
                        cell.SetCellValue("2022 / 2023 ");
                    if (col == 12)
                        cell.SetCellValue("2023 / 2024");
                    if (col == 13)
                        cell.SetCellValue("2024 / 2025");
                    if (col == 14)
                        cell.SetCellValue("2025 / 2026");
                    break;
                case 108:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 109:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 110:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 111:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;


                case 112:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 113:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 114:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 115:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 116:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 117:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 118:
                    if (col == 0)
                        cell.SetCellValue("Stock up\nstart date:");
                    if (col == 2)
                        cell.SetCellValue("2022-10-17");
                    if (col == 5)
                        cell.SetCellValue("Price / Pcs.:");
                    if (col == 7)
                        cell.SetCellValue(summary.Project.Price);
                    if (col == 9)
                        cell.SetCellValue("Total Series\nCosts:");
                    if (col == 11)
                        cell.SetCellValue(summary.Project.TotalSeriesCosts);
                    if (col == 14)
                        cell.SetCellValue("Total Serien\nDemand:");
                    if (col == 16)
                        cell.SetCellValue(summary.Project.TotalSeriesDemand);
                    break;
                case 121:
                    if (col == 0)
                        cell.SetCellValue("11474_SPN10548267_Oscil.SMD 106.25.MHz LVDS 50ppm 2V5_10.01.2023_18:09:44");
                    break;
            }
        }

        /// <summary>
        /// 设置数据 SeriesDemands seriesDemands
        /// </summary>
        /// <param name="seriesDemands"></param>
        public void setCellValueForSecondErgodic(ICell cell, Summary summary, SeriesDemands seriesDemands, int rowNum, int col)
        {
            if (col == 0)
                cell.SetCellValue(seriesDemands?.CounterMaterial);
            if (col == 1)
                cell.SetCellValue(seriesDemands?.CounterMaterialDescription);
            if (col == 2)
                cell.SetCellValue(seriesDemands?.PlantID);
            if (col == 3)
                cell.SetCellValue(seriesDemands?.SendingPlantID);
            if (col == 4)
                cell.SetCellValue(seriesDemands?.Customer);
            if (col == 5)
                cell.SetCellValue(seriesDemands?.CounterTypeID.ToString());
            if (col == 6)
                cell.SetCellValue(seriesDemands?.OnStockFrom);
            if (col == 7)
                cell.SetCellValue(seriesDemands?.OnStockUntil);
            if (col == 8)
                cell.SetCellValue(seriesDemands?.CoverageType);
            if (col == 9)
                cell.SetCellValue(seriesDemands?.ProjectName);
            if (col == 11)
                cell.SetCellValue(seriesDemands?.PlanRestOfCurrentFY);
            if (col == 12)
                cell.SetCellValue(seriesDemands?.PlanFirstFollowingFY);
            if (col == 13)
                cell.SetCellValue(seriesDemands?.PlanSecondFollowingFY);
            if (col == 14)
                cell.SetCellValue(seriesDemands?.PlanThirdFollowingFY);

            if (rowNum < (summary.SeriesDemandsList.Count + 105))
            {
                if (col == 15)
                    cell.SetCellValue(seriesDemands?.SumPlan);
                if (col == 16)
                    cell.SetCellValue(seriesDemands?.ComponentQuantity);
                if (col == 17)
                    cell.SetCellValue(seriesDemands?.TotalDemand);
            }
            else
            {
                if (col == 15)
                    cell.SetCellValue("0");
                if (col == 16)
                    cell.SetCellValue("1");
                if (col == 17)
                    cell.SetCellValue("0");
            }

        }

        /// <summary>
        ///  填充单元格内容
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="rowNum"></param>
        public void SetCellValueForThird(ICell cell, int rowNum, int col, Summary summary, ServiceDemands serviceDemands)
        {
            switch (rowNum)
            {
                case 200:
                    if (col == 0)
                        cell.SetCellValue("Tabelle zur Bedarfsermittlung MFR 190 / 005");
                    break;
                case 202:
                    if (col == 0)
                        cell.SetCellValue("Determination of Series Demand");
                    if (col == 11)
                        cell.SetCellValue("Description obsolete");
                    if (col == 14)
                        cell.SetCellValue(summary.Project.ObsoleteMaterialDescription.Replace("50ppm 2V5", ""));
                    break;
                case 204:
                    if (col == 7)
                        cell.SetCellValue(summary.Project.AffectedBLs);
                    break;
                case 203:
                    if (col == 0)
                        cell.SetCellValue("(temporary/final)");
                    if (col == 6)
                        cell.SetCellValue("PDN:");
                    if (col == 7)
                        cell.SetCellValue(summary.Project.PDN);
                    if (col == 9)
                        cell.SetCellValue("SPN:");
                    if (col == 10)
                        cell.SetCellValue(summary.Project.SPN);
                    if (col == 11)
                        cell.SetCellValue("material:");
                    if (col == 14)
                        cell.SetCellValue("50ppm 2V5");
                    break;
                case 205:
                    if (col == 0)
                        cell.SetCellValue("Responsible BL:");
                    if (col == 2)
                        cell.SetCellValue(summary.Project.BL);
                    if (col == 4)
                        cell.SetCellValue("Affected BLs:");
                    if (col == 7)
                        cell.SetCellValue("SSME CT, -ME-");
                    if (col == 11)
                        cell.SetCellValue("Manufacturer:");
                    if (col == 14)
                        cell.SetCellValue(summary.Project.Manufacturer);
                    break;
                case 207:
                    if (col == 0)
                        cell.SetCellValue("Obsolescence Tool");
                    if (col == 9)
                        cell.SetCellValue("Project");
                    if (col == 11)
                        cell.SetCellValue("Spare part information");
                    break;

                case 208:
                    if (col == 0)
                        cell.SetCellValue("Counter \n Material");
                    if (col == 1)
                        cell.SetCellValue("Description   \nCounter Material");
                    if (col == 2)
                        cell.SetCellValue("Plant");
                    if (col == 3)
                        cell.SetCellValue("sen-\nding\nPlant");
                    if (col == 4)
                        cell.SetCellValue("Custo-\nmer");
                    if (col == 5)
                        cell.SetCellValue("Coun-\nter\nType");
                    if (col == 6)
                        cell.SetCellValue("on stock \nfrom:");
                    if (col == 7)
                        cell.SetCellValue("on stock \nuntil:");
                    if (col == 8)
                        cell.SetCellValue("co-\nver-\nage \ntype");
                    if (col == 11)
                        cell.SetCellValue("Repair-\npart\nyes \n / no");
                    if (col == 12)
                        cell.SetCellValue("# of \n repairs\n until EOS /\n ESD ");
                    if (col == 13)
                        cell.SetCellValue("EOS / ESD\nEnd of\n(extended)\nService");
                    if (col == 14)
                        cell.SetCellValue("Sum Service\n Demand");
                    if (col == 15)
                        cell.SetCellValue("Com-\npo-nent-quan-\ntity");
                    if (col == 16)
                        cell.SetCellValue("Total Demand \n = \n Risk of Rejection");
                    break;
                case 209:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 210:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 211:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 212:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 213:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 214:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 215:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 216:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 217:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 218:
                    if (col == 0)
                        cell.SetCellValue("Stock up \n start date:");
                    if (col == 2)
                        cell.SetCellValue("2022-10-17");
                    if (col == 5)
                        cell.SetCellValue("Price / Pcs.:");
                    if (col == 7)
                        cell.SetCellValue(summary.Project.Price);
                    if (col == 9)
                        cell.SetCellValue("Total  Service   \n Costs:");
                    if (col == 11)
                        cell.SetCellValue(summary.Project.TotalServiceCosts);
                    if (col == 14)
                        cell.SetCellValue("Total  Service   \n Demand:");
                    if (col == 16)
                        cell.SetCellValue(summary.Project.TotalServiceDemand);
                    break;
                case 219:
                    if (col == 0)
                        cell.SetCellValue("start date:");
                    if (col == 9)
                        cell.SetCellValue("Costs:");
                    if (col == 14)
                        cell.SetCellValue("Demand:");

                    break;
                case 221:
                    if (col == 0)
                        cell.SetCellValue("11474_SPN10548267_Oscil.SMD 106.25.MHz LVDS 50ppm 2V5_10.01.2023_18:09:46");
                    break;

            }
        }

        /// <summary>
        /// 设置数据 SeriesDemands seriesDemands
        /// </summary>
        /// <param name="seriesDemands"></param>
        public void SetCellValueForThirdErgodic(ICell cell, Summary summary, ServiceDemands serviceDemands, int rowNum, int col)
        {
            if (col == 0)
                cell.SetCellValue(serviceDemands?.CounterMaterial);
            if (col == 1)
                cell.SetCellValue(serviceDemands?.CounterMaterialDescription);
            if (col == 2)
                cell.SetCellValue(serviceDemands?.PlantID);
            if (col == 3)
                cell.SetCellValue(serviceDemands?.SendingPlantID);
            if (col == 4)
                cell.SetCellValue(serviceDemands?.Customer);
            if (col == 5)
                cell.SetCellValue(serviceDemands?.CounterTypeID.ToString());
            if (col == 6)
                cell.SetCellValue(serviceDemands?.OnStockFrom);
            if (col == 7)
                cell.SetCellValue(serviceDemands?.OnStockUntil);
            if (col == 8)
                cell.SetCellValue(serviceDemands?.CoverageType);
            if (col == 9)
                cell.SetCellValue(serviceDemands?.ProjectName);
            if (col == 11)
                cell.SetCellValue(serviceDemands?.RepairPart);
            if (col == 12)
                cell.SetCellValue(serviceDemands?.RepairsUntil_EOSOrESD);
            if (col == 13)
                cell.SetCellValue(serviceDemands?.EndOfService_EOSOrESD);
            if (rowNum < (summary.SeriesDemandsList.Count + 206))
            {

                if (col == 14)
                    cell.SetCellValue(serviceDemands?.SumServiceDemand);
                if (col == 15)
                    cell.SetCellValue(serviceDemands?.ComponentQuantity);
                if (col == 16)
                    cell.SetCellValue(serviceDemands?.TotalDemand);
            }
            else
            {
                if (col == 14)
                    cell.SetCellValue("0");
                if (col == 15)
                    cell.SetCellValue("1");
                if (col == 16)
                    cell.SetCellValue("0");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="summary"></param>
        /// <param name="col"></param>
        /// <param name="cell"></param>
        public void Remove(Summary summary, int col, ICell cell, IWorkbook workbook)
        {
            if (col == 0)
                cell.SetCellValue(summary.Project.Approvaler3);
            if (col == 1)
                cell.SetCellValue(summary.Project.Approvaler3);
            if (col == 2 || col == 3)
            {
                RemoveBorderStyle(workbook, cell);
                cell.SetCellValue(summary.Project.Approvaler3);
            }
            if (col == 4)
                cell.SetCellValue(summary.Project.Approvaler3);
            if (col == 5 || col == 6)
            {
                RemoveBorderStyle(workbook, cell);
                cell.SetCellValue(summary.Project.Approvaler3);
            }
            if (col == 7)
                cell.SetCellValue(summary.Project.Approvaler3);
            if (col == 8)
            {
                RemoveBorderStyle(workbook, cell);
                cell.SetCellValue(summary.Project.Approvaler3);
            }
            if (col == 10 || col == 11 || col == 12)
            {
                RemoveBorderStyle(workbook, cell);
                cell.SetCellValue(summary.Project.Approvaler3);
            }

        }

        public void AddBorder(Summary summary, int col, ICell cell, IWorkbook workbook)
        {
            if (col == 2 || col == 3 || col == 5 || col == 6 || col == 8 || col == 11 || col == 12)
            {
                AddBorderStyle(workbook, cell);
            }

        }

        /// <summary>
        ///  填充单元格内容
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="rowNum"></param>
        public void SetFillCellValue(IWorkbook workbook, ICell cell, int rowNum, int col, Summary summary, SeriesDemands seriesDemands, ServiceDemands serviceDemands)
        {
            switch (rowNum)
            {
                case 0:
                    if (col == 9)
                        cell.SetCellValue(summary.Project.BL);

                    break;
                case 1:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.PDN);
                    if (col == 8)
                        cell.SetCellValue(summary.Project.Manufacturer);
                    break;
                case 2:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.SPN);
                    if (col == 8)
                        cell.SetCellValue(summary.Project.ObsoleteMaterialDescription);
                    break;
                case 4:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.AffectedBLs);
                    if (col == 8)
                        cell.SetCellValue(summary.Project.StockUpStartdate);
                    break;
                case 5:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.BL);
                    if (col == 8)
                        cell.SetCellValue(summary.Project.Price);
                    break;
                case 8:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.TotalSeriesDemand);
                    if (col == 8)
                        cell.SetCellValue(summary.Project.TotalSeriesCosts);
                    break;
                case 9:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.TotalServiceDemand);
                    if (col == 8)
                        cell.SetCellValue(summary.Project.TotalServiceCosts);
                    break;

                case 11:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.totalDemand);
                    if (col == 8)
                        cell.SetCellValue(summary.Project.totalCosts);
                    break;
                case 18:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.CreatederDepartment);
                    if (col == 5)
                        cell.SetCellValue(summary.Project.Createder);
                    if (col == 8)
                        cell.SetCellValue(summary.Project.CreateDate);
                    break;
                case 20:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.ApprovalerDepartment1);
                    if (col == 5)
                        cell.SetCellValue(summary.Project.Approvaler1);
                    if (col == 8)
                        cell.SetCellValue(summary.Project.ApprovalDate1);
                    break;
                case 21:
                    if (string.IsNullOrEmpty(summary.Project.Approvaler2))
                    {
                        Remove(summary, col, cell, workbook);
                    }
                    else
                    {
                        AddBorder(summary, col, cell, workbook);
                        if (col == 0)
                            cell.SetCellValue("Approval*:");
                        if (col == 1)
                            cell.SetCellValue("Department:");
                        if (col == 2)
                            cell.SetCellValue(summary.Project.ApprovalerDepartment2);
                        if (col == 4)
                            cell.SetCellValue("Name:");
                        if (col == 5)
                            cell.SetCellValue(summary.Project.Approvaler2);
                        if (col == 7)
                            cell.SetCellValue("Date:");
                        if (col == 8)
                            cell.SetCellValue(summary.Project.ApprovalDate2);
                        if (col == 10)
                            cell.SetCellValue("Signature:");
                    }
                    break;
                case 22:
                    if (string.IsNullOrEmpty(summary.Project.Approvaler3))
                    {
                        Remove(summary, col, cell, workbook);
                    }
                    else
                    {
                        AddBorder(summary, col, cell, workbook);
                        if (col == 0)
                            cell.SetCellValue("Approval*:");
                        if (col == 1)
                            cell.SetCellValue("Department:");
                        if (col == 2)
                            cell.SetCellValue(summary.Project.ApprovalerDepartment3);
                        if (col == 4)
                            cell.SetCellValue("Name:");
                        if (col == 5)
                            cell.SetCellValue(summary.Project.Approvaler3);
                        if (col == 7)
                            cell.SetCellValue("Date:");
                        if (col == 8)
                            cell.SetCellValue(summary.Project.ApprovalDate2);
                        if (col == 10)
                            cell.SetCellValue("Signature:");
                    }
                    break;
                case 100:
                    if (col == 5)
                        cell.SetCellValue(summary.Project.BL);
                    if (col == 7)
                        cell.SetCellValue(summary.Project.PDN);
                    if (col == 10)
                        cell.SetCellValue(summary.Project.SPN);
                    if (col == 14)
                        cell.SetCellValue(summary.Project.ObsoleteMaterialDescription);
                    break;
                case 105:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 106:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 107:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 108:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 109:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 110:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 111:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 112:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 113:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 114:
                    setCellValueForSecondErgodic(cell, summary, seriesDemands, rowNum, col);
                    break;
                case 115:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.StockUpStartdate);
                    if (col == 7)
                        cell.SetCellValue(summary.Project.Price);
                    if (col == 11)
                        cell.SetCellValue(summary.Project.TotalSeriesCosts);
                    if (col == 16)
                        cell.SetCellValue(summary.Project.TotalSeriesDemand);
                    break;
                case 200:
                    if (col == 7)
                        cell.SetCellValue(summary.Project.PDN);
                    if (col == 10)
                        cell.SetCellValue(summary.Project.SPN);
                    if (col == 14)
                        cell.SetCellValue(summary.Project.ObsoleteMaterialDescription);
                    break;
                case 201:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.BL);
                    if (col == 7)
                        cell.SetCellValue(summary.Project.AffectedBLs);
                    if (col == 14)
                        cell.SetCellValue(summary.Project.Manufacturer);
                    break;
                case 206:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 207:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 208:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 209:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 210:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 211:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 212:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 213:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 214:
                    SetCellValueForThirdErgodic(cell, summary, serviceDemands, rowNum, col);
                    break;
                case 215:
                    if (col == 2)
                        cell.SetCellValue(summary.Project.StockUpStartdate);
                    if (col == 7)
                        cell.SetCellValue(summary.Project.Price);
                    if (col == 11)
                        cell.SetCellValue(summary.Project.TotalServiceCosts);
                    if (col == 16)
                        cell.SetCellValue(summary.Project.TotalServiceDemand);
                    break;

            }
        }

    }
}
