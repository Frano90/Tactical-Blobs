using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Wall : GridObject
{
    [SerializeField] private LifeSystem lifeSystem;
    public event Action OnDeath;

    public void Init()
    {
        lifeSystem = GetComponent<LifeSystem>();
    }

    public override void ExecuteAction()
    {
        
    }

    public override void SetParentTile(Tile tile)
    {
        
    }

    public override void ExecuteReaction()
    {
        
    }
}
