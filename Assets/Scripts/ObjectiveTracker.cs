using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectiveTracker : MonoBehaviour
{
    public static ObjectiveTracker Instance;
    public List<Toggle> objectives;
    public GameObject winPanel;
    public string sceneToLoad = "";
    public string nextLevel = "";

    void Awake(){
        if(Instance == null)
         {    
             Instance = this; // In first scene, make us the singleton.
             //DontDestroyOnLoad(gameObject);
         }
         else if(Instance != this)
             Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.

        winPanel.SetActive(false);

        //DontDestroyOnLoad(this.gameObject);
    }

    void Update(){
        
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PathMover pathMover = other.GetComponent<PathMover>();
        if(pathMover != null && !pathMover.hasCollided){
            pathMover.isOnCashRegister = true;
            if(!ObjectiveTracker.Instance.ObjectivesCleared()){
                Debug.Log("You Lose!");
                ObjectiveTracker.Instance.ResetObjectives();
                //PathCreator.Instance.ClearLineRenderer();
                pathMover.isOnCashRegister = false;
                pathMover.canMove = false;
                // pathMover.transform.position = pathMover.startPosition;
                // pathMover.transform.rotation = pathMover.startRotation;
                pathMover.ResetTransform();
                pathMover.points.Clear();
                pathMover.traversedPoints.Clear();
                pathMover.assignedPathCreator.ClearLineRenderer();
            }else{
                winPanel.SetActive(true);
            }

            //PathCreator.Instance.ClearLineRenderer();
        }
    }
    public bool ObjectivesCleared(){
        
        foreach(Toggle objective in objectives){
            if(!objective.isOn){
                return false;
            }
        }

        return true;
    }

    public void ResetObjectives(){
        foreach(Toggle objective in objectives){
            objective.isOn = false;
        }
    }

    public void LoadNextLevel(){
        SceneManager.LoadScene(nextLevel);
    }
}
