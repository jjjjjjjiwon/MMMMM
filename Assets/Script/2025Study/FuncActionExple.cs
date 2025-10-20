using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncActionExple : MonoBehaviour
{

    void Start()
    {
        Func<int> func1 = () => 1;
        Debug.Log($"{func1()}");

        Func<int, int> func2 = (a) => a * 2;
        Debug.Log($"num = {func2(2)}");

        Func<int, int, int, int> f3 = (x, y, z) => x * y + z;
        int answer = f3(4, 6, 2);

        Action action = () => Debug.Log("Action");
        action();

        Action<int, int> action2 = (a, b) => Debug.Log($"{a} + {b} = {a + b}");
        action2(2, 3);

        Action<int> ac3 = (x) =>
        {
            x = x * 2;
            Debug.Log(x);
        };
        ac3(37);


    }

}
