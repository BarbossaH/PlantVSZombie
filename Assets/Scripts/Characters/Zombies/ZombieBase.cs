

using System;
using Characters.Attributes;
using Interfaces;
using Managers;
using UnityEngine;
namespace Characters.Zombies
{
    public class ZombieBase : MonoBehaviour,IDamageable
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
                Debug.Log("Low Health");
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
        public void Die()
        {
            // Debug.Log("play death animation");
           anim.SetTrigger(ZombieAnimationParams.Die);
           gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");   
        }
        
        private void DestroyGameObject()
        { 
            //immediately destroy game object looks like a little bad, delay 0.5f second could be better
            //but, I won't do this. lol
            Destroy(gameObject);
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