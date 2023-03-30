using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TMP_Text text;
    float alpha = 1;


    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeAlphaAndPos();


    }

    public void Init(string damage)
    {
        text.text = damage;

        alpha = 1;
    }

    void ChangeAlphaAndPos()
    {
        if(alpha<=0)
        {
            Object.DestroyImmediate(gameObject); 
            return;
        }
        alpha -= 0.01f;
        transform.position += Vector3.up * Time.fixedDeltaTime * 2.0f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        
    }

}
