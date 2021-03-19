using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoverScript : MonoBehaviour
{
    public List<GameObject> points = new List<GameObject>();
    int pathIndex = 0;
    public float speed = 10f;
    public float WPRadius = 1.0833f;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, points[pathIndex].transform.position) > WPRadius){
            
            transform.position = Vector3.MoveTowards(
                transform.position
                , points[pathIndex].transform.position
                , Time.deltaTime * speed
            );
            transform.LookAt(points[pathIndex].transform.position);
        }else{
            pathIndex++;
            if(pathIndex >= points.Count){
                pathIndex = 0;
            }
        }
        
    }
}
