namespace Zombies
{ 
    using UnityEngine;
    using Managers;
    using Grid;
public class NormalZombie : ZombieBase
{
    private readonly string[] animationNames = { "Walk1_NormalZombie", "Walk2_NormalZombie", "Walk3_NormalZombie" };

    private void Start()
    {
        PlayAnimationRandomly();
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
        anim.Play(animationNames[Random.Range(0,animationNames.Length)]);
    }
    public override void Attack()
    {
        //if the grid where the zombie is in has a plant, and the plant is to its left and at the attack distance, zombie starts attacking
        LogicGrid grid = GridManager.Instance.GetGridByWorldCoordinate(transform.position);
        //Debug.Log(transform.position);
        // if (grid != null&& grid.IsPlanted) 
        if(grid is {IsPlanted:true})
        {
            GameObject plant = grid.Plant;
            float distance = transform.position.x- plant.transform.position.x;
            //Debug.Log(distance);

            if ( distance <= attackRange && distance>=0 )
            {
                //attack
                Debug.Log("attack");
            }
        }
    }

    public override void Boomed()
    {
    }

    public override void Die()
    {
    }

    public override void Move()
    {
        transform.Translate(new Vector3(-speedX*Time.fixedDeltaTime,0,0));
    }

    public override void Spawn()
    {
    }

    public override void TakeDamaged()
    {
    }

}

}
