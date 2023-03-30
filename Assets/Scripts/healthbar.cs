using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platformer.Gameplay;
using Platformer.Mechanics;

public class healthbar : MonoBehaviour
{
    public Slider slider;
    public int healthbarv;
    
    // Start is called before the first frame update
    public void Start()
    {
        slider.maxValue = 10;
        slider.minValue = 0;
    }

    // Update is called once per frame
    public void Update()
    {
        healthbarv = GameObject.Find("Player").GetComponent<Health>().currentHP;
        Debug.Log(healthbarv);
        slider.value = healthbarv;
    }

   
}
