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
        
        protected static float _runSpeed = 2.75f; //player's speed when running
        protected static float _dashSpeed = _runSpeed * 2.8f; //player's speed when dashing
        protected static float _speed = _runSpeed; //current player's speed
        protected static float _dashTime = 0.18f; //Amount of time that the dashing action needs
        
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
            
            if (_pStateMachine.CurrentState() != _pStateMachine._pRunState)
            {
                if (Mathf.Abs(_verticalInput) > Mathf.Epsilon ||
                    Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
                {
                    _pStateMachine.ChangeState(_pStateMachine._pRunState);
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

            
            if (Input.GetKeyDown(KeyCode.X))
            {
                _pStateMachine.ChangeState(_pStateMachine._pAttackState);
            }
            
        }
    }
}