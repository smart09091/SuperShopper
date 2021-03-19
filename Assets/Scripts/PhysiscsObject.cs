using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysiscsObject : MonoBehaviour
{
    public Vector3 startingPosition;
    public Quaternion startingRotation;
    void Awake(){
        startingPosition = transform.localPosition;
        startingRotation = transform.localRotation;
    }

    void Start(){
        GameEvents.Instance.onPlayerAfterCollision += ResetTransformValues;
    }
    
    void ResetTransformValues(){
        transform.localPosition = startingPosition;
        transform.localRotation = startingRotation;
    }
}
