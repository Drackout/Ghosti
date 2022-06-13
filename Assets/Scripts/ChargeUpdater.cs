using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeUpdater : MonoBehaviour
{
    [SerializeField] private FloatValue ChargeValue;
    [SerializeField, Range(0.0f, 4.0f)]
    private float      chargeScale = 1;

    // Update is called once per frame
    void Update()
    {
        ChargeValue.ChangeValue(-Time.deltaTime * chargeScale);

        if (ChargeValue.GetValue() < 0)
        {
            ChargeValue.SetValue(0);
        }
    }

    public void SetScale(float s)
    {
        chargeScale = s;
    }
}
