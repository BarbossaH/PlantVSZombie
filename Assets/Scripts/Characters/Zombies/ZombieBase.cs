
using Characters.Attributes;
using Interfaces;
using UnityEngine;
namespace AIFSM
{
    public class ZombieBase : MonoBehaviour,IDamageable
    {
        [SerializeField] protected float speedX;
        [SerializeField] protected float attackRange;
 
        public void TakeDamaged(float damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
        }

        public void Die()
        {
            // Debug.Log("play death animation");
        }
        
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public bool IsAttacking{ get; set; }
        //for test
        public void SetSpeed(float speed)
        {
            speedX = speed;
        }
    }
}