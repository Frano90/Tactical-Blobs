using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    public Action OnUnitFinishedAction;
    //public Vector2 posOnGrid;
    public abstract void ExecuteAction();
    //public abstract void SetPosOnGrid(Tile tile);
    public abstract void ExecuteReaction();
    //public abstract void Init();
    
}
