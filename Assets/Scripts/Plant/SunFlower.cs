namespace Plant
{
    using System.Collections;
    using UnityEngine;
    using Managers;
    public class SunFlower : PlantBase
    {
        //this script is for creating sun from sunflower, like sky sun manager script

        //when this plant is created but not planted, its animation should not be played until it's planted
        //if it's created for player previewing, it also should be translucent.
        [SerializeField] private float changeColorDuration = 0.5f;
        
        public override void ActivatePlantFunction()
        {
            InvokeRepeating(nameof(CreateSun), 3, 10);
        }

        private void CreateSun()
        {
            CreateSun(SunTypeEnum.Normal);
        }

        private void CreateSun(SunTypeEnum sunType = SunTypeEnum.Normal)
        {
            ChangeFlowerColor();
            Sun sun = GameObject.Instantiate(SunManager.Instance.GetSunPrefabByType(sunType), transform.position,
                Quaternion.identity, SunManager.Instance.transform).GetComponent<Sun>();
            sun.JumpAnimationFromFlower();
        }

        private void ChangeFlowerColor()
        {
            StartCoroutine(ChangeColorRoutine());
        }

        private IEnumerator ChangeColorRoutine()
        {
            float currentTime = 0f;

            while (currentTime < changeColorDuration)
            {
                currentTime += 0.05f;
                render.color = Color.Lerp(Color.white, new Color(1.0f, 0.6f, 0), currentTime / changeColorDuration);
                yield return new WaitForSeconds(0.05f);
            }

            render.color = Color.white;
        }

    }
}