

using Interfaces;
using UnityEngine;
namespace Zombies
{
    public class ZombieBase : MonoBehaviour,IDamageable
    {
        [SerializeField] protected float speedX;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float zombieDamage;
        private float walkSpeed;

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