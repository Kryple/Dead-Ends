using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Observer_Pattern;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FSM
{
    public class StateMachine : Subject
    {
        BaseState _currentState;
        protected void Start()
        {
            _currentState = GetInitialState();
            if (_currentState != null)
                _currentState.Enter();
        }
        



        void Update()
        {
            if (_currentState != null)
                _currentState.UpdateLogic();
        }

        void LateUpdate()
        {
            {
                if (_currentState != null)
                    _currentState.UpdatePhysics();
            }
        }

        protected virtual BaseState GetInitialState()
        {
            return null;
        }

        public void ChangeState(BaseState newState)
        {
            _currentState.Exit();

            _currentState = newState;
            _currentState.Enter();
        }

        public BaseState CurrentState()
        {
            return _currentState;
        }
        
    }
}