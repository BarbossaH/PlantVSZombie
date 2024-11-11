namespace AIFSM
{
    public class SpotTargetTrigger:EnemyTriggerBase
    {
        public override void InitTriggerId()
        {
            TriggerId = EnemyTriggerIdEnum.SpotTarget;
        }

        public override bool HandleTrigger(FsmManager fsm)
        {
            return fsm.zombie.IsAttacking;
        }
    }
}