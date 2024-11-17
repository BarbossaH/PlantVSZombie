using System.Collections.Generic;
using System;

namespace Characters.Zombies
{
    public abstract class EnemyStateBase
    {
        //this class serves as the base class of all derived classes, and offers some basic api like enter state, exit state, and execute state.
        
        private readonly Dictionary<EnemyTriggerIdEnum, EnemyStateIdEnum> map;
        
        public  EnemyStateIdEnum StateId { get; protected set; }
        private readonly List<EnemyTriggerBase> triggers;
        protected EnemyStateBase()
        {
            map = new Dictionary<EnemyTriggerIdEnum, EnemyStateIdEnum>();
            triggers = new List<EnemyTriggerBase>();
            InitStateId();
        }

        public abstract void InitStateId();

        //this method is called by state machine
        public void AddMap(EnemyTriggerIdEnum triggerId, EnemyStateIdEnum stateId)
        {
            map.Add(triggerId, stateId);
            // triggers.Add(triggerId);
            CreateTrigger(triggerId);
        }
        private void CreateTrigger(EnemyTriggerIdEnum triggerId)
        {
            Type triggerBase = Type.GetType("Characters.Zombies." + triggerId + "Trigger");

            if (triggerBase == null)
            {
                throw new InvalidOperationException($"Type not found for triggerId: {triggerId}");
            }

            EnemyTriggerBase trigger = (EnemyTriggerBase)Activator.CreateInstance(triggerBase);
            
            triggers.Add(trigger);
        }
        
        public virtual void EnterState(FsmManager fsm){}
        public virtual void ExecuteState(FsmManager fsm){}
        public virtual void ExitState(FsmManager fsm){}

        public void CheckTrigger(FsmManager fsm)
        {
            foreach (var trigger in triggers)
            {
                // Debug.Log(trigger.TriggerId);
                
                //to check the corresponding trigger suitable
                if (trigger.CheckTrigger(fsm))
                {
                    EnemyStateIdEnum stateId = map[trigger.TriggerId];
                    fsm.ChangeActiveState(stateId);
                }
            }
        }
    }
}