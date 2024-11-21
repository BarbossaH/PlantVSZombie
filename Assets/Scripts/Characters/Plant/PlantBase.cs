using System;
using System.Collections;
using Conf;
using Interfaces;
using Managers;
using UnityEngine.Events;
using UnityEngine;
namespace Characters.Plant
{
    public abstract class PlantBase : MonoBehaviour, IDamageable,IDataObserver
    {
        private Animator anim;
        private SpriteRenderer render;
        private Coroutine currentCoroutine;
        public bool IsZombieDetected{get; set;}
        public int Row{get; set;}
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

        protected virtual void Start()
        {
            NotificationCenter.Instance.RegisterObserver(this);
        }

        private void OnDestroy()
        {
            NotificationCenter.Instance.UnregisterObserver(this);
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

        public abstract void Die();
     

        void PlantAttack()
        {
            //有些植物不是攻击植物，所以不能检测射线，比如太阳花和风叶草
            //射线类提供僵尸出现在哪一行的函数，如果没有检测到就设为
            //如果是攻击植物，那就从格子中得出哪一行有僵尸，取出行数，那么把这一行的植物设置成准备攻击
            //如果设置成准备攻击，一旦进入各自植物的攻击范围，植物园就开始攻击
        }

        public void OnDataChanged(EventTypeEnum type, object eventData = null)
        {
            if (type == EventTypeEnum.ZombieDetected && eventData != null )
            {
               IsZombieDetected=(Row==(int)eventData);
               // Debug.Log(IsZombieDetected);
            }
        }
    }
}