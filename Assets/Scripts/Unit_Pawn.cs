using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Pawn : GridObject
{
    [SerializeField] private LifeSystem lifeSystem;
    public event Action OnDeath;
    
    

    public void Init()
    {
        lifeSystem = GetComponent<LifeSystem>();
    }

    public override void ExecuteAction()
    {
        print("EJECUTO");
        
        //Tengo algo adelante?
        //Movete para adelante
        
    }

    public override void SetParentTile(Tile tile)
    {
        
    }

    public override void ExecuteReaction()
    {
        
    }

    void FinishAction()
    {
        OnUnitFinishedAction?.Invoke();
        print("TERMINO EJECUCION");
    }
}
