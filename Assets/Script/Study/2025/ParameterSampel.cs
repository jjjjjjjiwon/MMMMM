using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterSampel : MonoBehaviour
{
    void Start()
    {
        // NamedParameter("Lee", 23, 30.5f);
        // NamedParameter(name : "CoderZero", height:174.5f, age:47);
        // NamedParameter(age : 10, name : "Kim", height : 10.2f);
        // NamedParameter(name : "jung", age : 30, 3.1f);

        OptionalParmeter(1, 1.0f);

        Op2(1, 2, 3.0f);
        Op2(1, 2, isInt: false);
        Op2(isInt: false, j: 2, i: 1);
    }

    void Update()
    {

    }

    void NamedParameter(string name, int age, float height)
    {
        Debug.Log($"Name : {name}, Age : {age}, Height : {height}");
    }

    void OptionalParmeter(int i, float f, bool isInt = true)
    {
        if (isInt == true)
        {
            Debug.Log(i);
        }
        else
        {
            Debug.Log(f);
        }
    }

    void Op2(int i, int j, float f = 1.2f, bool isInt = true)
    {
        if (isInt == true)
        {
            Debug.Log(i + j);
        }
        else
        {
            Debug.Log(f);
        }
    }
}
