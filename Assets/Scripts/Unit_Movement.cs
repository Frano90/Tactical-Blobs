
using System.Collections;
using UnityEngine;

public class Unit_Movement : Unit_Basic
{

    [SerializeField] protected int moveRange;
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private float speed = 2;
    [SerializeField] protected Direction currentDirection;
    Vector3 currentTileDestination = Vector3.zero;
        
        protected void AskToMove(Direction dir, int moveRange)
        {
            Main.instance.unitController.MoveUnit(this, dir, moveRange);
        }

        public void Move(Vector3 destination)
        {
            currentTileDestination = destination;
            StartVisualFeedback();
        }

        public void StartVisualFeedback()
        {
            StartCoroutine(MovingUnit());
        }

        IEnumerator MovingUnit()
        {
            bool isMovingfeedbackFinished = false;

            float _current = 0;
            float _target = 1;

            // print("la distancia al siguiente es " + Vector3.Distance(transform.position, currentTileDestination));
            //
            // print("yo quiero ir a " + currentTileDestination + " y estoy en " + transform.position);
            
            float time = Vector3.Distance(transform.position, currentTileDestination) / speed;
            
            while (!isMovingfeedbackFinished)
            {
                //print("looping");
                _current = Mathf.MoveTowards(_current, _target, time * Time.deltaTime);

                transform.position = Vector3.Lerp(transform.position, currentTileDestination, _movementCurve.Evaluate(_current));

                //print("cur " + _current + "  target " + _target);
                
                if (_current >= _target)
                {
                    isMovingfeedbackFinished = true;
                    print("llegue a donde queria");
                }
                
                 
                
                yield return null;    
            }

            
            
            OnFinishMovement();
        }

        protected virtual void OnFinishMovement()
        {
            print("termine de moverme");
            OnUnitFinishedAction?.Invoke();
            //CheckIfVisualFeedbackActionFinished();
        }

        public override void ExecuteAction()
        {
            
        }

        // public override void SetPosOnGrid(Tile tile)
        // {
        //     
        // }

        public override void ExecuteReaction()
        {
            
        }

       
    }

public enum Direction
{
    N,
    NE,
    E,
    SE,
    S,
    SW,
    W,
    NW
}