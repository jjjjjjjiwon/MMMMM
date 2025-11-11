using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ThreadDemo2 : MonoBehaviour
{
    void Start()
    {
        EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
        Thread thread = new Thread(Run);
        //thread.Start();
        thread.Start(ewh);
        for (int i = 0; i < 1; i++)
        {
            Debug.Log($"Main thread : {i}");
            Thread.Sleep(100);
        }
        //thread.Join();
        ewh.WaitOne();
        Debug.Log("Main tread end");

        ThreadPool.QueueUserWorkItem(Run);
    }

    void Run(object obj)
    {
        EventWaitHandle ewh = obj as EventWaitHandle;
        for (int i = 0; i < 2; i++)
        {
            Debug.Log($"Sub Thread {i}");
            Thread.Sleep(100);
        }
        Debug.Log("Syub tread end");
        ewh.Set();
    }

}
