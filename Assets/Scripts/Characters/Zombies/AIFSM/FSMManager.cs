using System.Collections.Generic;
using UnityEngine;

namespace Characters.Zombies
{
    public class FsmManager: MonoBehaviour
    {
        //this class is for initializing the default state and changing states 
        private EnemyStateBase currentState;
        private List<EnemyStateBase> states;
        [HideInInspector]public ZombieBase zombie; // attack, health, It doesn't exist yet
        [HideInInspector] public Animator anim;
        [SerializeField] EnemyStateIdEnum defaultStateId;
        private void Start()
        {
            InitComponent();
            ConfigFsm();
            InitDefaultState();
        }

        private void InitComponent()
        {
            zombie = GetComponent<ZombieBase>();
            anim = GetComponentInChildren<Animator>();
        }
        private void ConfigFsm()
        {
            //if there are many state that need to configure, we can use reflection to implement this
            states = new List<EnemyStateBase>();
            //create state objects
            WalkState walkState = new WalkState();
            walkState.AddMap(EnemyTriggerIdEnum.NoHealth,EnemyStateIdEnum.Death);
            walkState.AddMap(EnemyTriggerIdEnum.Attack,EnemyStateIdEnum.Attack);

            //set up state map
            states.Add(walkState);
            AttackState attackState = new AttackState();
            attackState.AddMap(EnemyTriggerIdEnum.Walk,EnemyStateIdEnum.Walk);
            states.Add(attackState);
            
            DeathState deathState = new DeathState();
            states.Add(deathState);
        }
        private void InitDefaultState()
        { 
           EnemyStateBase defaultState= states.Find(s=>s.StateId==defaultStateId);
           currentState = defaultState;
           currentState.EnterState(this);
        }
 
        //check the current state and execute current state
        public void Update()
        {
            //if trigger has been changed, this will check triggerId is suitable
            //keep checking the state of the game object, if the state has been changed, like health, attack, spotting player
            currentState.CheckTrigger(this);
            //after checking and changing the state, if the new state has something to do, then call the next method
            currentState.ExecuteState(this);
        }

        public void ChangeActiveState(EnemyStateIdEnum stateId)
        {
            currentState.ExitState(this);
            //here state could translate to default state, but default state is not in the states
            //if stateId is default, then assign currentState to default state
            if (stateId == EnemyStateIdEnum.Default) {stateId = defaultStateId;}
            currentState = states.Find(s => s.StateId == stateId);
            currentState.EnterState(this);
        }

    }
}