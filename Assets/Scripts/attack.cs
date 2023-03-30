using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class attack : MonoBehaviour
{
    private Vector2 _movementDirection;
    public GameObject Player;
    private float _movementSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _movementSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_movementDirection * _movementSpeed * Time.deltaTime);
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void SetMovementDirection(Vector2 dir)
    {
        _movementDirection = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Player != null)
            {
                Destroy();
            }
        }
    }
}
