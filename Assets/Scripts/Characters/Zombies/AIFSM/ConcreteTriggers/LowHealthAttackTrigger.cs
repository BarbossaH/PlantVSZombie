namespace Characters.Zombies
{
    public class LowHealthAttackTrigger:EnemyTriggerBase
    {
        public override void InitTriggerId()
        {
            TriggerId = EnemyTriggerIdEnum.LowHealthAttack;
        }

        public override bool CheckTrigger(FsmManager fsm)
        {
            return fsm.zombie.IsLowHealth && fsm.zombie.IsAttacking;
        }
    }
}