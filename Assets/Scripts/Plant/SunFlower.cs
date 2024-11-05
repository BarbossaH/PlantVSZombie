using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : MonoBehaviour
{
    //this script is for creating sun from sunflower, like skysunmanager script
    void Start()
    {
        InvokeRepeating("CreateSun", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateSun()
    {
        Sun sun = GameObject.Instantiate(GameManager.Instance.GameConf.SunPrefab, transform.position,Quaternion.identity, transform).GetComponent<Sun>();
        sun.JumpAnimationFromFlower();
    }
}
