using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CancelationExample : MonoBehaviour
{
    CancellationTokenSource cts;
    Task<int> task;

    void Start()
    {
        cts = new CancellationTokenSource();
        CancellationToken ct = cts.Token;
        task = Task.Factory.StartNew(TaskMethod, ct);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cts.Cancel();
            if (task != null)
            {
                Debug.Log($"count : {task.Result}");
            }
        }
    }
    
    int TaskMethod()
    {
        int count = 0;
        
        for (int i = 0; i < 10; i++)
        {
            if (cts.Token.IsCancellationRequested)
            {
                break;
            }
            ++count;
            Thread.Sleep(1000);
        }

        return count;
    }
}
