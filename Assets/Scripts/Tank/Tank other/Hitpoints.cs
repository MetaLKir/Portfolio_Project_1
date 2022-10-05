using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hitpoints : MonoBehaviour
{
    [SerializeField] int healthMax = 100;
    [SerializeField] int healthCurrent = 0;

    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHit, OnHeal;


    public int HealthCurrent
    {
        get 
        { 
            return healthCurrent; 
        }
        set
        {
            healthCurrent = value;
            OnHealthChange?.Invoke((float)HealthCurrent / healthMax); // for UI
        }
    }
    
    void Awake()
    {
        if (healthCurrent == 0)
            HealthCurrent = healthMax;
    }

    public void InflictDamage(int damageTaken)
    {
        HealthCurrent -= damageTaken;

        if (healthCurrent <= 0)
        {
            OnDead?.Invoke();
        }
        else
            OnHit?.Invoke();
    }

    public void Heal(int healTaken)
    {
        HealthCurrent += healTaken;
        HealthCurrent = Mathf.Clamp(HealthCurrent, 0, healthMax); // prevents restore more than healthMax
        OnHeal?.Invoke();
    }
}
