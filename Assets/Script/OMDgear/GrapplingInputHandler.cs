using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GrapplingInputHandler : MonoBehaviour
{
    [Header("키 설정")]
    public KeyCode shootKey = KeyCode.Space;
    public KeyCode moveKey = KeyCode.E;
    
    // 델리게이트 선언
    public static Action OnShootPressed;
    public static Action OnMovePressed;
    
    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            OnShootPressed?.Invoke();
            Debug.Log("발사 이벤트 발생");
        }
        
        if (Input.GetKeyDown(moveKey))
        {
            OnMovePressed?.Invoke();
            Debug.Log("이동 이벤트 발생");
        }
    }
}
