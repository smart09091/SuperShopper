using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    public Toggle outputText;
    public GameObject shopIconPrefab;

    void Awake(){
        // GameObject onScreenDisplay = GameObject.FindGameObjectWithTag("OnScreenDisplay");
        
        // GameObject shopIcon = GameObject.Instantiate(shopIconPrefab);
        // shopIcon.transform.parent = onScreenDisplay.transform;
        // shopIcon.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            if(outputText != null){
                outputText.isOn = true;
            }
        }
    }
}
