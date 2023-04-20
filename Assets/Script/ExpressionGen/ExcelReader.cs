using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System.Linq;
using System.Text;
using Excel;
using System.IO;


public class ExcelReader
{

    public string FilePath { get => filePath; set => filePath = value; }
    public List<ExpressionObj> Expressions => expressions;

    private string filePath;

    private DataSet dataSet;
    private List<ExpressionObj> expressions;

    public ExcelReader(string filePath)
    {
        this.filePath = filePath;
        this.dataSet = GetDataSet();
        this.expressions = GetExprObjs(dataSet);
    }

    /// <summary>
    /// 重写这个方法实现你自己的读取Excel表格方案
    /// </summary>
    /// <returns>DataSet是<see cref="System.Data.DataSet "/></returns>
    private DataSet GetDataSet()
    {
        if (dataSet != null)
        {
            return dataSet;
        }
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogError("Excel 文件路径为空");
            return null;
        }

        using FileStream mStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        IExcelDataReader mExcelReader = ExcelReaderFactory.CreateOpenXmlReader(mStream);
        dataSet = mExcelReader.AsDataSet();
        Debug.Log("Excel 文件读取完毕 " + mExcelReader.ExceptionMessage);
        if (dataSet == null) Debug.LogError("Excel 内容为空！");
        return dataSet;
    }

    private List<ExpressionObj> GetExprObjs(DataSet dataSet)
    {
        if (dataSet == null)
        {
            Debug.LogError("Excel 内容为空！");
            return null;
        }
        List<ExpressionObj> expressionObjs = new();
        for (int i = 0; i < dataSet.Tables.Count; i++)
        {
            ReadObj(dataSet.Tables[i], out var loadedObjs);
            expressionObjs = expressionObjs.Union(loadedObjs).ToList();
        }
        return expressionObjs;
    }

    private void ReadObj(DataTable mSheet, out List<ExpressionObj> expressionObjs)
    {
        expressionObjs = new();
        //判断数据表内是否存在数据
        if (mSheet.Rows.Count < 1) return;
        for (int i = 1; i < mSheet.Rows.Count; i++)
        {
            ExpressionObj expression = new()
            {
                variableName = mSheet.Rows[i][0].ToString(),
                atlasName = mSheet.Rows[i][1].ToString(),
                desc = mSheet.Rows[i][2].ToString(),
                expression = mSheet.Rows[i][3].ToString(),
            };
            if (expression.Invalid) continue;
            expressionObjs.Add(expression);
        }

    }
}
