using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Basic : GridObject
{
    [SerializeField] protected LifeSystem lifeSystem;
    public event Action OnDeath;

    public override void Init()
    {
        lifeSystem = new LifeSystem();
    }

    public override void ExecuteAction()
    {
        
    }

    public virtual void CheckIfVisualFeedbackActionFinished()
    {
        OnUnitFinishedAction?.Invoke();
    }
    
    public override void ExecuteReaction()
    {
        
    }
}
