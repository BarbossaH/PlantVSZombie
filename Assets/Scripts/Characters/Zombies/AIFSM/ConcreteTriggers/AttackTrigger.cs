using UnityEngine;

namespace Characters.Zombies
{
    public class AttackTrigger:EnemyTriggerBase
    {
        public override void InitTriggerId()
        {
            TriggerId = EnemyTriggerIdEnum.Attack;
        }

        public override bool CheckTrigger(FsmManager fsm)
        {
            return fsm.zombie.IsAttacking;
        }
    }
}