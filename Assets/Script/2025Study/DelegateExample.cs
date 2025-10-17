using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateExample : MonoBehaviour
{
    delegate int CalculateDelegate(int a, int b);
    void Start()
    {
        CalculateDelegate Plus = new CalculateDelegate(Add);
        Plus += Subtract;
        Plus += Add;
        CalculateDelegate Minus = Subtract;
        DebugLog(3, 4, Plus.Invoke);
        //DebugLog(3, 4, Minus);

    }
    
    void DebugLog(int a, int b, CalculateDelegate cd)
    {
        Debug.Log($"{a}, {b}");
        Debug.Log(cd(a, b));
    }
    int Add(int a, int b)
    {
        return a + b;
    }

    int Subtract(int a, int b)
    {
        return a - b;
    }

    void Update()
    {
        
    }
}
