using System.Collections;
using Conf;
using Interfaces;
using Managers;
using UnityEngine.Events;

namespace Characters.Plant
{
    using UnityEngine;

    public abstract class PlantBase : MonoBehaviour, IDamageable
    {
        private Animator anim;
        private SpriteRenderer render;
        private Coroutine currentCoroutine;
        public abstract float MaxHealth { get; set; }
        public abstract float CurrentHealth { get; set; }
        //if plant is sunflower, then it is the duration of generating sun.
        public abstract float CdDuration { get; set; }
        protected virtual void Awake()
        {
            render = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            anim.speed = 0;
        }

        public virtual void ShowPlant(float alpha = 1)
        {
            render.color = new Color(1, 1, 1, alpha);
        }

        public virtual void Plant()
        {
            ShowPlant();
            anim.speed = 1;
            // Invoke("ActivatePlantFunction", 0f);
            ActivatePlantFunction();
        }

        protected IEnumerator ChangeColorRoutine(float duration, Color targetColor, UnityAction callback = null)
        {
            float currentTime = 0f;

            while (currentTime < duration)
            {
                currentTime += 0.05f;
                render.color = Color.Lerp(targetColor, Color.white, currentTime / duration);
                yield return new WaitForSeconds(0.05f);
            }
            render.color = Color.white;
            if(callback !=null){callback();}
            
        }
        public abstract void ActivatePlantFunction();
        public void TakeDamaged(float damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                Die();
                return;
            }
            if(currentCoroutine!=null){StopCoroutine(currentCoroutine);}
            currentCoroutine= StartCoroutine(ChangeColorRoutine(0.5f, new Color(1f, 0.3f, 0.2f)));
        }

        public void Die()
        {
           Destroy(gameObject);
        }

    }
}