using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryExample : MonoBehaviour
{
        void Start()
    {
        var dict = new Dictionary<int, string>();
        dict.Add(101, "1번쨰");
        dict.Add(102, "2번쨰");
        dict.Add(103, "3번쨰");
        dict.Add(104, "4번쨰");

        foreach (var item in dict)
        {
            //Debug.Log($"{item.Key} {item.Value}");
        }

        foreach (var key in dict)
        {
            //Debug.Log(key);
        }
        foreach (var value in dict)
        {
            //Debug.Log(value);
        }

        Debug.Log($"{dict[101]}");
        if(dict.TryGetValue(101, out string newValue) == false) // .TryGetValue의 반환 값은 bool
        {
            dict.Add(101, "새로운거");
        }

        // dict.Add(101, "이건 안들어감"); // 안됨
        // Debug.Log($"{dict[101]}");

        dict[101] = "이건 들어간다";
        Debug.Log($"{dict[101]}");

    }

}
