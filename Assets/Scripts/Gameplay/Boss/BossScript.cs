using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BossMove;

public class BossScript : MonoBehaviour
{
    public GameObject bulletPrefab; // the prefab of the bullet to be fired
    public float bulletSpeed; // the speed at which the bullet will travel
    public float fireTimeGap = 3f; // how often the boss fires
    private float nextFire; // the time when the boss can fire again
    public GameObject BossArea;
    
    public BossState currentState;             // Boss��ǰ״̬
	
	void Start()
	{
		
	}

    void Update()
    {
		currentState = GetComponent<BossMove>().currentState;
        if (Time.time > nextFire && currentState==BossState.Horizontal)
        {
			Debug.Log(currentState);
            nextFire = Time.time + fireTimeGap; // set the next time the boss can fire
            Fire(); // call the Fire() method to shoot the bullets
        }
    }

    void Fire()
    {
        float angleStep = 360f / 8; // the angle between each bullet
        float angle = 0f; // the starting angle

        for (int i = 0; i < 8; i++) // loop through 8 times to fire 8 bullets
        {
            Vector2 bulletDirection = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)); // create a vector for the direction of the bullet
            Vector2 bulletVelocity = bulletDirection * bulletSpeed; // calculate the velocity of the bullet

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // create the bullet from the prefab
            bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity; // set the velocity of the bullet

            Destroy(bullet, 2f); // destroy the bullet after 2 seconds

            angle += angleStep; // increment the angle for the next bullet
        }
    }

}


