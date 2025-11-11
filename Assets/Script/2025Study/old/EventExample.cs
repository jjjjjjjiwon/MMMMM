using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent
{
    public event EventHandler Click;

    public void MouseButton()
    {
        if(this.Click != null)
        {
            Click(this, EventArgs.Empty);
        }
    }
}

public class EventExample : MonoBehaviour
{
    void Start()
    {
        ButtonEvent buttonEvent = new ButtonEvent();
        buttonEvent.Click += new EventHandler(ButtonClick);
        buttonEvent.Click += new EventHandler(ButtonClick);

        buttonEvent.MouseButton();
        
    }

    void ButtonClick(object sender, EventArgs e)
    {
        Debug.Log("버튼 클릭");
    }
}
