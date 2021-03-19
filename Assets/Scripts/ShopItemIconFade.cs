using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemIconFade : MonoBehaviour
{
    Image shopItemIconBorder;
    Image shopItemIconImage;
    public float visibleTime = 2f;
    public float fadeTime = 1f;
    float currentVisibleTime = 0f;
    float currentFadeTime = 0f;

    void Awake(){
        shopItemIconBorder = GetComponent<Image>();
        shopItemIconImage = transform.GetChild(0).GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentVisibleTime += Time.deltaTime * 1f;
        if(currentVisibleTime >= visibleTime){
            currentFadeTime += Time.deltaTime * 1f;
            float alpha = 1 - (currentFadeTime/fadeTime);
            if(alpha < 0){
                shopItemIconBorder.color = new Color(
                    shopItemIconBorder.color.r,
                    shopItemIconBorder.color.g,
                    shopItemIconBorder.color.b,
                    0
                );
                shopItemIconImage.color = new Color(
                    shopItemIconImage.color.r,
                    shopItemIconImage.color.g,
                    shopItemIconImage.color.b,
                    0
                );
            }
            else{
                shopItemIconBorder.color = new Color(
                    shopItemIconBorder.color.r,
                    shopItemIconBorder.color.g,
                    shopItemIconBorder.color.b,
                    shopItemIconBorder.color.a - (currentFadeTime/fadeTime)
                );
                shopItemIconImage.color = new Color(
                    shopItemIconImage.color.r,
                    shopItemIconImage.color.g,
                    shopItemIconImage.color.b,
                    shopItemIconImage.color.a - (currentFadeTime/fadeTime)
                );
            }
        }
    }
}
