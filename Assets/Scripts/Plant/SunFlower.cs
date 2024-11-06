using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : PlantBase
{
    //this script is for creating sun from sunflower, like skysunmanager script
    void Start()
    {
        InvokeRepeating("CreateSun", 3, 3);
    }

    private void CreateSun()
    {
        CreateSun(SunTypeEnum.Normal);
    }
    public override void CreateSun(SunTypeEnum sunType=SunTypeEnum.Normal)
    {
        Sun sun = GameObject.Instantiate(SunManager.Instance.GetSunPrefabByType(sunType), transform.position, Quaternion.identity, transform).GetComponent<Sun>();
        sun.JumpAnimationFromFlower();
    }


}
