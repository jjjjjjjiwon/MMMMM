using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        //Thread thread = new Thread(Run);
        //Thread thread = new Thread(new ThreadStart(Run));
        //Thread thread = new Thread(() => Run());
        // Thread thread = new Thread(delegate ()
        // {
        //     Run();
        // });
        //thread.Start();

        //new Thread(() => Run()).Start();

        // Thread thread = new Thread(Run2);
        // thread.Start(1);
        // Thread thread = new Thread(() => Run2(1));
        // thread.Start(1);

        Thread thread = new Thread(() => sum(1, 2, 3));
        thread.Start();
    }

    void Run()
    {
        Debug.Log($"Thread{Thread.CurrentThread.ManagedThreadId} : start");
        Thread.Sleep(1000);
        Debug.Log($"Thread{Thread.CurrentThread.ManagedThreadId} : end");
    }

    void Run2(object ob)
    {
        Debug.Log(ob);
    }

    static void sum(int d1, int d2, int d3)
    {
        int sum = d1 + d2 + d3;
        Debug.Log(sum);
    }
}
