using System.Collections;
using Conf;
using Managers;
using UnityEngine;

namespace Characters.Plant
{
   public class Peashooter : PlantBase
    {
        public override void Die()
        {
            
        }

        public override float MaxHealth { get; set; }
        public override float CurrentHealth { get; set; }
        public override float CdDuration { get; set; }
        [SerializeField] private float detectionRange = 14.5f;
        private bool isShooting;
        private Transform gun;
        private Coroutine shootRoutine;
        protected override void Awake()
        {
            base.Awake();
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            //shoot frequency, shoot once per 2 seconds
            CdDuration = 2.0f;
            gun = transform.Find("Gun");

        }
        
        /// <summary>
        /// this function is called when the plant have been planted, because it could be on the mouse
        /// </summary>
        public override void ActivatePlantFunction()
        {
            Debug.Log(CurrentHealth);
            StartDetectZombie();
        }

        private void StartDetectZombie()
        {
            StartCoroutine(DetectZombieRoutine());
        }

        private IEnumerator DetectZombieRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, detectionRange, LayerMask.GetMask("Zombie"));
                if (hit.collider is not null)
                {
                    Shoot();
                }
                else
                {
                    isShooting = false;
                    if (shootRoutine is not null)
                    {
                        StopCoroutine(shootRoutine);
                    }
                }
            }
        }

        private void Shoot()
        {
            //actually, here just needs to change the animation of the plant, and animation will invoke the attack method
            if (!isShooting)
            {
                StartShootCoroutine();
                isShooting = true;
            }
        }

        private void StartShootCoroutine()
        {
            shootRoutine= StartCoroutine(ShootRoutine());
        }

        private IEnumerator ShootRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(2f);
                GameObject bulletPrefab = PlantManager.Instance.GetBulletPrefabByType(PoolTypeEnum.PeaBullet);
                // Debug.Log(bulletPrefab);
                GameObject bullet = GameObject.Instantiate(bulletPrefab, gun.position, Quaternion.identity);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * detectionRange);
        }
        
    }
}