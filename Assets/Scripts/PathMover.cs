using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PathMover : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    public List<Vector3> traversedPoints = new List<Vector3>();
    public Vector3 startPosition;
    public Quaternion startRotation;
    public bool isOnCashRegister = false;
    public bool canMove;
    public float speed = 5f;
    public float WPRadius = 1f;
    public Vector3 currentPoint;
    public Material playerPathColor;
    public PathCreator assignedPathCreator;
    public bool isMainPlayer = false;
    public List<PathMover> players;
    public bool hasCollided = false;
    public float collisionTime = 3f;
    public float currentCollisionTime = 0;

    private void Awake(){
        startPosition = transform.position;
        startRotation = transform.rotation;

        players = FindObjectsOfType<PathMover>().ToList<PathMover>();
        Debug.Log(players.Count);
    }

    private void Start(){
        
        GameEvents.Instance.onPlayerAfterCollision += HandlePlayerAfterCollision;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdatePathing();
        CheckCollisionTimer();
    }
    

    private void UpdatePathing(){
        if(canMove){
            if(Vector3.Distance(transform.position, currentPoint) > WPRadius){
                
                transform.position = Vector3.MoveTowards(transform.position, currentPoint, Time.fixedDeltaTime * speed);
                transform.LookAt(currentPoint);
            }else{
                if(traversedPoints.Count > 0){
                    
                    traversedPoints.RemoveAt(0);
                    if(traversedPoints.Count > 0){
                        currentPoint = traversedPoints[0];
                    }

                    assignedPathCreator.DisplayPath();
                }else{
                    bool playersAreMoving = false;

                    foreach(PathMover player in players){
                        if(player.traversedPoints.Count > 0){
                            playersAreMoving = true;
                        }
                    }

                    if(!playersAreMoving){
                        canMove = false;
                        if(!isOnCashRegister){
                            ResetTransform();
                            ObjectiveTracker.Instance.ResetObjectives();
                        }
                        
                        assignedPathCreator.ClearLineRenderer();
                    }
                }
            }
        }
    }

    public void HandlePlayerDuringCollision(){
        canMove = false;
        currentCollisionTime = 0f;
        hasCollided = true;
    }

    private void CheckCollisionTimer(){
        if(hasCollided){
            currentCollisionTime += Time.fixedDeltaTime * 1f;
            if(currentCollisionTime > collisionTime){
                GameEvents.Instance.BeforeResettingPhysicsObjects();
                GameEvents.Instance.PlayerAfterCollision();
                hasCollided = false;
                currentCollisionTime = 0f;
            }
        }
    }

    public void HandlePlayerAfterCollision(){
        ResetTransform();
        ObjectiveTracker.Instance.ResetObjectives();
        assignedPathCreator.ClearLineRenderer();
    }

    public void ResetTransform(){
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
