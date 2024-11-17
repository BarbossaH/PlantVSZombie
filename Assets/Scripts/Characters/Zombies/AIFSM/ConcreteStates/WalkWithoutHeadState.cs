using Characters.Attributes;

namespace Characters.Zombies
{
    public class WalkWithoutHeadState:EnemyStateBase
    {
        public override void InitStateId()
        {
            StateId = EnemyStateIdEnum.WalkWithoutHead;
        }

        public override void EnterState(FsmManager fsm)
        {
            base.EnterState(fsm);
            fsm.zombie.SetWalkSpeed(0.3f);

            fsm.anim.SetTrigger(ZombieAnimationParams.LowHealth);
        }

        public override void ExitState(FsmManager fsm)
        {
            base.ExitState(fsm);
        }
    }
}