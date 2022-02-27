using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int xPos, yPos;
    private bool isOccupied;
    [SerializeField] private Material baseMat, offsetMat;
    [SerializeField] private Transform unitContainerPosition;

    public Vector3 GetUnitContainerPosition => unitContainerPosition.transform.position; 
    public void SetGridPosition(int x, int y)
    {
        xPos = x;
        yPos = y;
    }

    public Vector2 GetPosOnGrid => new Vector2(xPos, yPos);
    
    public void Init(bool isOffSet)
    {
        GetComponentInChildren<MeshRenderer>().material = isOffSet ? baseMat : offsetMat;
    }

    public void SetOccupied(bool b)
    {
        isOccupied = b;
    }

    public bool IsOccupied => isOccupied;
}
