using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxShield(float maxShiled)
    {
        slider.maxValue = maxShiled;
        slider.value = maxShiled;
    }

    public void SetShield(float shield)
    {
        slider.value = shield;
    }
}
