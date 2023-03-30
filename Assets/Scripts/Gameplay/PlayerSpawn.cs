//using System.Diagnostics;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public static Vector2 lastCheckPointPos=new Vector2(-3,0);

        public void Awake()
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
        }

        public void Update()
        {
            //Debug.Log(lastCheckPointPos);
        }
        public override void Execute()
        {
            var player = model.player;
            player.collider2d.enabled = true;
            player.controlEnabled = false;
            if (player.audioSource && player.respawnAudio)
                player.audioSource.PlayOneShot(player.respawnAudio);
            player.health.Increment();
            player.Teleport(lastCheckPointPos);
            
            
            player.jumpState = PlayerController.JumpState.Grounded;
            player.animator.SetBool("dead", false);
            
            model.virtualCamera.m_Follow = player.transform;
            model.virtualCamera.m_LookAt = player.transform;
            Simulation.Schedule<EnablePlayerInput>(2f);
        }
    }
}