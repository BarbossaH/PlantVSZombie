namespace AIFSM
{
    public class AttackState:EnemyStateBase
    {
        public override void InitStateId()
        {
            StateId = EnemyStateIdEnum.Attack;
        }
        
        public override void EnterState(FsmManager fsm)
        {
            base.EnterState(fsm);
            fsm.zombie.SetSpeed(1.0f);
        }
    }
}