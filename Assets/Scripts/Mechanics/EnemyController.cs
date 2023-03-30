using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using Unity.InteractiveTutorials;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public PatrolPath path;
        public AudioClip ouch;
        public GameObject TxtPrefab; //用来在PlayerEnemyCollision里生成预制件
        public Rigidbody2D rb;
        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;
        public bool kick;
        public Bounds Bounds => _collider.bounds;
        public float timeElapsed = 0f;
        bool canDamge = true;
        float coolDownTime = 2f;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, 0);
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {


            var player = collision.gameObject.GetComponent<PlayerController>();
            //if (player != null && canDamge)
            if (player != null)
            {
                //canDamge = false;
                //timeElapsed = 0f;
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = this;
                ev.floatingTxtPre = TxtPrefab;
                kick = true;
            }
            
        }
        

        void Update()
        {
            //timeElapsed += Time.deltaTime;
            //if (timeElapsed > coolDownTime)
            //{
            //    canDamge = true;
            //}

            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
        }

    }
}