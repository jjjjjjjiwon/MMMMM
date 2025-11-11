using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonData
{
    public string name;
    public int age;
    public float height;
    public bool man;
    public string description;
    public string[] tools;
}


public class JsonExample : MonoBehaviour
{
    void Start()
    {
        ///////////////////////////////////////////// Json
        /// JSON은 간단하고 빠르고, 어디서나 통하는 데이터 교환의 공통 언어
        /// 직렬화는 객체나 데이터를 저장·전송하기 위해 일렬화된 형태로 변환하는 과정이다.
        /// 역직렬화는 직렬화된 데이터를 다시 프로그램에서 사용할 수 있는 객체나 구조로 복원하는 과정이다.
        
        const string filepath = @"D:\AAA\JsonExample.json"; 

        // JsonData[] jsonDatas = new JsonData[2];
        // jsonDatas[0] = new JsonData();
        // jsonDatas[1] = new JsonData();

        // 크기를 잡고 만드는
        int len = 2;
        JsonData[] jsonData = new JsonData[len];
        // 초기화 편하게
        for (int i = 0; i < len; i++)
        {
            jsonData[i] = new JsonData();
        }
        jsonData[0].name = "coderzero";
        jsonData[0].age = 48;
        jsonData[0].height = 172.5f;
        jsonData[0].man = true;
        jsonData[0].description = null;
        jsonData[0].tools = new string[3];
        jsonData[0].tools[0] = "Unity";
        jsonData[0].tools[1] = "Visual Studio";
        jsonData[0].tools[2] = "Phtoshop";

        jsonData[1].name = "coderzero";
        jsonData[1].age = 48;
        jsonData[1].height = 172.5f;
        jsonData[1].man = true;
        jsonData[1].description = null;
        jsonData[1].tools = new string[2];
        jsonData[1].tools[0] = "3D Max";
        jsonData[1].tools[1] = "Photoshop";

        // serilize
        string toJson0 = JsonUtility.ToJson(jsonData[0]);
        Debug.Log(toJson0);

        string toJson1 = JsonUtility.ToJson(jsonData[0]);
        Debug.Log(toJson1);

        // de-serialize
        JsonData fromJson = JsonUtility.FromJson<JsonData>(toJson0);
        Debug.Log(fromJson.name);

        // save file
        File.WriteAllText(filepath, toJson1);

        // load file
        string readJson = File.ReadAllText(filepath);
        Debug.Log(readJson);
    }

}
