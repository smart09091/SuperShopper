using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemIconPulse : MonoBehaviour
{
    bool enlargeIcon = true;
    public float pulseRate = .1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enlargeIcon){
            transform.localScale = new Vector3(
                transform.localScale.x + (pulseRate * Time.deltaTime),
                transform.localScale.y + (pulseRate * Time.deltaTime),
                transform.localScale.z
                
            );
            if(transform.localScale.x >= 1.25f){
                enlargeIcon = !enlargeIcon;
            }
        }else{
            transform.localScale = new Vector3(
                transform.localScale.x - (pulseRate * Time.deltaTime),
                transform.localScale.y - (pulseRate * Time.deltaTime),
                transform.localScale.z
                
            );
            if(transform.localScale.x <= 1f){
                enlargeIcon = !enlargeIcon;
            }

        }
    }
}
