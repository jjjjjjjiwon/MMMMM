using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] Toggle toggle;
    [SerializeField] Slider slider;
    [SerializeField] Button button;
    [SerializeField] TMP_Dropdown dropdown;

    void Start()
    {
        titleText.text = "This is Title";
    }
    public void onToggleClicked()
    {
        if (toggle.isOn)
        {
            Debug.Log("Toggle is on");
        }
    }

    public void onSliderClicked()
    {
        Debug.Log(slider.value);
    }

    public void onButton()
    {
        Debug.Log("Button Clicked");
    }
    
    public void onDropDown()
    {
        Debug.Log(dropdown.value);
    }
}
