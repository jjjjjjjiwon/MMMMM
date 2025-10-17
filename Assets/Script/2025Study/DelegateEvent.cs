using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateEventClass
{
    public delegate void DelegateMethod(string str);
    public event DelegateMethod EventMethod;

    public void MultipleOf5(int num)
    {
        if (num % 5 == 0 && num != 0)
        {
            EventMethod($"{num}는 5의 배수");
        }
    }
}

public class DelegateEvent : MonoBehaviour
{

    void Start()
    {
        DelegateEventClass dec = new DelegateEventClass();
        dec.EventMethod += DebugLog;
        
        for (int i = 0; i < 30;  i ++)
        {
            dec.MultipleOf5(i);
        }
    }

    void DebugLog (string str)
    {
        Debug.Log(str);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
