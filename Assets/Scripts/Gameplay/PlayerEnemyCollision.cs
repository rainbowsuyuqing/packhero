using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using UnityEngine.Timeline;
using static Platformer.Core.Simulation;



namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public GameObject attack;
        
        public EnemyController enemy;
        public PlayerController player;
        public GameObject floatingTxtPre; //Í¨¹ýEnemyController´«µÝ
        public int bstatus;
     
        //TokenController token = new TokenController();
        //void Start()
			//{
				//bstatus = GameObject.Find("GameController").GetComponent<TokenController>().status;
				
			//}
        
        //;
        //private int bottaStatus = bottaObject.status;
        //int bottaStatus = TokenController.status;
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
     

        public void Start()
        {
            bstatus = GameObject.Find("GameController").GetComponent<TokenController>().status;
        }
        public override void Execute()
        {

            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;
            //bstatus = GameObject.Find("GameController").GetComponent<TokenController>().status;
            //Debug.Log(bstatus);
            int enemyDmg, playerDmg;
            var playerHealth = player.GetComponent<Health>();
            if (willHurtEnemy )
            {
              
               
                var enemyHealth = enemy.GetComponent<Health>();

                if (enemyHealth != null )
                {
                    
                    //attack.SetActive(true);
                    enemyDmg = enemyHealth.Decrement(false);
                    CreateFloatingText(enemyDmg.ToString(), enemy.transform);
                    
                    
		
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                        player.Bounce(2);
                    }
                    else
                    {
                       player.Bounce(7);
                    }
                }
				
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                }
            }
            else if (playerHealth != null)
            {
                if (playerHealth.IsAlive)
                {
                   playerDmg= playerHealth.Decrement(true);
                   CreateFloatingText(playerDmg.ToString(), player.transform);
                }
                else
                {
                    Schedule<PlayerDeath>();
                }
            }

        }

        void CreateFloatingText(string Value,Transform charPosition)
        {
            //Debug.Log(charPosition);
            Object.Instantiate(floatingTxtPre, new Vector3(charPosition.position.x,charPosition.position.y+1.0f,charPosition.position.z), Quaternion.identity).GetComponent<FloatingText>().Init("-" + Value);
            
            

        }
    }
}

