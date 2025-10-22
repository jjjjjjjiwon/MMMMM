using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class ParallelForExample : MonoBehaviour
{
    void Start()
    {
        Parallel.For(0, 1000, (i) => { Debug.Log($"{Thread.CurrentThread.ManagedThreadId} : {i}"); });
    }

}
