using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Read_UI_Value_Sample : MonoBehaviour
{
    //Ejemplo Toggle

    public Toggle miToggle;


    public void OnToggleChange()
    {

        if (miToggle.isOn == true)
        {
            Debug.Log("Mi Toggle esta Verdadero!");
        }
        else
        {
            Debug.Log("Mi toggle ha cambiado a Falso!");
        }

    }

    //Ejemplo slider

    public void Start()
    {
        textSlider.text = (miSlider.value * 100).ToString("F0") + "%";
        languageChanger = GetComponent<LanguageChanger>();
    }

    public Slider miSlider;
    public Text textSlider;

    public void OnSliderChanged()
    { 
        textSlider.text = (miSlider.value * 100).ToString("F0") + "%";
    }


    private LanguageChanger languageChanger;

    public void DropdownChange(int index)
    {
        switch (index)
        {
            case 0: languageChanger.SetEnglish();
                break;
            case 1: languageChanger.SetSpanish();
                break;
            case 2: languageChanger.SetUkrainan();
                break;
        }
    }



}
