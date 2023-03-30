using System;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;
//�˽ű�Player��Enemy���󶼸���ӵ��һ��ʵ��
//�ᱻPlayerEnemyCollision.cs��PlayerDeath�ֱ����
namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 10;
        public int bstatus;
        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        public int currentHP;
        
        public GameObject floatingTxtPre;
        public Text CurrentHpText;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        ///
        
        public void Update()
        {
            int showHP = currentHP * 10;
            bstatus = GameObject.Find("GameController").GetComponent<TokenController>().status;
            if (CurrentHpText != null) 
            {
                CurrentHpText.text = showHP.ToString();
            }
           
        }
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 10, 0, maxHP);
        }

        private void OnTriggerEnter2D(Collider2D collision)//子弹击中玩家
        {
            int playerDmg;
            if (collision.CompareTag("Bullet")) // Check if the collided object is a bullet
            {
                if(gameObject.tag == "Player") 
                {
                    playerDmg = Decrement(true); // Apply 10 damage to the player
                    CreateFloatingText(playerDmg.ToString(), gameObject.transform);
                    Destroy(collision.gameObject); // Destroy the bullet object
                }

            }
        }
        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public int Decrement(bool isPlayer)
        {
            int damageValue = 0;

            if (isPlayer)
            {
                if (bstatus == 0)
                {
                    damageValue = 100;
                    currentHP = Mathf.Clamp(currentHP - 10, 0, maxHP);
                    Debug.Log(currentHP + "-10");
                }
                else if (bstatus == 1)
                {
                    damageValue = 50;
                    currentHP = Mathf.Clamp(currentHP - 5, 0, maxHP);
                    Debug.Log(currentHP + "-5");
                }
                else if (bstatus == 2)
                {
                    damageValue = 20;
                    currentHP = Mathf.Clamp(currentHP - 2, 0, maxHP);
                }
                else if (bstatus == 3)
                {
                    damageValue = 10;
                    currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
                }
            }
            else
            {
                damageValue = 100;
                currentHP = Mathf.Clamp(currentHP - 10, 0, maxHP);
            }

            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }

            return damageValue;
        }
       
        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        /// 
        public void Die()
        {
            while (currentHP > 0) Decrement(false);
        }

        void Awake()
        {
            currentHP = maxHP;
            
        }

        void CreateFloatingText(string Value, Transform charPosition)
        {
            
            //Debug.Log(charPosition);
            Instantiate(floatingTxtPre, new Vector3(charPosition.position.x, charPosition.position.y + 1.0f, charPosition.position.z), Quaternion.identity).GetComponent<FloatingText>().Init("-" + Value);
            

        }

    }



}

