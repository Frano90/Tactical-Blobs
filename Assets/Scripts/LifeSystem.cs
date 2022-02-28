using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LifeSystem
{
    [SerializeField] private int maxHP, currentHP;

    public event Action OnDeath;
    
    public LifeSystem(int total)
    {
        currentHP = total;
        maxHP = total;
    }
    
    
    public void Heal(int x)
    {
        currentHP += x;
        if (currentHP > maxHP) currentHP = maxHP;
    }

    public void Damage(int x)
    {
        Debug.Log("me pegan");
        currentHP -= x;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Death();
        }
        
    }

    private void Death()
    {
        Debug.Log("Se murio");
        OnDeath?.Invoke();
    }
}
