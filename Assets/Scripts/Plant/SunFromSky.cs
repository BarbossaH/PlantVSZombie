namespace Plant
{
    using UnityEngine;
    using Managers;
    public class SunFromSky : MonoBehaviour
    {
        //this class is only for generating sun from sky, and set the relative arguments, like SunFlower script
        private readonly float spawnSunPosY = 6f;

        private readonly float spawnSunPosMinX = -5.5f;
        private readonly float spawnSunPosMaxX = 5f;

        private readonly float sunLandPosMinY = -3.5f;
        private readonly float sunLandPosMaxY = 3f;

        private void Start()
        {
            InvokeRepeating(nameof(CreateSun), 2, 15);

        }

        private void CreateSun()
        {
            CreateSun(SunTypeEnum.Normal);
        }

        private void CreateSun(SunTypeEnum sunType = SunTypeEnum.Normal)
        {
            Sun sun = GameObject.Instantiate(SunManager.Instance.GetSunPrefabByType(sunType), Vector3.zero,
                Quaternion.identity, SunManager.Instance.transform).GetComponent<Sun>();
            float landingPosY = Random.Range(sunLandPosMinY, sunLandPosMaxY);
            float spawnSunPosX = Random.Range(spawnSunPosMinX, spawnSunPosMaxX);
            sun.InitPosForSky(landingPosY, spawnSunPosX, spawnSunPosY);
            sun.SunFallingFromSky();
        }


    }
}