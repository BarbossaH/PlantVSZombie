using Characters.Attributes;

namespace Zombies
{
    public class DeathState:EnemyStateBase
    {
        public override void InitStateId()
        {
            StateId = EnemyStateIdEnum.Death;
        }

        public override void EnterState(FsmManager fsm)
        {
            //when zombie dies, the fsm should stop working
            base.EnterState(fsm);
            //这里有问题，因为如果禁用状态机，就不能切换死亡的动画，因为动画除了死亡之外都可以在状态机控制，但是这里不行？
            //所以死亡动画需要在别的地方做
            fsm.enabled = false;
            // fsm.anim.SetBool(ZombieAnimationParams.Walk, true);
        }
    }
}