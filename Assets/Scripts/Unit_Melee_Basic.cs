using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Melee_Basic : Unit_Movement
{
    [SerializeField] private AttackData[] attacks;
    [SerializeField] private float attackFeedbackDuration = 2f;
    

    public override void ExecuteAction()
    {
        print("EJECUTO");
        Main.instance.unitController.AskToAttackTo(this, attacks);
    }
    
    public void Attack(GridObject desiredTileGetObjectInTile, int attackDamage)
    {
        if (desiredTileGetObjectInTile == null)
        {
            OnFinishAttackAnimation();
            return;
        }
        
        print("Voy a atacar");
        StartVisualFeedback(desiredTileGetObjectInTile, attackDamage);
    }

    public void StartVisualFeedback(GridObject desiredTileGetObjectInTile, int attackDamage)
    {
        StartCoroutine(AttackAnimation(desiredTileGetObjectInTile,attackDamage));
    }
    
    public void StartVisualFeedback()
    {
        
    }

    IEnumerator AttackAnimation(GridObject desiredTileGetObjectInTile, int attackDamage)
    {
        
        bool isAttackfeedbackFinished = false;

        float timer = 0;
        bool isAttackdone = false;
        while (!isAttackfeedbackFinished)
        {
            timer += Time.deltaTime;

            if (!isAttackdone && desiredTileGetObjectInTile != null)
            {
                if (timer / attackFeedbackDuration >= .9f)
                {
                    isAttackdone = true;
                    desiredTileGetObjectInTile.GetComponent<Unit_Basic>().TakeDamage(attackDamage);
                }
            }
            
            
            if (timer >= attackFeedbackDuration)
            {
                timer -= attackFeedbackDuration;
                isAttackfeedbackFinished = true;
                print("se termino de atacar, papa");
            }
            
       
            yield return null;    
        }

        OnFinishAttackAnimation();
        //onFinishAttackAnimation?.Invoke();
    }

    IEnumerator AttackAnimation(Queue<AttackData> attacks)
    {
        
        bool isAttackfeedbackFinished = false;

        float timer = 0;
        bool isAttackdone = false;
        while (!isAttackfeedbackFinished)
        {
            timer += Time.deltaTime;

            if (!isAttackdone)
            {
                if (timer / attackFeedbackDuration >= .9f)
                {
                    isAttackdone = true;
                    foreach (var attackData in attacks)
                    {
                        if(attackData.target == null) continue;
                        
                        attackData.target.GetComponent<Unit_Basic>().TakeDamage(attackData.damage);    
                    }
                    
                }
            }
            
            
            if (timer >= attackFeedbackDuration)
            {
                timer -= attackFeedbackDuration;
                isAttackfeedbackFinished = true;
                print("se termino de atacar, papa");
            }
            
       
            yield return null;    
        }

        OnFinishAttackAnimation();
        //onFinishAttackAnimation?.Invoke();
    }
    void OnFinishAttackAnimation()
    {
        AskToMove(currentDirection, moveRange);
    }
    public override void ExecuteReaction()
    {
        
    }
    public void Attack(Queue<AttackData> desiredTileGetObjectInTile)
    {
        StartCoroutine(AttackAnimation(desiredTileGetObjectInTile));
    }
}

[Serializable]
public struct AttackData
{
    public Direction dir;
    public GridObject target;
    public int range, damage;
}