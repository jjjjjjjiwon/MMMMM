using System.Collections;
using UnityEngine;

public class Corutine : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
}