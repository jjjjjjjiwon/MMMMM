using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CorutineDemo : MonoBehaviour
{
    IEnumerator m_Coroutine;
    void Start()
    {
        m_Coroutine = CoroutineMethod();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(m_Coroutine);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StopCoroutine(m_Coroutine);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            StopCoroutine(m_Coroutine);
        }
    }

    IEnumerator CoroutineMethod()
    {
        int count = 0;
        while (true)
        {
            Debug.Log(count);
            yield return new WaitForSeconds(1f);
            count++;
        }
    }
}
