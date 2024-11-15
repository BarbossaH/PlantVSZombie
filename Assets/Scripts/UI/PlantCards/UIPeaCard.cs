using System;
using UnityEngine;
using Managers;
using Conf;

namespace UI
{
    public class UIPeaCard:UICardBase
    {
        public override float CoolDown { get; protected set; }= 5.0f;


        public override PlantTypeEnum CurrentPlantType { get; protected set; }


        protected override void Start()
        {
            base.Start();
            CurrentPlantType = PlantTypeEnum.Pea;
            sunAmountTMP.text = ((int)CurrentPlantType).ToString();
        }
   }
}