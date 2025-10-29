using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileInfoDemo : MonoBehaviour
{
    public string filePath = @"D:\AAA\MyTest.txt";

    FileInfo fileInfo;
    void Start()
    {
        fileInfo = new FileInfo(filePath);
        using (StreamWriter sw = fileInfo.CreateText())
        {
            sw.WriteLine("Hello");
            sw.WriteLine("How");
        }
        using (StreamReader sr = fileInfo.OpenText())
        {
            var s = "";
            while ((s = sr.ReadLine()) != null)
            {
                Debug.Log(s);
            }
        }
        try
        {
            string filePath2 = Path.GetTempFileName();
            var fileInfo2 = new FileInfo(filePath2);
            fileInfo2.Delete();
            fileInfo.CopyTo(filePath2);
            Debug.Log($"{filePath} was copied to {filePath2}");
            fileInfo2.Delete();
            Debug.Log($"{filePath2} was Delete");
        }
        finally
        {
            Debug.Log("The process fail");
        }
    }

}
