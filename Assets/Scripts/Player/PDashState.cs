using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FSM;
using System.Threading.Tasks;

namespace Player
{
    public class PDashState : PPatrolState
    {
        
        public PDashState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _speed = _dashSpeed;
            
            _animator.SetBool(a_isMoving, false);
            _animator.SetTrigger(a_dash);
            ChangeToIdle();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            _rigidbody2D.AddForce(_direction * _speed);
        }

        public override void Exit()
        {
            base.Exit();
            _animator.ResetTrigger(a_dash);
            _animator.SetBool(a_isMoving, true);
        }


        public async void ChangeToIdle()
        {
            await Task.Delay(100);
            _pStateMachine.ChangeState(_pStateMachine._pIdleState);

        }
        
    }
}