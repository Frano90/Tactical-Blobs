using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    public abstract void ExecuteAction();
    public abstract void SetParentTile(Tile tile);
    public abstract void ExecuteReaction();
}
