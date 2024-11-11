using UnityEngine;

namespace AIFSM
{
    public class NoHealthTrigger:EnemyTriggerBase
    {
        public override void InitTriggerId()
        {
            TriggerId = EnemyTriggerIdEnum.NoHealth;
        }

        public override bool HandleTrigger(FsmManager fsm)
        {
            return fsm.zombie.CurrentHealth<=0;
        }
    }
}