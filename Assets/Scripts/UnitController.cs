using System;
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
            var tileToSpawn = gridController.GetGrid()[new Vector2(item.x, item.y)];

            GridObject newGridUnit = Instantiate<GridObject>(item.gridUnit_pf,tileToSpawn.GetUnitContainerPosition, Quaternion.identity);
            newGridUnit.GetComponent<UnitSelectorController>().OnUnitSelected += AddUnit;
            newGridUnit.OnUnitFinishedAction += CheckIfCombatIsFinished;
            tileToSpawn.SetOccupied(true, newGridUnit);

            gridController.AddNewObjectToTheGrid(newGridUnit, new Vector2(item.x, item.y));
            

        }
    }

    
    //Todo esto se puede ir a una clase de combatManager o algo asi

    #region InitLoop
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
        print("Le toca a la siguiente unidad");
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
        print("Termino el loop");
        ResetGridObjects(unitAlreadyActioned);
        play_btt.interactable = true;
        reset_btt.interactable = true;
        isTurnLoopPlaying = false;
        
    }
    

    #endregion

    #region LoopInteractions

    public void MoveUnit(GridObject unit, Direction dir, int range)
    {
        var currentUnitPos = gridController.GetUnitCoordenates()[unit];
        Vector2 desiredDestination = Vector2.zero;
        
        switch (dir)
        {
            case Direction.N:
                desiredDestination = new Vector2(currentUnitPos.x, currentUnitPos.y + range); 
                break;
            case Direction.NE:
                desiredDestination = new Vector2(currentUnitPos.x + range, currentUnitPos.y + range);
                break;
            case Direction.E:
                desiredDestination = new Vector2(currentUnitPos.x  + range, currentUnitPos.y);
                break;
            case Direction.SE:
                desiredDestination = new Vector2(currentUnitPos.x + range, currentUnitPos.y - range);
                break;
            case Direction.S:
                desiredDestination = new Vector2(currentUnitPos.x, currentUnitPos.y - range);
                break;
            case Direction.SW:
                desiredDestination = new Vector2(currentUnitPos.x - range, currentUnitPos.y - range);
                break;
            case Direction.W:
                desiredDestination = new Vector2(currentUnitPos.x - range, currentUnitPos.y);
                break;
            case Direction.NW:
                desiredDestination = new Vector2(currentUnitPos.x - range, currentUnitPos.y + range);
                break;
        }

        if (!gridController.ThisTileExist((int) desiredDestination.x, (int) desiredDestination.y))
        {
            print("No existe a donde te queres mover");
            unit.OnUnitFinishedAction?.Invoke();
            return;
        }
        
        var desiredTile = gridController.GetGrid()[new Vector2(desiredDestination.x, desiredDestination.y)];

        if (desiredTile.IsOccupied)
        {
            unit.OnUnitFinishedAction();//Todavia no atacan ni nada. Pierden el turno si se chocan con algo
        }
        else
        {
            //Actualziar movimiento hecho en la grid
            gridController.UpdateUnitPosition(unit, currentUnitPos, desiredDestination);
            unit.GetComponent<Unit_Movement>().Move(desiredTile.GetUnitContainerPosition);
        }
    }

    public void PassTurn(Unit_Basic unitBasic)
    {
        unitBasic.OnUnitFinishedAction?.Invoke();
    }

    public void AskToAttackTo(GridObject unit, AttackData[] attacks)
    {
        print("ataque que se van a hacer " + attacks.Length);
        var currentUnitPos = gridController.GetUnitCoordenates()[unit];

        Queue<AttackData> affected = new Queue<AttackData>();
        
        foreach (var attack in attacks)
        {
            Vector2 desiredDestination = Vector2.zero;
            
            switch (attack.dir)
            {
                case Direction.N:
                    desiredDestination = new Vector2(currentUnitPos.x, currentUnitPos.y + attack.range); 
                    break;
                case Direction.NE:
                    desiredDestination = new Vector2(currentUnitPos.x + attack.range, currentUnitPos.y + attack.range);
                    break;
                case Direction.E:
                    desiredDestination = new Vector2(currentUnitPos.x  + attack.range, currentUnitPos.y);
                    break;
                case Direction.SE:
                    desiredDestination = new Vector2(currentUnitPos.x + attack.range, currentUnitPos.y - attack.range);
                    break;
                case Direction.S:
                    desiredDestination = new Vector2(currentUnitPos.x, currentUnitPos.y - attack.range);
                    break;
                case Direction.SW:
                    desiredDestination = new Vector2(currentUnitPos.x - attack.range, currentUnitPos.y - attack.range);
                    break;
                case Direction.W:
                    desiredDestination = new Vector2(currentUnitPos.x - attack.range, currentUnitPos.y);
                    break;
                case Direction.NW:
                    desiredDestination = new Vector2(currentUnitPos.x - attack.range, currentUnitPos.y + attack.range);
                    break;
            }
            
            if (!gridController.ThisTileExist((int) desiredDestination.x, (int) desiredDestination.y))
            {
                print("No existe a donde queres atacar");
                continue;
            }
            
            var desiredTile = gridController.GetGrid()[new Vector2(desiredDestination.x, desiredDestination.y)];
            
            AttackData newAttackData = new AttackData();
            newAttackData.damage = attack.damage;
            newAttackData.range = attack.range;
            newAttackData.target = desiredTile.GetObjectInTile;
            newAttackData.dir = attack.dir;
            
            affected.Enqueue(newAttackData);
            
            //unit.GetComponent<Unit_Melee_Basic>().Attack(desiredTile.GetObjectInTile, attack.damage);

        }
        
        unit.GetComponent<Unit_Melee_Basic>().Attack(affected);
        
    }

    #endregion


    public void RemoveObjectFromGrid(GridObject objectToRemove)
    {
        gridController.RemoveObjectFromGrid(objectToRemove);
        var tileToLiberatePos = gridController.GetUnitCoordenates()[objectToRemove];
    }
}
