using Characters.Attributes;

namespace AIFSM
{
    public class WalkState:EnemyStateBase
    {
        public override void InitStateId()
        {
            StateId = EnemyStateIdEnum.Walk;
        }

        public override void EnterState(FsmManager fsm)
        {
            base.EnterState(fsm);
            //play animation
            fsm.anim.SetBool(ZombieAnimationParams.Walk, true);
        }

        public override void ExitState(FsmManager fsm)
        {
            base.ExitState(fsm);
            fsm.anim.SetBool(ZombieAnimationParams.Walk, false);
        }
    }
}