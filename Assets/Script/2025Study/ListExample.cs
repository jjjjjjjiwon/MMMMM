using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ListExample : MonoBehaviour
{
    void Start()
    {
        var list = new List<string>();

        list.Add("1번째");
        list.Add("2번째");
        list.Add("3번째");
        list.Add("4번째");

        Debug.Log(list[1]);
        Debug.Log(list.Capacity);
        Debug.Log(list.Count);


        list.RemoveAt(2);
        list.Remove("4번째");
        list.Insert(0, "새 0번째");

        foreach (var item in list)
        {

            Debug.Log($"{item} in list");
        }
        list.Clear();
        Debug.Log(list.Capacity);

    }



}
    
