
namespace Characters.Zombies
{
    public abstract class EnemyTriggerBase
    {
        //this class serves as the parent class of the all kinds of child classes of trigger conditions.
        
        //store all triggerId
        
        public EnemyTriggerIdEnum TriggerId { get; protected set; }

        public EnemyTriggerBase()
        {
            InitTriggerId();
        }
        //this function is asking the derived classes must initialize the TriggerId, otherwise it will occur issues
        public abstract void InitTriggerId();
        
        //handle trigger,because in compile time, we don't know what need to be handled, so here using an abstract class to declare it and the children will implement the actual details
        public abstract bool CheckTrigger(FsmManager fsm);
    }
}