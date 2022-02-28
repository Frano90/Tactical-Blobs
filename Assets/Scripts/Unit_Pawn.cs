using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Pawn : Unit_Movement
{
    
    public override void ExecuteAction()
    {
        print("EJECUTO");

        AskToMove(currentDirection, moveRange);
        //Tengo algo adelante?
        //Si, le pego. No, me muevo

    }

    // public override void SetPosOnGrid(Tile tile)
    // {
    //     
    // }

    public override void ExecuteReaction()
    {
        
    }

    void FinishAction()
    {
        OnUnitFinishedAction?.Invoke();
        print("TERMINO EJECUCION");
    }
}
