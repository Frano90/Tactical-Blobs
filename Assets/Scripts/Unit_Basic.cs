using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Basic : GridObject
{
    [SerializeField] protected LifeSystem lifeSystem;
    [SerializeField] private int totalHP;
    [SerializeField] private float takeDamageFeedbackDuration = 2f;

    public void Start()
    {
        lifeSystem = new LifeSystem(totalHP);
        lifeSystem.OnDeath += Dead;
    }

    private void Dead()
    {
        Main.instance.unitController.RemoveObjectFromGrid(this);
        Destroy(gameObject);
    }

    public override void ExecuteAction()
    {
        AskToPassTurn();
    }

    protected void AskToPassTurn()
    {
        Main.instance.unitController.PassTurn(this);
    }
    
    // public virtual void CheckIfVisualFeedbackActionFinished()
    // {
    //     OnUnitFinishedAction?.Invoke();
    // }
    
    public override void ExecuteReaction()
    {
        
    }

    public void TakeDamage(int attackDamage)
    {
        StartCoroutine(TakeDamageFeedback(attackDamage));
    }

    IEnumerator TakeDamageFeedback(int attackDamage)
    {
        bool isTakeDamagefeedbackFinished = false;

        float timer = 0;
        
        while (!isTakeDamagefeedbackFinished)
        {
            timer += Time.deltaTime;

            if (timer >= takeDamageFeedbackDuration)
            {
                timer -= takeDamageFeedbackDuration;
                isTakeDamagefeedbackFinished = true;
            }
            
            print("looping");
            yield return null;    
        }
        
        
        lifeSystem.Damage(attackDamage);
    }
}
