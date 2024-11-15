

using System;
using Characters.Attributes;
using Interfaces;
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
        
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }

        public bool IsAttacking { get;protected set; }
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