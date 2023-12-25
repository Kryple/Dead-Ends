using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace Player
{
    public class PPatrolState : PAllStates
    {
        protected static string a_floSpeed = "floSpeed";
        
        protected static string a_dash = "triDash";
        protected static string a_isMoving = "booIsMoving";
        
        protected static float _runSpeed = 3.7f; //player's speed when running
        protected static float _dashSpeed = _runSpeed * 4f; //player's speed when dashing
        protected static float _speed = _runSpeed; //current player's speed
        protected static float _dashTime = 0.12f; //Amount of time that the dashing action needs
        
        public PPatrolState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
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