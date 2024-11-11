namespace Characters.Plant
{
    using UnityEngine;

    public abstract class PlantBase : MonoBehaviour
    {
        protected Animator anim;
        protected SpriteRenderer render;

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
            Invoke("ActivatePlantFunction", 0f);

        }

        public abstract void ActivatePlantFunction();
    }
}