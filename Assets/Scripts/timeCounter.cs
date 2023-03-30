using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeCounter : MonoBehaviour
{
 	public Slider timeSlider;
	float timer=0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime/15;
		if(timer<=1.5f)
		{
			timeSlider.value=timer;
		}
    }
}
