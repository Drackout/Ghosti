using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgress : MonoBehaviour
{
    [SerializeField] private FloatValue beamvalue;
    [SerializeField] private FloatValue maxValue;
    [SerializeField] private Image      bar;
    [SerializeField] private Gradient   color;

    void Update()
    {
        float value = beamvalue.GetValue();

        bar.fillAmount = value;    
        if (color != null)
        {
            bar.color = color.Evaluate(bar.fillAmount);
        }
    }
}
