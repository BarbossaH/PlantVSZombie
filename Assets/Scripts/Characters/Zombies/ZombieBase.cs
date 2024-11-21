

using System;
using Characters.Attributes;
using Conf;
using Interfaces;
using Managers;
using UnityEngine;
namespace Characters.Zombies
{
    public abstract class ZombieBase : MonoBehaviour
    {
        [SerializeField] protected float speedX;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float zombieDamage;
        protected Animator anim;

        private float walkSpeed;
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public bool IsLowHealth { get; private set; }
        private bool headLost;
        public bool IsAttacking { get;protected set; }
        
        public bool StartInvasion { get; set; }
        public abstract PoolTypeEnum PoolType { get; protected set; }
        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            walkSpeed = speedX;
        }

        public void TakeDamaged(float damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth / MaxHealth < 0.3f)
            {
                // Debug.Log("Low Health");
                if (!headLost)
                {
                    ZombieManager.Instance.GenerateHead(transform);
                    headLost = true;
                }
                IsLowHealth = true;
            }
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
        }
        public virtual void AttackPlant(){}
        protected virtual void Die()
        {
            // Debug.Log("play death animation");
           anim.SetTrigger(ZombieAnimationParams.Die);
           gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");   
        }
        
        //for test
        public void SetWalkSpeed()
        {
            speedX = walkSpeed;
        }
        public void SetWalkSpeed(float speed)
        {
            speedX = speed;
        }
    }
}