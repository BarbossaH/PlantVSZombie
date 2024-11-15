using System.Collections;
using Characters.Zombies;
using Conf;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Characters.Bullets
{
    public class PeaBullet:BulletBase
    {
        private Rigidbody2D rb;

        protected override float AutorotationSpeed { get; set; } = 0.01f;
        protected override float Damage { get; set; }
        private SpriteRenderer spriteRenderer;
        [SerializeField] private float speed = 3;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer=GetComponent<SpriteRenderer>();
        }
        protected override void Start()
        {
            base.Start();
            AutoMove();
            DestroyBullet(5);
        }
        private void OnTriggerEnter2D (Collider2D other)
        {
            if (other.gameObject.layer==LayerConstants.ZombieLayer)
            {
                spriteRenderer.sprite = PlantManager.Instance.GetBulletHitSpriteByType(BulletTypeEnum.PeaBullet);
                rb.velocity = Vector2.zero;
                rb.gravityScale = 1;
                DestroyBullet();
                other.gameObject.GetComponent<ZombieBase>().TakeDamaged(3.0f);
            }
        }

        private void DestroyBullet(float delay=0.2f)
        {
            StartCoroutine(DestroyBulletRoutine(delay));
        }

        private IEnumerator DestroyBulletRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }


        protected override void AutoRotate()
        {
            StartCoroutine(RotateRoutine());
        }

        private IEnumerator RotateRoutine()
        {
            while (true)
            {
                yield return null;
                //y-axis: Vector3.up, x-axis:Vector3.right
                transform.RotateAround(transform.position, Vector3.back, AutorotationSpeed);
            }
        }
        protected override void AutoMove()
        {
            // StartCoroutine(MoveRoutine());
            rb.AddForce(Vector2.right*speed, ForceMode2D.Impulse);
        }

        // private IEnumerator MoveRoutine()
        // {
        //     while (true)
        //     {
        //         yield return  null;
        //         transform.position += Vector3.right * AutorotationSpeed;
        //     }
        // }
        
        
        protected override void Attack()
        {
        }
    }
}