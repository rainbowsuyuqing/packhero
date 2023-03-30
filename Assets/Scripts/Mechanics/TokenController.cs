using System.Collections;
using System.Collections.Generic;
using Unity.InteractiveTutorials;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class animates all token instances in a scene.
    /// This allows a single update call to animate hundreds of sprite 
    /// animations.
    /// If the tokens property is empty, it will automatically find and load 
    /// all token instances in the scene at runtime.
    /// </summary>
    public class TokenController : MonoBehaviour
    {
        [Tooltip("Frames per second at which tokens are animated.")]
        public float frameRate = 12;
        [Tooltip("Instances of tokens which are animated. If empty, token instances are found and loaded at runtime.")]
        public TokenInstance[] tokens;
        float nextFrameTime = 0;
        public GameObject Player;
        public RuntimeAnimatorController controller1, controller2, controller3, controller4;
        public AnimationClip change;
        public int tokensLength;
        public GameObject popup1;
        bool showpopup1;
        public int status;
        public GameObject popup2;
        public GameObject popup3;
        public Camera camera;
        public GameObject icon1;
        public GameObject icon2;
        public GameObject icon3;
        public GameObject icon4;
        public GameObject Tutorial1;
        public GameObject Tutorial2;
        public GameObject Tutorial3;


        [ContextMenu("Find All Tokens")]
        void FindAllTokensInScene()
        {
            tokens = UnityEngine.Object.FindObjectsOfType<TokenInstance>();
            
        }

        void Awake()
        {
            
            tokensLength=130;
            
            
            //if tokens are empty, find all instances.
            //if tokens are not empty, they've been added at editor time.
            
            if (tokens.Length == 0)
                FindAllTokensInScene();
                Debug.Log("getalltoken");
                
            //Register all tokens so they can work with this controller.
            for (var i = 0; i < tokens.Length; i++)
            {
                tokens[i].tokenIndex = i;
                tokens[i].controller = this;
                
                
            }
        }

        void Update()
        {
            
        if(popup1.active==true||popup2.active==true||popup3.active==true)
            {
                showpopup1=true;
            }
        else
            {
                showpopup1=false;
            }
       

        if (showpopup1==true)
            {
                Time.timeScale=0f;
            }
        
        else
            {
                Time.timeScale=1f;
            }
            //if it's time for the next frame...
            if (Time.time - nextFrameTime > (1f / frameRate))
            {
                //update all tokens with the next animation frame.
                for (var i = 0; i < tokens.Length; i++)
                {
                    
                    var token = tokens[i];
                    //if token is null, it has been disabled and is no longer animated.
                    if (token != null)
                    {
                        token._renderer.sprite = token.sprites[token.frame];
                        if (token.collected && token.frame == token.sprites.Length - 1)
                        {

                            tokensLength=tokensLength-1;
                            Debug.Log(tokensLength);
                            //collectedToken = 130 - tokensLength;
                            //HP_score.text = tokensLength.ToString();
                            //Debug.Log(tokens.Length);
                            token.gameObject.SetActive(false);
                            tokens[i] = null;
                            if(tokensLength ==115)
                            {
                                camera.GetComponent<Camera>().backgroundColor = new Color(205f/255f, 225f/255f, 224f/255f); //=new Color(21,54,78);
                                popup1.SetActive(true);
                                Player.GetComponent<Animator>().runtimeAnimatorController = controller2;
                            }
                            else if (tokensLength == 80)
                            {
                                popup2.SetActive(true);
                                Player.GetComponent<Animator>().runtimeAnimatorController = controller3;
                                
                                camera.GetComponent<Camera>().backgroundColor =new Color(199f/255f,239f/255f,237f/255f);
                                
                            }
                            else if (tokensLength == 45)
                            {
                                popup3.SetActive(true);
                                Player.GetComponent<Animator>().runtimeAnimatorController = controller4;
                                
                                camera.GetComponent<Camera>().backgroundColor =new Color(191f/255f,240f/255f,255f/255f);
                                
                            }

                            if (tokensLength<=115 && tokensLength>80)
                            {
                                icon2.SetActive(true);
                                camera.GetComponent<Camera>().cullingMask=LayerMask.GetMask("Default","TransparentFX","Ignore Raycast","Water","UI","p2plants");
                                status = 1;
                                Debug.Log(status+"tokenok");

                            }
                            else if (tokensLength <= 80 && tokensLength>45)
                            {
                                icon3.SetActive(true);
                                camera.GetComponent<Camera>().cullingMask=LayerMask.GetMask("Default","TransparentFX","Ignore Raycast","Water","UI","p3plants");
                                status = 2;
                            }
                            else if (tokensLength <= 45)
                            {
                                icon4.SetActive(true);
                                camera.GetComponent<Camera>().cullingMask=LayerMask.GetMask("Default","TransparentFX","Ignore Raycast","Water","UI","p4plants");
                                status = 3;
                            }
                        }
                        else
                        {
                            token.frame = (token.frame + 1) % token.sprites.Length;
                        }
                    }
                }
                
                //calculate the time of the next frame.
                nextFrameTime += 1f / frameRate;

            }
        }

    }
}
