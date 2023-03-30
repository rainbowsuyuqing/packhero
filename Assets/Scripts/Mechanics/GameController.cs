using System.Diagnostics;
using Platformer.Core;
using Platformer.Model;
using UnityEngine.UI;
using UnityEngine;
using Unity.InteractiveTutorials;


namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary>
    /// 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public Text length;
        public int collectedToken;
        public int tokenLength;
        public int collectedEndint;
        public Text collectedEnd;
        public Text Name;
        public Text Name2;
        public Text Status;
        public int statusif;
        //public static Vector2 lastCheckPointPos;
        
        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void start()
        {
            //GameObject.Find("Player").transform.position = lastCheckPointPos;
        }
        void Update()
        {
            
            tokenLength = GameObject.Find("GameController").GetComponent<TokenController>().tokensLength;
            if (Instance == this) Simulation.Tick();
            collectedToken = 130 - tokenLength;
            collectedEndint = 130 - tokenLength;
            length.text = collectedToken.ToString();
            collectedEnd.text = collectedEndint.ToString();
            statusif = GameObject.Find("GameController").GetComponent<TokenController>().status;
            if (statusif==1)
            {
               
                Name.text = "Eco-Sleeve";
				Name2.text="Eco-Paper Tape";
                Status.text = "2/4";
            }
            else if(statusif==2)
            {
                Name.text = "Flexi-Hex";
				Name2.text="Eco Strapping";
                Status.text = "3/4";
            }
            else if(statusif==3)
            {
                Name.text = "Eco PinchTop Box";
				Name2.text="Eco-Wallet";
                Status.text = "4/4";
            }
  
        }
    }
}