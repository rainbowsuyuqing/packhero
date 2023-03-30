using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platformer.Gameplay;
using Platformer.Mechanics;

public class enemybar : MonoBehaviour
{
    public GameObject enemyhealthbar;

    public float healthenemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthenemy = GameObject.Find("Boss").GetComponent<Health>().currentHP;
        enemyhealthbar.transform.localScale = new Vector3(1, healthenemy/50, 1);
            
    }
}
