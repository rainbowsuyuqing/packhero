using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    public GameObject AirWall;
    private BoxCollider2D[] colliders = new BoxCollider2D[2];

    

    // Start is called before the first frame update
    void Start()
    { 
        colliders = AirWall.GetComponentsInChildren<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AirWall.SetActive(true);
            //colliders[0].isTrigger = false;
            //colliders[1].isTrigger = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
