namespace AIFSM
{
    public class SpotTargetTrigger:EnemyTriggerBase
    {
        public override void InitTriggerId()
        {
            TriggerId = EnemyTriggerIdEnum.SpotTarget;
        }

        public override bool CheckTrigger(FsmManager fsm)
        {
            return fsm.zombie.IsAttacking;
        }
    }
}