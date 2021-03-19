using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    public void Awake(){
        Instance = this;
    }
    
    public event Action onPlayerAfterCollision;
    public event Action onBeforeResettingPhysicsObjects;

    public void PlayerAfterCollision(){
        if(onPlayerAfterCollision != null){
            onPlayerAfterCollision();
        }
    }

    public void BeforeResettingPhysicsObjects(){
        if(onBeforeResettingPhysicsObjects != null){
            onBeforeResettingPhysicsObjects();
        }
    }
}
