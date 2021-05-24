using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using config;
using UnityEngine;
using ProtoBuf;
using ProtoBuf.Meta;

public class ProtobufTest : MonoBehaviour
{
    private List<string> errorLogList = new List<string>();
    private ProjectModel typeModel = new ProjectModel();

    private void Awake()
    {
        Application.logMessageReceived += (condition, trace, type) =>
        {
            if (type == LogType.Assert || type == LogType.Error || type == LogType.Exception)
            {
                errorLogList.Add(condition);
                if (errorLogList.Count > 20)
                {
                    errorLogList.RemoveAt(0);
                }
            }
        };
    }

    private void Start()
    {
        Test test = CreateTestInstance();
        byte[] binaryData;
        using (MemoryStream memoryStream = new MemoryStream())
        {
            typeModel.Serialize(memoryStream, test);
            binaryData = memoryStream.ToArray();
        }
        using (MemoryStream memoryStream2 = new MemoryStream(binaryData))
        {
            Test test2 = typeModel.Deserialize<Test>(memoryStream2);
            Debug.LogError(TestToString(test2));
        }
    }

    private Test CreateTestInstance()
    {
        Test test = new Test();
        test.intValue = 1;
        test.floatValue = 2.2f;
        test.longValue = 10;
        test.doubleValue = 1.23f;
        test.stringValue = "122333.fffsdf";
        test.intArrayValue = new int[] {1, 2, 3};
        test.floatArrayValue = new float[] {1, 2, 3};
        test.longArrayValue = new long[] {12000, 12312412124, 1123412412L};
        test.stringArrayValue.Add("str1");
        test.stringArrayValue.Add("str2");
        test.stringArrayValue.Add("str3");
        test.doubleArrayValue = new double[] {1.1, 2.32, 4.5};
        test.intStringPairValue = new IntStringPair() {key = 1, value = "str1"};
        return test;
    }

    private string TestToString(Test test)
    {
        string output = "";
        output += "test.intValue:" + test.intValue + "\r\n";
        output += "test.floatValue:" + test.floatValue + "\r\n";
        output += "test.doubleValue:" + test.doubleValue + "\r\n";
        output += "test.stringValue:" + test.stringValue + "\r\n";
        output += "test.longValue:" + test.longValue + "\r\n";
        for (int i = 0; i < test.intArrayValue.Length; i++)
        {
            output += "test.intArrayValue[" + i + "]" + test.intArrayValue[i] + "\r\n";
        }
        for (int i = 0; i < test.floatArrayValue.Length; i++)
        {
            output += "test.floatArrayValue[" + i + "]" + test.floatArrayValue[i] + "\r\n";
        }
        for (int i = 0; i < test.doubleArrayValue.Length; i++)
        {
            output += "test.doubleArrayValue[" + i + "]" + test.doubleArrayValue[i] + "\r\n";
        }
        for (int i = 0; i < test.stringArrayValue.Count; i++)
        {
            output += "test.stringArrayValue[" + i + "]" + test.stringArrayValue[i] + "\r\n";
        }
        for (int i = 0; i < test.longArrayValue.Length; i++)
        {
            output += "test.longArrayValue[" + i + "]" + test.longArrayValue[i] + "\r\n";
        }
        output += "test.intStringPairValue.key:" + test.intStringPairValue.key + "\r\n";
        output += "test.intStringPairValue.value:" + test.intStringPairValue.value + "\r\n";
        return output;
    }

    private void OnGUI()
    {
        GUI.contentColor = Color.red;
        for (int i = errorLogList.Count - 1; i >= 0; i--)
        {
            GUILayout.Label(errorLogList[i]);
        }
    }
}