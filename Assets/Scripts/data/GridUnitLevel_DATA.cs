using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LvlData")]
public class GridUnitLevel_DATA : ScriptableObject
{
  public List<GridUnitToPlace> gridUnits;
}

[Serializable]
public class GridUnitToPlace
{
  public int x, y;
  public GridObject gridUnit_pf;
}
