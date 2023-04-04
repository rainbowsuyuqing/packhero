using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public int deviceType = 0; // 0 is PC web, 1 is mobile
        public Joystick joystick;
        public GameObject jumpButton;
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;
        public Collider bossCollider;
        private Collider playerCollider;
        public bool jump;
        bool st1;
        bool pressedJump=false;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
		

       
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public Bounds Bounds => collider2d.bounds;
        
		
        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {

               
                if (Application.isMobilePlatform)
                {
                    deviceType = 1;
                    Debug.Log("Unity WebGL is running on mobile device");
                }
                else
                {
                    joystick.gameObject.SetActive(false);
                    jumpButton.SetActive(false);
                    deviceType = 0;
                    Debug.Log("Unity WebGL is running on desktop");
                }
            }
            else
            {
                Debug.Log("Unity WebGL is not running");
            }
        }


        protected override void Update()
        {
            if (controlEnabled)
            {
                if (deviceType == 0)
                {
                    move.x = Input.GetAxis("Horizontal");
                }
                else if (deviceType == 1)
                {
                    move.x = joystick.Horizontal;
                }
                if (jumpState == JumpState.Grounded && (Input.GetButtonDown("Jump")|| (deviceType == 1 &&pressedJump == true)))
                {
                    jumpState = JumpState.PrepareToJump; 
                }

                else if (Input.GetButtonUp("Jump")|| (deviceType == 1 && pressedJump ==false))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }
		
        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
				
                case JumpState.PrepareToJump:
                    if (IsGrounded && !stopJump)
                    {
                        jumpState = JumpState.Jumping;
                        jump = true;
                        stopJump = false;
                    }
                    break;
                case JumpState.Jumping:
                    Schedule<PlayerJumped>().player = this;
                    jumpState = JumpState.InFlight;
                    break;
                case JumpState.InFlight:
                    if (IsGrounded || stopJump)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    if(deviceType == 1) { pressedJump = false; }
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;
            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
            targetVelocity = move * maxSpeed;
        }
    

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed,
            st1,
        }

        public void ButtonJump()
        {
            pressedJump = true;
        }
    }
}
