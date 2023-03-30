using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class bulletAttack : MonoBehaviour
{
    private int projectileAmout = 4;
    
    private float startAngle = 110f, endAngle = 300f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / projectileAmout;
        float angle = startAngle;
        for (int i = 0; i < projectileAmout; i++)
        {
            float projDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float projDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);
            Vector3 projMoveVector = new Vector3(projDirX, projDirY, 0f);
            Vector2 projDir = (projMoveVector - transform.position).normalized;
            GameObject proj = OmniShotPool.OmniShotPoolInstance.GetProjectile();
            proj.transform.position = transform.position;
            proj.SetActive(true);
            proj.GetComponent<attack>().SetMovementDirection(projDir);
            angle += angleStep;
            Debug.Log("thisis"+projectileAmout);
        }

       
    }
}
