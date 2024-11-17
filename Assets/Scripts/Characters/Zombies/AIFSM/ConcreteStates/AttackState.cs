using Characters.Attributes;
using UnityEngine;

namespace Characters.Zombies
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
            fsm.zombie.SetWalkSpeed(0.0f);
            fsm.anim.SetBool(ZombieAnimationParams.Attack, true);
        }

        public override void ExitState(FsmManager fsm)
        {
            base.ExitState(fsm);
            // fsm.zombie.SetSpeed(1.0f);
            //I don't need to set attack animation as false, otherwise it will return to walk state
            fsm.anim.SetBool(ZombieAnimationParams.Attack, false);
        }

        public override void ExecuteState(FsmManager fsm)
        {
            base.ExecuteState(fsm);
            fsm.zombie.AttackPlant();
        }
    }
}