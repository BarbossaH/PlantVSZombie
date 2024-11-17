using Characters.Attributes;

namespace Characters.Zombies
{
    public class AttackWithoutHeadState:EnemyStateBase
    {
        public override void InitStateId()
        {
            StateId = EnemyStateIdEnum.AttackWithoutHead;
        }

        public override void EnterState(FsmManager fsm)
        {
            base.EnterState(fsm);
            fsm.zombie.SetWalkSpeed(0.0f);
            // Debug.Log("attack");
            fsm.anim.SetTrigger(ZombieAnimationParams.LowHealth);
            fsm.anim.SetBool(ZombieAnimationParams.Attack,true);
        }

        public override void ExitState(FsmManager fsm)
        {
            base.ExitState(fsm);
        }
    }
}