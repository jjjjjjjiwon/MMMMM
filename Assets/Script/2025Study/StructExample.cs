using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructExample : MonoBehaviour
{
    public struct Struct
    {
        public int a, b;

        public Struct(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        public void DebugLog()
        {
            Debug.Log($"a = {a}, b = {b}");
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Struct str1;
        str1.a = 10;
        str1.b = 20;
        str1.DebugLog();

        Struct str2 = new Struct(1, 2);
        str2.DebugLog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
