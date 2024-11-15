namespace Characters.Zombies
{
    public class WalkTrigger:EnemyTriggerBase
    {
        public override void InitTriggerId()
        {
            TriggerId = EnemyTriggerIdEnum.Walk;
        }

        public override bool CheckTrigger(FsmManager fsm)
        {
            return !fsm.zombie.IsAttacking;
        }
    }
}