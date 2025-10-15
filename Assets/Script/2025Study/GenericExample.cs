using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenericExample : MonoBehaviour
{
    public class GenericsClass<T>
    {
        T value;

        public GenericsClass(T value)
        {
            this.value = value;
        }

        public void Method1()
        {
            Debug.Log(value);
        }
    }
    void Start()
    {
        // GenericsClass<int> genericsClass = new GenericsClass<int>(5);
        // genericsClass.Method1();
        // GenericsClass<float> genericsClass1 = new GenericsClass<float>(5.1f);
        // genericsClass1.Method1();

        int ix = 1, iy = 2;
        Swap(ref ix, ref iy);
        Debug.Log($"x = {ix}, y = {iy}");

        // Tuple 사용
        (ix, iy) = (iy, ix);
        Debug.Log($"x = {ix}, y = {iy}");

        string sx = "ab", sy = "bc";
        Swap(ref sx, ref sy);
        Debug.Log($"sx = {sx}, sy = {sy}");
        
    }

    void Swap<T>(ref T x, ref T y)
    {
        var temp = y;
        y = x;
        x = temp;
    }
}
