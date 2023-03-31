using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class openlink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void controlUrl()
    {
        Application.OpenURL("https://www.botta.it/eco-packaging/eco-protective-packaging/bottiglie-bottle-packaging/");
    }

    public void openLinkTape()
    {
        
        Application.OpenURL("https://www.botta.it/en/eco-packaging/eco-closures-and-sealing/");
    }
    
}
