using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PathMover pathMover = other.GetComponent<PathMover>();

        if(pathMover != null){
            if(pathMover.isMainPlayer){
                pathMover.canMove = false;
                pathMover.points.Clear();
                pathMover.traversedPoints.Clear();
                pathMover.ResetTransform();

            }
        }
    }
}
