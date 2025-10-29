using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class FileDemo : MonoBehaviour
{
    
    public string filePath = @"D:\AAA\MyTest.txt"; // usb에 넣음
    void Start()
    {
        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("Hello");
            }
        }
        using (StreamReader sr = File.OpenText(filePath))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                Debug.Log(s);
            }
        }
    }

}
