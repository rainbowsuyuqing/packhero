using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
	public float timeleft;
	public float timeUsedf;
	public Text txt_time;
	//public AudioClip countsound;
	bool isCounting;
	int hour;
	int minute;
	int second;
	int minute2;
	int second2;
	public GameObject fail;
	bool tutorialIsOn;
	public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject Success;
	public Text timeUsed;
	
	
    // Start is called before the first frame update
    void Start()
    {
		isCounting=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(tutorial1.active==true||tutorial2.active==true||tutorial3.active==true||Success.active==true)
			{
				isCounting=false;
			}
		else{
				isCounting=true;
			}
	    if (isCounting==true)
	    {
		    
		    timeleft -= Time.deltaTime;
		    minute = (int)timeleft /60;
		    second = (int)(timeleft - minute * 60);
			timeUsedf =240f-timeleft;
   			minute2 = (int)timeUsedf /60;
			second2 = (int)timeUsedf % 60;
	    }

	    txt_time.text = string.Format("{0:D2}:{1:D2}",minute,second);
		timeUsed.text= string.Format("{0:D2}:{1:D2}",minute2,second2);
	    if (timeleft <= 30 && isCounting)
	    {
		    txt_time.color = Color.red;

		    //this.GetComponent<AudioSource>().PlayOneShot(countSound);
	    }
	    if (timeleft <= 0 && isCounting)
	    {
		    isCounting = false;
		    fail.SetActive(true);

		    //this.GetComponent<AudioSource>().PlayOneShot(countSound);
	    }
    }
}
