using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateChain : MonoBehaviour
{
    delegate void CalculateChain(int a, int b);

    void Start()
    {
        CalculateChain calculateChain = Add;
        calculateChain += Subtract;
        calculateChain(3, 2);
    }
    
    void Add(int a, int b)
    {
        Debug.Log($"Add : {a + b}");
    }
    void Subtract(int a, int b)
    {
        Debug.Log($"Sub : {a - b}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
