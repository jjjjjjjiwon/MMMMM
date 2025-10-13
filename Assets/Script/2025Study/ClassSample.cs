using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class BaseClass
{
    public string stringVariable;
    public int intVariable { get; set; }
}
abstract public class ABSBaseClass
{
    abstract public void Method();
}
// Override
public class DerivedClass3 : ABSBaseClass
{
    override public void Method()
    {
        
    }
}
public class DerivedClass1 : BaseClass
{
    public void Method()
    {
        Debug.Log(stringVariable);
    }
}

public class DerivedClass2 : BaseClass
{
    public void Method()
    {
        Debug.Log(intVariable);
    }
}

public class ClassSample : MonoBehaviour
{
    DerivedClass1 baseClass1 = new DerivedClass1();
    //BaseClass baseClass1 = new DerivedClass1();
    DerivedClass2 baseClass2 = new DerivedClass2();
    //ABSBaseClass bc1 = new ABSBaseClass();
    DerivedClass3 dc1 = new DerivedClass3();
    void Start()
    {
        baseClass1.stringVariable = "DerivedClass1";
        baseClass1.Method();
        baseClass2.intVariable = 3;
        baseClass2.Method();
        dc1.Method();
    }
}
