using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameConf", menuName ="GameConf")]
public class GameConf : ScriptableObject
{
    [Tooltip("Sun")]
    public GameObject SunPrefab;
}
