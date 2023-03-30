using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the health component on an enemy has a hitpoint value of  0.
    /// </summary>
    /// <typeparam name="EnemyDeath"></typeparam>
    public class EnemyDeath : Simulation.Event<EnemyDeath>
    {
        public EnemyController enemy;
        public BossScript boss;
        

        

        public override void Execute()
        {
            boss = enemy.GetComponent<BossScript>();
            var bossShoot = enemy.GetComponent<BossScript>();
            var bossMove = enemy.GetComponent<BossMove>();
            enemy._collider.enabled = false;
            enemy.control.enabled = false;
            enemy.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            if (enemy._audio && enemy.ouch)
                enemy._audio.PlayOneShot(enemy.ouch);
            
            if(boss!= null)
            {
                Object.Destroy(boss.BossArea);
            }
            Object.Destroy(enemy.gameObject,5f);
            if (bossMove != null)
            {
                Object.Destroy(bossMove);
            }

            if (bossShoot != null)
            {
                Object.Destroy(bossShoot);
            }


        }
    }
}