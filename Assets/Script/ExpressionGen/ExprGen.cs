using System.IO;
using UnityEditor;
using System.Linq;
using UnityEngine;

public static class ExprGen
{

    [MenuItem("公式生成器/转换所选| Gen Selected Excel")]
    public static void SelectionToCSharp()
    {
        if (Selection.objects == null) return;
        string assetPath = AssetDatabase.GetAssetPath(Selection.objects.FirstOrDefault());
        var mExcelReader = new ExcelReader(Path.GetFullPath(assetPath));
        var mCodeGenerator = new CodeGenerator(Path.GetFileNameWithoutExtension(assetPath), mExcelReader.Expressions);
        mCodeGenerator.StartGeneratingCode();
    }
}
