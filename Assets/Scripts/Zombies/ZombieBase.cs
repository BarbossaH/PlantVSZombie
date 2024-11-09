using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieBase : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] protected float speedX;
    [SerializeField] protected float attackRange;
    protected bool isAttacking;
    protected Animator anim;

    public bool IsAttacking { get { return isAttacking; } }
    //protected
    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public abstract void Move();
    public abstract void Attack();
    public abstract void Die();
    public abstract void Boomed();
    public abstract void Spawn();
    public abstract void TakeDamaged();
}
