using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ProtoBuf;
using ProtoBuf.Meta;
using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;
using Assembly = System.Reflection.Assembly;

public class ProtobufNetHelperEditor
{
    public const string protobufNetDirRoot = "Assets/protobuf-net";
    public const string protobuf_net_dll_path = "Assets/protobuf-net/Plugins/protobuf-net.dll";

    /// <summary>
    /// 示例类型,用于从该类型所在的Assembly中获取到所有protobuf能够使用的类型,每个相关dll中只需要填一个即可
    /// </summary>
    public static List<Type> exampleTypes = new List<Type>()
    {
        typeof(config.Test),
    };

    [MenuItem("Tools/重建protobuf-model.dll")]
    private static void RebuildProtobufModelForProject()
    {
        RuntimeTypeModel typeModel = GetModel(out string typeNames);
        if (typeModel == null)
        {
            return;
        }
        typeModel.Compile("ProjectModel", "protobuf-model.dll");
        if (!Directory.Exists(protobufNetDirRoot + "/Plugins"))
        {
            Directory.CreateDirectory(protobufNetDirRoot + "/Plugins");
        }
        File.Copy("protobuf-model.dll", protobufNetDirRoot + "/Plugins/protobuf-model.dll", true);
        File.Delete("protobuf-model.dll");
        UnityEngine.Debug.Log("为以下类型重建protobuf-model.dll\r\n" + typeNames);
        AssetDatabase.Refresh();
    }

    private static RuntimeTypeModel GetModel(out string typeNames)
    {
        List<Type> types = GetAllRelatedTypeList();
        RuntimeTypeModel typeModel = RuntimeTypeModel.Create();
        StringBuilder stringBuilder = new StringBuilder();
        List<Type> list = new List<Type>();
        foreach (var t in types)
        {
            var contract = t.GetCustomAttributes(typeof(ProtoContractAttribute), false);
            if (contract.Length > 0 && !list.Contains(t))
            {
                typeModel.Add(t, true);
                stringBuilder.Append(t.ToString());
                stringBuilder.Append("\r\n");
                list.Add(t);
            }
        }
        typeNames = stringBuilder.ToString();
        return typeModel;
    }

    private static List<Type> GetAllRelatedTypeList()
    {
        List<Type> list = new List<Type>();
        List<string> assemblyNames = new List<string>();
        for (int i = 0; i < exampleTypes.Count; i++)
        {
            var assembly = Assembly.GetAssembly(exampleTypes[i]);
            if (assemblyNames.Contains(assembly.FullName))
            {
                continue;
            }
            assemblyNames.Add(assembly.FullName);
            list.AddRange(assembly.GetTypes());
        }
        return list;
    }
}