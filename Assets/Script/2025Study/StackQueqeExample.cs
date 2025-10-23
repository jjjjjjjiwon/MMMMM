using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StackQueqeExample : MonoBehaviour
{
    void Start()
    {
        // STACK
        // var stack = new Stack<string>();
        // stack.Push("1 번째");
        // stack.Push("2 번째");
        // stack.Push("3 번째");

        // foreach (var item in stack)
        // {
        //     Debug.Log(item);
        // }

        // Debug.Log(stack.Peek().ToString());

        // stack.Pop();

        // foreach (var item in stack)
        // {
        //     Debug.Log(item);
        // }

        // QUEUE
        var queue = new Queue<string>();
        queue.Enqueue("frist");
        queue.Enqueue("second");
        queue.Enqueue("third");

        foreach (var item in queue)
        {
            Debug.Log(queue);
        }
        Debug.Log($"Peek: {queue.Peek()}");

        foreach (var item in queue)
        {
            Debug.Log(queue);
        }
    }

}
