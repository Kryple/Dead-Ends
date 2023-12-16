﻿using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace Player
{
    public class PPatrolState : PAllStates
    {
        protected AudioSource _audioSource;
        
        protected static string a_floSpeed = "floSpeed";
        
        protected static string a_dash = "triDash";
        protected static string a_isMoving = "booIsMoving";

        
        protected static float _runSpeed = 5.2f; //player's speed when running
        protected static float _dashSpeed = _runSpeed * 7.5f; //player's speed when dashing
        protected static float _speed = _runSpeed; //current player's speed

        

        public PPatrolState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            _audioSource = _pStateMachine._audioSource;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            
            
            
            _animator.SetFloat(a_floPosX, _horizontalInput);
            _animator.SetFloat(a_floPosY, _verticalInput);

            if (_pStateMachine.CurrentState() == _pStateMachine._pDashState)
            {
                return;
            }
            
            if (_pStateMachine.CurrentState() != _pStateMachine._PRunState)
            {
                if (Mathf.Abs(_verticalInput) > Mathf.Epsilon ||
                    Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
                {
                    _pStateMachine.ChangeState(_pStateMachine._PRunState);
                }
            }
            else if (_pStateMachine.CurrentState() != _pStateMachine._pIdleState)
            {
                if (Mathf.Abs(_verticalInput) <= Mathf.Epsilon &&
                    Mathf.Abs(_horizontalInput) <= Mathf.Epsilon)
                {
                    _pStateMachine.ChangeState(_pStateMachine._pIdleState);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _pStateMachine.ChangeState(_pStateMachine._pDashState);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                _pStateMachine.ChangeState(_pStateMachine._pAttackState);
            }
            
        }
    }
}