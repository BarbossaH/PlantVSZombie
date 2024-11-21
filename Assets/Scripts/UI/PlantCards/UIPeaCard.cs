using System;
using UnityEngine;
using Managers;
using Conf;

namespace UI
{
    public class UIPeaCard:UICardBase
    {
        public override float CoolDown { get; protected set; }= 5.0f;


        public override PoolTypeEnum CurrentPlantType { get; protected set; }


        protected override void Start()
        {
            base.Start();
            CurrentPlantType = PoolTypeEnum.PeaShooter;
            sunAmountTMP.text = ((int)CurrentPlantType).ToString();
        }
   }
}