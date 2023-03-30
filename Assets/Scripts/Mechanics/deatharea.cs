using System;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
public class deatharea : MonoBehaviour
{
    public int currentHP;
    public int damageValue;
    public int bstatus;
    public int maxHP;
    public bool enter;
    
    // Start is called before the first frame update
    void Start()
    {
        
        currentHP = GameObject.Find("Player").GetComponent<Health>().currentHP;
        maxHP = GameObject.Find("Player").GetComponent<Health>().maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        bstatus = GameObject.Find("GameController").GetComponent<TokenController>().status;
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            enter=true;
            //currentHP = Mathf.Clamp(currentHP - 10, 0, maxHP);
          if (bstatus==0)
            {
                damageValue = 10;
                currentHP = Mathf.Clamp(currentHP - damageValue, 0, maxHP);
                Debug.Log(currentHP+"-10");
            }
            else if (bstatus==1)
            {
                damageValue = 10;
                currentHP = Mathf.Clamp(currentHP - damageValue, 0, maxHP);
                Debug.Log(currentHP + "-5");
            }
            else if (bstatus==2)
            {
                damageValue = 10;
                currentHP = Mathf.Clamp(currentHP - damageValue, 0, maxHP);
            }
          else if (bstatus==3)
          {
              damageValue = 10;
              currentHP = Mathf.Clamp(currentHP - damageValue, 0, maxHP);
          }
            if (currentHP == 0)
            {
             GameObject.Find("Player").GetComponent<Health>().Die();
            }
            
        }
    }
}
}
