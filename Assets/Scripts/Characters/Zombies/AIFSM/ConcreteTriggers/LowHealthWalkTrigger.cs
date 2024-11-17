namespace Characters.Zombies
{
    public class LowHealthWalkTrigger:EnemyTriggerBase
    {
        public override void InitTriggerId()
        {
            TriggerId = EnemyTriggerIdEnum.LowHealthWalk;
        }

        public override bool CheckTrigger(FsmManager fsm)
        {
            return fsm.zombie.IsLowHealth && !fsm.zombie.IsAttacking;
        }
    }
}