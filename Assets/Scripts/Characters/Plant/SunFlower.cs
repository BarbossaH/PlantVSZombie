
using Conf;
using UnityEngine;
using Managers;
namespace Characters.Plant
{
    public class SunFlower : PlantBase
    {
        //this script is for creating sun from sunflower, like sky sun manager script

        //when this plant is created but not planted, its animation should not be played until it's planted
        //if it's created for player previewing, it also should be translucent.
        [SerializeField] private float changeColorDuration = 0.5f;


        public override float MaxHealth { get; set; }
        public override float CurrentHealth { get; set; }
        public override float CdDuration { get; set; }
        private float currentTime ;
        private bool isPlanted;
        private Coroutine createSunRoutine;
        protected override void Awake()
        {
            base.Awake();
            MaxHealth = 30;
            CurrentHealth = MaxHealth;
            CdDuration = 2f;
        }

        private void Update()
        {
            if (isPlanted)
            {
                currentTime += Time.deltaTime;
                if (currentTime>=CdDuration)
                {
                    //create a sun
                    CreateSun();
                    currentTime = 0f;
                }
            }
        }

        public override void ActivatePlantFunction()
        {
            isPlanted = true;
        }
        
        private void CreateSun()
        {
            if (createSunRoutine != null)
            {
                StopCoroutine(createSunRoutine);
            }
            createSunRoutine=  StartCoroutine(ChangeColorRoutine(changeColorDuration,new Color(1.0f, 0.6f, 0),InstantiateSun));
        }
 
        private void InstantiateSun()
        {
            // Sun sun = GameObject.Instantiate(SunManager.Instance.GetSunPrefabByType(sunType), transform.position,
            //     Quaternion.identity, SunManager.Instance.transform).GetComponent<Sun>();
            Sun sun = ObjectPoolManager.Instance.GetObject(PoolTypeEnum.Sun, transform.position, Quaternion.identity)
                .GetComponent<Sun>();  
            sun.JumpAnimationFromFlower();
        }

        public override void Die()
        {
            // Debug.Log(123);
            ObjectPoolManager.Instance.ReturnObject(PoolTypeEnum.Sunflower,gameObject);
        }
    }
}