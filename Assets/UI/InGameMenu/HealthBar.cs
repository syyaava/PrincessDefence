using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Damageble Princess;
    private Slider slider;
    
    void Start()
    {
        slider = GetComponent<Slider>();
        Princess.OnHealthChange.AddListener(SetValue);
        slider.maxValue = Princess.Health;
        slider.value = Princess.Health;
    }

    public void SetValue(float value)
    {
        slider.maxValue = Princess.MaxHealth;
        slider.value = value;        
    }
}
