using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using UnityEngine;

public class GenericTaskExample : MonoBehaviour
{
    void Start()
    {
        Task<int> task = Task.Factory.StartNew<int>(() => GetSize("GenericTask"));
        int result = task.Result;
        Debug.Log(result);
    }
    
    int GetSize(string str)
    {
        return str.Length;
    }

}
