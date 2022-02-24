using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private GridController gridController;
    [SerializeField] private GridUnitLevel_DATA lvlData;
    
    // Start is called before the first frame update
    void Start()
    {
        gridController = GetComponent<GridController>();

        SpawnUnits();
    }

    private void SpawnUnits()
    {
        foreach (var item in lvlData.gridUnits)
        {
            GridObject newGridUnit = Instantiate<GridObject>(item.gridUnit_pf, gridController.GetGrid()[new Vector2(item.x, item.y)].GetUnitContainerPosition, Quaternion.identity);
            newGridUnit.GetComponent<UnitSelectorController>().OnUnitClicked += RefreshUnitLoopOrder;
        }
    }

    void RefreshUnitLoopOrder(GridObject unit)
    {
        
    }

}
