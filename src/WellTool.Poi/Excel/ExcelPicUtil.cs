using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace WellTool.Poi.Excel;

/// <summary>
/// Excel图片工具类
/// </summary>
public static class ExcelPicUtil
{
    /// <summary>
    /// 获取工作簿指定sheet中图片列表
    /// </summary>
    /// <param name="workbook">工作簿</param>
    /// <param name="sheetIndex">sheet的索引</param>
    /// <returns>图片映射，键格式：行_列，值：PictureData</returns>
    public static Dictionary<string, byte[]> GetPicMap(IWorkbook workbook, int sheetIndex)
    {
        if (workbook == null)
            throw new ArgumentNullException(nameof(workbook), "Workbook must be not null!");

        if (sheetIndex < 0)
            sheetIndex = 0;

        if (workbook is HSSFWorkbook hssfWorkbook)
        {
            return GetPicMapXls(hssfWorkbook, sheetIndex);
        }
        if (workbook is XSSFWorkbook xssfWorkbook)
        {
            return GetPicMapXlsx(xssfWorkbook, sheetIndex);
        }

        throw new ArgumentException($"Workbook type [{workbook.GetType()}] is not supported!");
    }

    /// <summary> 
    /// 获取XLS工作簿指定sheet中图片列表 
    /// </summary> 
    /// <param name="workbook">HSSFWorkbook</param> 
    /// <param name="sheetIndex">sheet的索引</param> 
    /// <returns>图片映射</returns> 
    private static Dictionary<string, byte[]> GetPicMapXls(HSSFWorkbook workbook, int sheetIndex)
    {
        var picMap = new Dictionary<string, byte[]>();

        try
        {
            var sheet = workbook.GetSheetAt(sheetIndex);
            var patriarch = sheet.DrawingPatriarch;
            if (patriarch == null)
                return picMap;

            // 使用 workbook.GetAllPictures() 获取所有图片
            var pictures = workbook.GetAllPictures();
            for (int i = 0; i < pictures.Count; i++)
            {
                var pictureData = pictures[i];
                if (pictureData != null)
                {
                    // 尝试将 object 转换为 IPictureData
                    if (pictureData is NPOI.SS.UserModel.IPictureData ipictureData)
                    {
                        // 生成图片键名，格式为 "sheet{sheetIndex}_pic{i}"
                        var key = $"sheet{sheetIndex}_pic{i}";
                        picMap[key] = ipictureData.Data;
                    }
                    else if (pictureData is HSSFPictureData hssfPictureData)
                    {
                        // 生成图片键名，格式为 "sheet{sheetIndex}_pic{i}"
                        var key = $"sheet{sheetIndex}_pic{i}";
                        picMap[key] = hssfPictureData.Data;
                    }
                }
            }

            return picMap;
        }
        catch
        {
            return picMap;
        }
    }

    /// <summary>
    /// 获取XLSX工作簿指定sheet中图片列表
    /// </summary>
    /// <param name="workbook">XSSFWorkbook</param>
    /// <param name="sheetIndex">sheet的索引</param>
    /// <returns>图片映射</returns>
    private static Dictionary<string, byte[]> GetPicMapXlsx(XSSFWorkbook workbook, int sheetIndex)
    {
        var result = new Dictionary<string, byte[]>();

        try
        {
            var sheet = workbook.GetSheetAt(sheetIndex);
            var drawing = sheet.CreateDrawingPatriarch();

            if (drawing is XSSFDrawing xssfDrawing)
            {
                // 遍历所有形状
                foreach (var shape in xssfDrawing.GetShapes())
                {
                    if (shape is XSSFPicture pic)
                    {
                        var anchor = pic.GetPreferredSize();
                        if (anchor != null)
                        {
                            var key = $"{anchor.Row1}_{anchor.Col1}";
                            result[key] = pic.PictureData.Data;
                        }
                    }
                }
            }
        }
        catch
        {
            // 可能有异常，跳过之
        }

        return result;
    }
}
