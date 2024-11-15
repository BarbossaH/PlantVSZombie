using Characters.Attributes;

namespace Characters.Zombies
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
            fsm.zombie.SetWalkSpeed(0.3f);
            //because walk is default state, so when other states exit their states, it should return to walk state
            //which means we need to care about the switching of this animation 
            // fsm.anim.SetBool(ZombieAnimationParams.Walk, true);
        }

        public override void ExitState(FsmManager fsm)
        {
            base.ExitState(fsm);
            // fsm.anim.SetBool(ZombieAnimationParams.Walk, false);
        }
    }
}