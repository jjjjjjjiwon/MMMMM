using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class funy : MonoBehaviour
{
    int a = 10;
    void Start()
    {
        Func<int> ramda = () => a;
        Action<int> setter = (val) => a = val;
        Debug.Log(ramda());
        a = 20;
        Debug.Log(ramda());
        setter(99);
        Debug.Log(ramda());
    }
}
