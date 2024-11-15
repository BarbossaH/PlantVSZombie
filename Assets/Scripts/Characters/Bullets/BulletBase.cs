
using UnityEngine;

namespace Characters.Bullets
{
    public abstract class BulletBase : MonoBehaviour
    {
        protected abstract float AutorotationSpeed { get; set; }
        protected abstract float Damage { get; set; }
        protected abstract void AutoRotate();
        protected abstract void AutoMove();
        protected abstract void Attack();
      
        protected virtual void Start()
        {
            AutoRotate();
        }


        // private void OnCollisionEnter(Collision other)
        // {
        //     if (other.gameObject.layer == LayerConstants.ZombieLayer)
        //     {
        //         //todo: attack zombie
        //     }
        // }
    }
}