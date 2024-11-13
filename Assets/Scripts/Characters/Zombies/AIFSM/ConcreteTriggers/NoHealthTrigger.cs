using UnityEngine;

namespace Zombies
{
    public class NoHealthTrigger:EnemyTriggerBase
    {
        public override void InitTriggerId()
        {
            TriggerId = EnemyTriggerIdEnum.NoHealth;
        }

        public override bool CheckTrigger(FsmManager fsm)
        {
            return fsm.zombie.CurrentHealth<=0;
        }
    }
}