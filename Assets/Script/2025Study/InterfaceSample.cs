using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

interface Ibase1
{
    public void Method11();
}
interface Ibase2
{
    public void Method21();
}
public class Class11 : Ibase1, Ibase2
{
    public void Method11()
    {
        Debug.Log("Interface Method11");
    }
    public void Method21()
    {
        Debug.Log("Interface Method21"); 
    }
}

public class InterfaceSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Ibase1 ib1 = new Ibase1();
        Class11 c1 = new Class11();
        c1.Method11();
        c1.Method21();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
