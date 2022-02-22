using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private GridController gridController;
    [SerializeField] private GridObject wall_pf;
    
    // Start is called before the first frame update
    void Start()
    {
        gridController = GetComponent<GridController>();

        SpawnWalls();
    }

    private void SpawnWalls()
    {
        GridObject newWall = Instantiate<GridObject>(wall_pf, gridController.GetRandomTile().GetUnitContainerPosition, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
