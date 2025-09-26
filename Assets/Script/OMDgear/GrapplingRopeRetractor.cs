using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRopeRetractor : MonoBehaviour
{
    void OnEnable()
    {
        GrapplingInputHandler.OnMovePressed += RetractorFunction;
    }
    void OnDisable()
    {
        GrapplingInputHandler.OnMovePressed -= RetractorFunction;
    }

    void RetractorFunction()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
