
using UnityEngine;
using Managers;
using Grid;
namespace AIFSM
{
    public class NormalZombie : ZombieBase
    {
        private readonly string[] animationNames = { "Walk1_NormalZombie", "Walk2_NormalZombie", "Walk3_NormalZombie" };
        private Animator anim;

   
        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
        }
        protected override void Start()
        {
            base.Start();
            PlayAnimationRandomly();
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            IsAttacking = false;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            Attack();
  }

        private void PlayAnimationRandomly()
        {
            anim.Play(animationNames[Random.Range(0, animationNames.Length)]);
        }

        private void Attack()
        {
            //if the grid where the zombie is in has a plant, and the plant is to its left and at the attack distance, zombie starts attacking
            LogicGrid grid = GridManager.Instance.GetGridByWorldCoordinate(transform.position);
            //Debug.Log(transform.position);
            // if (grid != null&& grid.IsPlanted) 
            if (grid is { IsPlanted: true })
            {
                GameObject plant = grid.Plant;
                float distance = transform.position.x - plant.transform.position.x;
                
                if (distance <= attackRange && distance >= 0)
                {
                    //attack
                    // Debug.Log("attack");
                    IsAttacking = true;
                }
            }
            else
            {
                IsAttacking = false;
            }
        }
        
        private void Move()
        {
            transform.Translate(new Vector3(-speedX * Time.fixedDeltaTime, 0, 0));
        }
        
    }

}
