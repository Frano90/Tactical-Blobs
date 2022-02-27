using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{
    private GridController gridController;
    [SerializeField] private GridUnitLevel_DATA lvlData;

    [SerializeField] private Button play_btt, reset_btt;
    
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
            newGridUnit.GetComponent<UnitSelectorController>().OnUnitSelected += AddUnit;
            newGridUnit.OnUnitFinishedAction += CheckIfCombatIsFinished;
            
        }
    }

    
    //Todo esto se puede ir a una clase de combatManager o algo asi
    
    private Queue<GridObject> inputUnitOrder = new Queue<GridObject>();
    private Queue<GridObject> unitAlreadyActioned = new Queue<GridObject>();
    private bool isTurnLoopPlaying;
    public void ResetOrder()
    {
        ResetGridObjects(inputUnitOrder);
        
        inputUnitOrder = new Queue<GridObject>();
        
    }
    void ResetGridObjects(Queue<GridObject> objs)
    {
        foreach (var item in objs)
        {
            item.GetComponent<UnitSelectorController>().Reset();
        }
    }
    public void AddUnit(GridObject unitClicked)
    {

        if (inputUnitOrder.Contains(unitClicked)) return;
        
        inputUnitOrder.Enqueue(unitClicked);
    }
    public void StartTurnLoop()
    {
        if(inputUnitOrder.Count == 0) return;
        
        isTurnLoopPlaying = true;
        play_btt.interactable = false;
        reset_btt.interactable = false;
        CallUnitToAction();
    }
    private void CallUnitToAction()
    {
        GridObject currentGridObjectActing = inputUnitOrder.Dequeue();
        currentGridObjectActing.ExecuteAction();
        unitAlreadyActioned.Enqueue(currentGridObjectActing);
    }
    void CheckIfCombatIsFinished()
    {
        if (inputUnitOrder.Count == 0)
        {
            OnFinishTurnLoop();
        }
        else
        {
            CallUnitToAction();
        }
        
    }
    void OnFinishTurnLoop()
    {
        ResetGridObjects(unitAlreadyActioned);
        play_btt.interactable = true;
        reset_btt.interactable = true;
        isTurnLoopPlaying = false;
        
    }
    

}
