using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireGun : MonoBehaviour
{
    public static WireGun Instance;  // 싱글톤
    public bool isAiming = false;    // static 제거, public으로
    
    void Awake()
    {
        Instance = this;
    }
    
    void Update()
    {
        HandleAiming();
    }
    
    void HandleAiming()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = !isAiming;
            Debug.Log("조준 상태: " + isAiming);
        }
    }
}
