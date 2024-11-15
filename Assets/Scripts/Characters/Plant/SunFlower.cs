using System.Collections;

namespace Characters.Plant
{
    using UnityEngine;
    using Managers;
    using Conf;
    
    public class SunFlower : PlantBase
    {
        //this script is for creating sun from sunflower, like sky sun manager script

        //when this plant is created but not planted, its animation should not be played until it's planted
        //if it's created for player previewing, it also should be translucent.
        [SerializeField] private float changeColorDuration = 0.5f;


        public override float MaxHealth { get; set; }
        public override float CurrentHealth { get; set; }
        public override float CdDuration { get; set; }

        protected override void Awake()
        {
            base.Awake();
            MaxHealth = 30;
            CurrentHealth = MaxHealth;
            CdDuration = 2f;
        }

        public override void ActivatePlantFunction()
        {
            // InvokeRepeating(nameof(CreateSun), CdDuration, CdDuration);
            StartCoroutine(SunCreateCoroutine());
        }
        

        private IEnumerator SunCreateCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(CdDuration);
                CreateSun();
            }
        }
        private void CreateSun()
        {
            StartCoroutine(ChangeColorRoutine(changeColorDuration,new Color(1.0f, 0.6f, 0),InstantiateSunNormal));
        }
        
        private void InstantiateSunNormal()
        {
            InstantiateSun();
        }
        private void InstantiateSun(SunTypeEnum sunType = SunTypeEnum.Normal)
        {
            Sun sun = GameObject.Instantiate(SunManager.Instance.GetSunPrefabByType(sunType), transform.position,
                Quaternion.identity, SunManager.Instance.transform).GetComponent<Sun>();
            sun.JumpAnimationFromFlower();
        }
    }
}