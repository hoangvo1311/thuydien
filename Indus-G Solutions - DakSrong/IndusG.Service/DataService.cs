using IndusG.DataAccess;
using IndusG.Models;
using System;
using System.Linq.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using OfficeOpenXml;
using System.Web;
using OfficeOpenXml.Style;
using System.Drawing;

namespace IndusG.Service
{
    public class DataService
    {
        public DataTableResponseModel LoadData(DataTableQueryModel queryModel, string from, string to)
        {
            using (var context = new QuantracEntities())
            {
                DateTime? fromDate = null;
                DateTime? toDate = null;

                if (!string.IsNullOrEmpty(from))
                {
                    fromDate = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                if (!string.IsNullOrEmpty(to))
                {
                    toDate = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                // Getting data
                var dataList = context.DB_Sesan_2PLC.Where(x => (!fromDate.HasValue || x.Date >= fromDate.Value)
                                        && (!toDate.HasValue || x.Date <= toDate.Value) &&
                                        (x.Minutes == "0" || x.Minutes == "15" || x.Minutes == "30" || x.Minutes == "45"));
                var recordsTotal = 0;

                //Sorting    
                if (!(string.IsNullOrEmpty(queryModel.SortColumn) && string.IsNullOrEmpty(queryModel.SortColumnDir)))
                {
                    dataList = dataList.OrderBy(queryModel.SortColumn + " " + queryModel.SortColumnDir);
                }

                //total number of rows count     
                recordsTotal = dataList.Count();

                if (queryModel.PageSize != -1)
                {
                    dataList = dataList.Skip(queryModel.Skip).Take(queryModel.PageSize);
                }

                //Paging     
                var data = dataList.Select(x => new DataModel
                {
                    Date = x.Date,
                    UpstreamWaterLevel_m = x.UpstreamWaterLevel_m,
                    DownstreamWaterLevel_m = x.DownstreamWaterLevel_m,
                    Qve_Ho = x.Qve_Ho,
                    Qoverflow = x.Qoverflow,
                    QcmH1H2H3 = x.QcmH1H2H3,
                    Qminflow = x.Qminflow,
                    Qve_Hadu = x.Qve_Hadu,
                    Qve_HoDB = x.Qve_HoDB,
                    Reserve_Water = x.Reserve_Water
                }).ToList();

                WorkContext.SetSession("queryData", data);

                //Returning Json Data    
                return new DataTableResponseModel
                {
                    draw = queryModel.Draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = data
                };
            }
        }

        public MemoryStream Export()
        {
            var dataList = WorkContext.GetSession<List<DataModel>>("queryData");
            LoggerHelper.Info($"Export {dataList.Count} records");
            var fileinfo = new FileInfo(HttpContext.Current.Server.MapPath("~/Documents/BM01 BC NGAY THANG NAM_Temp.xlsx"));
            if (fileinfo.Exists)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileinfo))
                {
                    ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                    ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
                    excelWorksheet.Name = "Tháng " + DateTime.Now.Month.ToString();
                    var startRow = 18;

                    for (int index = 0; index < dataList.Count; index++)
                    {
                        excelWorksheet.Cells[startRow + index, 1].Value = dataList[index].TimeString;
                        excelWorksheet.Cells[startRow + index, 2].Value = dataList[index].DateString;
                        excelWorksheet.Cells[startRow + index, 3].Value = dataList[index].UpstreamWaterLevel_m;
                        excelWorksheet.Cells[startRow + index, 4].Value = dataList[index].DownstreamWaterLevel_m;
                        excelWorksheet.Cells[startRow + index, 5].Value = dataList[index].Qve_Ho;
                        excelWorksheet.Cells[startRow + index, 6].Value = dataList[index].Qoverflow;
                        excelWorksheet.Cells[startRow + index, 7].Value = dataList[index].QcmH1H2H3;
                        excelWorksheet.Cells[startRow + index, 8].Value = dataList[index].Qminflow;
                        excelWorksheet.Cells[startRow + index, 9].Value = dataList[index].Qve_Hadu;
                        excelWorksheet.Cells[startRow + index, 10].Value = dataList[index].Qve_HoDB;
                        excelWorksheet.Cells[startRow + index, 11].Value = dataList[index].Reserve_Water;
                    }

                    //Format data
                    using (var range = excelWorksheet.Cells[startRow, 1, startRow + dataList.Count - 1, 12])
                    {
                        // Assign borders
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    // Additional Data
                    excelWorksheet.Cells[6, 1].Value = "THÁNG " + DateTime.Now.Month + "-" + DateTime.Now.Year;
                    excelWorksheet.Cells[startRow + dataList.Count + 1, 1].Value = "Nơi gửi";
                    excelWorksheet.Cells[startRow + dataList.Count + 2, 1].Value = "+";
                    excelWorksheet.Cells[startRow + dataList.Count + 3, 1].Value = "+";
                    excelWorksheet.Cells[startRow + dataList.Count + 4, 1].Value = "+";
                    //excelWorksheet.Range[excelWorksheet.Cells[startRow + dataList.Count + 5, 3], excelWorksheet.Cells[startRow + dataList.Count + 5, 4]].Merge();
                    //excelWorksheet.Cells[startRow + dataList.Count + 5, 3].Value = "Người lập biểu";
                    //excelWorksheet.Cells[startRow + dataList.Count + 5, 3].Style.Font.Bold = true;
                    //excelWorksheet.Cells[startRow + dataList.Count + 5, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //excelWorksheet.Cells[startRow + dataList.Count + 5, 10, startRow + dataList.Count + 5, 12].Merge = true;
                    //excelWorksheet.Cells[startRow + dataList.Count + 5, 10].Value = "GIÁM ĐỐC";
                    //excelWorksheet.Cells[startRow + dataList.Count + 5, 10].Style.Font.Bold = true;
                    //excelWorksheet.Cells[startRow + dataList.Count + 5, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    return new MemoryStream(excelPackage.GetAsByteArray());
                }
            }
            return null;
        }

    }
}
