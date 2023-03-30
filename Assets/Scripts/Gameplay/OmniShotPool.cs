using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OmniShotPool : MonoBehaviour
{
    public static OmniShotPool OmniShotPoolInstance;

    [SerializeField] 
    private GameObject pooledOmniShot;

    private bool needProjectiles = true;
    private List<GameObject> projectiles;
    
    // Start is called before the first frame update
    private void Awake()
    {
        OmniShotPoolInstance = this;
    }
    void Start()
    {
        projectiles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetProjectile()
    {
        if (projectiles.Count > 0)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (!projectiles[i].activeInHierarchy)
                {
                    return projectiles[i];
                }
            }
        }

        if (needProjectiles)
        {
            GameObject proj = Instantiate(pooledOmniShot);
            proj.SetActive(false);
            projectiles.Add(proj);
            return proj;

        }
        
        return null;
    }
}
