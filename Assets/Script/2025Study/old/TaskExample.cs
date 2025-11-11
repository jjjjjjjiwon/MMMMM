using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TaskExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Task.Factory.StartNew(() => { Debug.Log("Task"); });
        
        Task task2 = new Task(new Action(DebugLog));
        task2.Start();

        Task task3 = new Task(delegate { DebugLog(); });
        task3.Start();

        Task task4 = new Task(() => DebugLog());
        task4.Start();

        Task task5 = new Task(() => { DebugLog(); });
        task5.Start();

        task2.Wait();
        task3.Wait();
        task4.Wait();
        task5.Wait();

    }

    void DebugLog()
    {
        Debug.Log("Task2");
    }
}
