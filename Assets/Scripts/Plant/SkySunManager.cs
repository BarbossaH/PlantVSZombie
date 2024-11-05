using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySunManager : MonoBehaviour
{
    //this class is only for generating sun from sky, and set the relative arguements, like SunFlower script
    private float spawnSunPosY=6f;

    private float spawnSunPosMinX = -5.5f;
    private float spawnSunPosMaxX =5f;

    private float sunLandPosMinY = -3.5f;
    private float sunLandPosMaxY = 3f;

    private void Start()
    {
        InvokeRepeating("CreateSun", 2, 3);

    }
    private void Update()
    {
    }
    void CreateSun()
    {
      Sun sun=  GameObject.Instantiate(GameManager.Instance.GameConf.SunPrefab, Vector3.zero, Quaternion.identity,transform).GetComponent<Sun>();
        float landingPosY = Random.Range(sunLandPosMinY,sunLandPosMaxY);
        float spawnSunPosX = Random.Range(spawnSunPosMinX,spawnSunPosMaxX);
        sun.InitPosForSky(landingPosY, spawnSunPosX, spawnSunPosY);
        sun.SunFallingFromSky();
    }


}
