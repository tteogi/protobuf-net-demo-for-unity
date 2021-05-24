using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ProtoBuf;
using ProtoBuf.Meta;
using UnityEngine;
using UnityEditor;

public class ProtobufNetHelperEditor
{
    public const string protobufNetDirRoot = "Assets/protobuf-net";

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
        List<Type> types = new List<Type>();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        for (int i = 0; i < assemblies.Length; i++)
        {
            var asm = assemblies[i];
            var referAssemblies = asm.GetReferencedAssemblies();
            if (referAssemblies != null && referAssemblies.Length > 0)
            {
                bool isReferProtobufNet = false;
                for (int j = 0; j < referAssemblies.Length; j++)
                {
                    if (referAssemblies[j].Name == "protobuf-net")
                    {
                        isReferProtobufNet = true;
                    }
                }
                if (isReferProtobufNet)
                {
                    var typeArray = asm.GetTypes();
                    for (int j = 0; j < typeArray.Length; j++)
                    {
                        if (!types.Contains(typeArray[j]))
                        {
                            types.Add(typeArray[j]);
                        }
                    }
                }
            }
        }
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

    private static IEnumerable<Type> GetTypes(Assembly[] assemblies)
    {
        foreach (Assembly assembly in assemblies)
        {
            var types = AppDomain.CurrentDomain.Load(assembly.FullName).GetTypes();
            foreach (Type type in types)
            {
                yield return type;
            }
        }
    }
}