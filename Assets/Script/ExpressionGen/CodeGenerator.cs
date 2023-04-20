
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CodeGenerator
{
    List<ExpressionObj> exprs;
    string className;

    public CodeGenerator(string className, List<ExpressionObj> exprs)
    {
        this.className = className;
        this.exprs = exprs;
    }

    public void StartGeneratingCode()
    {
        ReplaceExpression(this.exprs);
        DevideValues(this.exprs, out List<ExpressionObj> inputValues, out List<ExpressionObj> outputValues);
        //Assets/Script/Template/
        string classTpl = ReadTemplate("ClassTemplate");
        string inputTpl = ReadTemplate("inputTpl");
        string propertyTpl = ReadTemplate("propertyTpl");
        string methodTpl = ReadTemplate("methodTpl");
        string csCode = GenTemplate(className,
             classTpl, inputTpl, propertyTpl, methodTpl,
             inputValues, outputValues, "float");

        string outputPath = Path.GetFullPath($"Assets/ExprGen/{className}.cs");
        Debug.Log($"输出文件将位于 {outputPath}");
        if (File.Exists(outputPath)) File.Delete(outputPath);
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
        using var writer = File.CreateText(outputPath);
        writer.Write(csCode);
    }

    public void ReplaceExpression(List<ExpressionObj> allExpObj)
    {
        foreach (var expObj in allExpObj)
        {
            foreach (var obj in allExpObj)
            {
                obj.expression = obj.expression.Replace(expObj.atlasName, expObj.variableName)
                    .Replace("（", "(")
                    .Replace("）", ")")
                    .Replace("“", "\"")
                    .Replace("”", "\"");
            }
        }
    }

    public void DevideValues(List<ExpressionObj> allObj, out List<ExpressionObj> inputValues, out List<ExpressionObj> outputValues)
    {
        inputValues = new();
        outputValues = new();
        foreach (var item in allObj)
        {
            if (string.IsNullOrEmpty(item.expression))
            {
                inputValues.Add(item);
            }
            else
            {
                outputValues.Add(item);
            }
        }
    }

    public string ReadTemplate(string templateName)
    {
        try
        {
            string fileGUID = AssetDatabase.FindAssets(templateName).First();
            string filePath = AssetDatabase.GUIDToAssetPath(fileGUID);
            return File.ReadAllText(Path.GetFullPath(filePath));
        }
        catch (System.Exception e)
        {
            Debug.LogError("寻找模板时出错");
            Debug.LogException(e);
            throw;
        }
    }

    public string GenTemplate(string className,
        string classTpl, string inputTpl, string propertyTpl, string methodTpl,
        List<ExpressionObj> inputValues, List<ExpressionObj> outputValues, string valueType = "long"
        )
    {
        string inputStr = default;
        string propertyStr = default;
        string methodStr = default;
        string classInitStr = default;

        /* inputTpl:
                 /// <summary>
                /// Template Input Value Summary
                /// </summary>
                public long inputValue = 0;
         */
        foreach (var item in inputValues)
        {
            inputStr += inputTpl.Replace("Template Input Value Summary", item.desc)
                .Replace("long", valueType)
                .Replace("inputValue", item.variableName);
        }

        /* propertyTpl:
              /// <summary>
             /// Template Property Summary
            /// </summary>
            public long TemplatePropertyName => GetTemplatePropertyValue();
         */
        foreach (var item in outputValues)
        {
            propertyStr += propertyTpl.Replace("Template Property Summary", item.desc)
                .Replace("long", valueType)
                .Replace("TemplatePropertyName", item.variableName)
                .Replace("TemplatePropertyValue", item.variableName);
        }

        /* methodTpl:
            /// <summary>
            /// Template Method Summary
            /// </summary>
            /// <returns></returns>
            private long GetTemplatePropertyValue()
            {
             return 0;
            }
         */
        foreach (var item in outputValues)
        {
            methodStr += methodTpl.Replace("Template Method Summary", item.desc)
                .Replace("long", valueType)
                .Replace("TemplatePropertyValue", item.variableName)
                .Replace("0", item.expression);
        }

        /* classInitTpl:
            /// <param name="inputValue"></param>
            public TemplateClassName(long inputValue)
            {
                this.inputValue = inputValue;
            }
         */
        foreach (var item in inputValues)
        {
            classInitStr += $"///<param name=\"{item.variableName}\">{item.desc}</param>\n";
        }
        classInitStr += $"\npublic {className}(";
        foreach (var item in inputValues)
        {
            classInitStr += $"{valueType} {item.variableName},";
        }
        classInitStr = classInitStr.Remove(classInitStr.Length - 1);
        classInitStr += ")\n{\n";
        foreach (var item in inputValues)
        {
            classInitStr += $"this.{item.variableName} = {item.variableName};\n";
        }
        classInitStr += "}\n\n";

        string outputTemplate = classTpl
            .Replace("TemplateClassName", className)
            .Replace("///Replace_InputValues_Here", inputStr)
            .Replace("///Replace_PropertyValues_Here", propertyStr)
            .Replace("///Replace_Method_Here", methodStr)
            .Replace("///Replace_ClassInit_Here", classInitStr);
        return outputTemplate;
    }
}
