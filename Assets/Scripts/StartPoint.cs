using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PathMover pathMover = other.GetComponent<PathMover>();
        if(pathMover != null){
            if(ObjectiveTracker.Instance.ObjectivesCleared() && !pathMover.isOnCashRegister){
                Debug.Log("You Lose!");
                ObjectiveTracker.Instance.ResetObjectives();
                pathMover.assignedPathCreator.ClearLineRenderer();
            }else
            if(!ObjectiveTracker.Instance.ObjectivesCleared()){
                Debug.Log("You Lose!");
                ObjectiveTracker.Instance.ResetObjectives();
                pathMover.assignedPathCreator.ClearLineRenderer();
            }
        }
    }
}
