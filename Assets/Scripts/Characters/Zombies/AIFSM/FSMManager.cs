using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIFSM
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
            states = new List<EnemyStateBase>();
            //create state objects
            WalkState walkState = new WalkState();
            walkState.AddMap(EnemyTriggerIdEnum.NoHealth,EnemyStateIdEnum.Death);
            //set up state map
            states.Add(walkState);
            
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
            //if trigger has been changed, this will check triggerId is suitbale
            currentState.CheckTrigger(this);
            currentState.ExecuteState(this);
        }

        public void ChangeActiveState(EnemyStateIdEnum stateId)
        {
            currentState.ExitState(this);
            //here state could translate to default state, but default state is not in the states
            //if stateId is default, then assign currentState to default state
            if (stateId == EnemyStateIdEnum.Default) stateId = defaultStateId;
            currentState = states.Find(s => s.StateId == stateId);
            currentState.EnterState(this);
        }

    }
}