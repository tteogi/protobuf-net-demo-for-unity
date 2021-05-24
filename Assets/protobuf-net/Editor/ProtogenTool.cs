using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

public class ProtogenTool
{
    [MenuItem("Tools/生成所有proto文件的CSharp代码")]
    public static void GenerateProtos()
    {
        string protoFolder = "./protos";
        string protogenTool = "./protogen/protogen.dll";
        string protoScriptFolder = "./Assets/Scripts/protoscripts";
        //清空代码文件夹下所有cs文件
        if (Directory.Exists(protoScriptFolder))
        {
            Directory.Delete(protoScriptFolder, true);
        }
        //获取proto文件夹下所有proto文件
        if (!Directory.Exists(protoFolder))
        {
            return;
        }
        var protoFiles = Directory.GetFiles(protoFolder, "*.proto", SearchOption.AllDirectories);
        if (protoFiles == null || protoFiles.Length == 0)
        {
            return;
        }
        for (int i = 0; i < protoFiles.Length; i++)
        {
            protoFiles[i] = protoFiles[i].Replace('\\', '/');
        }
        //创建输出目录
        Directory.CreateDirectory(protoScriptFolder);
        //调用protogen生成cs文件
        StringBuilder stringBuilder = new StringBuilder();
        //指定需要调用protogen
        stringBuilder.Append(protogenTool);
        //+names=original表示命名方式使用proto中的字段名
        stringBuilder.Append(" +names=original");
        //指定proto文件夹路径
        stringBuilder.Append(" --proto_path=");
        stringBuilder.Append(protoFolder.Replace('/', '\\').TrimEnd('\\'));
        //指定生成C#代码
        stringBuilder.Append(" --csharp_out=");
        stringBuilder.Append(protoScriptFolder.Replace('/', '\\').TrimEnd('\\'));
        for (int i = 0; i < protoFiles.Length; i++)
        {
            stringBuilder.Append(" ");
            stringBuilder.Append(Path.GetFileName(protoFiles[i]));
        }
        //调用dotnet执行proto=>csharp文件的转换
        var process = Process.Start("dotnet", stringBuilder.ToString());
        process.WaitForExit();
        AssetDatabase.Refresh();
    }
}