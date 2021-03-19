using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    public GameObject pathObject;
    public static PathCreator Instance;
    private LineRenderer lineRenderer;
    public List<Vector3> points = new List<Vector3>();
    public List<GameObject> pathPoints = new List<GameObject>();
    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate{};
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public PathMover pathMover;
    public PathMover targetPathMover;
    public bool debugPath = false;
    public bool isDrawingPath = false;

    public void Awake(){
        // if(Instance == null)
        //  {    
        //      Instance = this; // In first scene, make us the singleton.
        //      //DontDestroyOnLoad(gameObject);
        //  }
        //  else if(Instance != this)
        //      Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.

        lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray playerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit playerHitInfo;

        if (Physics.Raycast (playerRay, out playerHitInfo, Mathf.Infinity, playerLayer)){
            //Debug.Log(playerHitInfo.collider.gameObject.name);
            PathMover player = playerHitInfo.collider.GetComponent<PathMover>();
            if(player == targetPathMover){
                //Debug.Log("Player Detected");
                pathMover = player;
                lineRenderer.material = player.playerPathColor;
            }
        }else{
            //Debug.Log("Player Not Detected");
            if(!isDrawingPath)
                pathMover = null;
        }

        
        if(pathMover != null){
            if(Input.GetButtonDown("Fire1")){
                pathMover.points.Clear();
                pathMover.traversedPoints.Clear();

                isDrawingPath = true;
                pathMover.canMove = false;
            }

            if(Input.GetButton("Fire1")){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                //if(Physics.Raycast(ray, out hitInfo)){
                if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity, groundLayer)){
                    if(DistanceToLastPoint(hitInfo.point) > .5f){
                        pathMover.points.Add(hitInfo.point);
                        pathMover.traversedPoints.Add(hitInfo.point);

                        DisplayPath();
                        
                        if(debugPath){
                            GameObject pathPoint = GameObject.Instantiate(pathObject);

                            pathPoint.transform.position = hitInfo.point;

                            pathPoints.Add(pathPoint);
                        }
                    }
                }
            }else
            if(Input.GetButtonUp("Fire1")){
                OnNewPathCreated(points);
                if(pathMover.points.Count > 0){
                    pathMover.currentPoint = pathMover.points[0];
                }
                
                isDrawingPath = false;
                pathMover.canMove = true;
            }
        }
    }

    private float DistanceToLastPoint(Vector3 point){
        if(!pathMover.points.Any())
            return Mathf.Infinity;
        return Vector3.Distance(pathMover.points.Last(), point);
    }

    public void ClearLineRenderer(){
        lineRenderer.positionCount = 0;
    }

    public void DisplayPath(){
        if(pathMover != null){
            lineRenderer.positionCount = pathMover.traversedPoints.Count;
            lineRenderer.SetPositions(pathMover.traversedPoints.ToArray());
        }
    }
}
