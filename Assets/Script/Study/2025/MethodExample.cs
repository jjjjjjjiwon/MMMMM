using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodExample : MonoBehaviour
{

    void Start()
    {
        int parameter1 = 1, parameter2 = 2;
        int intMethod = IntMethod(parameter1, parameter2);
        Debug.Log(intMethod);

        float parameter3 = 1.2f;
        VoidMethod(parameter3.ToString());
        VoidMethod(null);
    }

    int IntMethod(int p1, int p2)
    {
        return (p1 + p2);
    }

    void VoidMethod(string message)
    {
        if (message == null)
        {
            return;
        }

        Debug.Log($"MSG : {message}");
        return; // 생략 가능
    }
}
