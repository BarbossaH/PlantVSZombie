
using Characters.Plant;
using Conf;
using UnityEngine;
using Managers;
using Grid;
using Random = UnityEngine.Random;

namespace Characters.Zombies
{
    public class NormalZombie : ZombieBase
    {
        private readonly string[] animationNames = { "Walk1_NormalZombie", "Walk2_NormalZombie", "Walk3_NormalZombie" };
        // private Animator anim;
        private float timer;
        private readonly float attackDuration = 0.5f;
        private LogicGrid currentGrid;
        private GameObject currentPlant;
        private Rigidbody2D rb;
        public override PoolTypeEnum PoolType { get;protected set; }

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
        }
        protected override void Start()
        {
            base.Start();
            MaxHealth = 10;
            CurrentHealth = MaxHealth;
            IsAttacking = false;
            PoolType= PoolTypeEnum.NormalZombie;
            PlayAnimationRandomly();
        }

        private void FixedUpdate()
        {
            if (CurrentHealth > 0 && StartInvasion)
            {
                Move();
            }
                
        }

        private void Update()
        {
            if (!StartInvasion)
            {
                anim.enabled=false;
            }
            else
            {
                anim.enabled=true;

                // PlayAnimationRandomly();
            }
            CheckCanAttack();
        }

        private void PlayAnimationRandomly()
        {
            anim.Play(animationNames[Random.Range(0, animationNames.Length)]);
        }

        //this is called by finite state machine
        public override void  AttackPlant()
        {
            if (currentPlant is null) return;
            if (timer <= attackDuration)
            {
                timer += Time.deltaTime;
            }
            else
            {
                // int count = GridManager.Instance.GetAllPantCount();
                // Debug.Log(count);
                PlantBase plant =  currentPlant.GetComponent<PlantBase>();
                plant.TakeDamaged(zombieDamage);
                timer = 0;
                //if plant has been eaten up, then reset the data in the grid
                if (plant.CurrentHealth <= 0)
                {
                    //if the plant game object has been destroyed, clear the data of the grid
                    currentGrid.Plant = null;
                    currentGrid.IsPlanted = false;
                    // Debug.Log(GridManager.Instance.GetAllPantCount());
                }

            }
        }
        private void CheckCanAttack()
        {
            //if the grid where the zombie is in has a plant, and the plant is to its left and at the attack distance, zombie starts attacking
            currentGrid = GridManager.Instance.GetGridByWorldCoordinate(transform.position);
            //Debug.Log(transform.position);
            // if (grid != null&& grid.IsPlanted) 
            //if the grid has a plant and the state is planted
            if (currentGrid is { IsPlanted: true })
            {
                GameObject plant = currentGrid.Plant;
                float distance = transform.position.x - plant.transform.position.x;
                //if zombie entering the attack range, then start attacking
                if (distance <= attackRange && distance >= 0)
                {
                    //attack
                    IsAttacking = true;
                    //record the plant near the zombie
                    currentPlant = plant;
                }
                else
                {
                    //if out of attack range
                    IsAttacking = false;
                    currentPlant = null;
                }
            }
            else
            {
                //zombie is not in a grid or in an empty grid
                currentPlant = null;
                IsAttacking = false;
            }
        }
        
        private void Move()
        {
            rb.MovePosition(transform.position + new Vector3(-speedX * Time.fixedDeltaTime, 0, 0));
            // transform.Translate(new Vector3(-speedX * Time.fixedDeltaTime, 0, 0));
        }

        protected override void Die()
        {
            base.Die();
            StartInvasion = false;
            ZombieManager.Instance.ReturnZombieToPool(gameObject, PoolType);
        }
    }

}
