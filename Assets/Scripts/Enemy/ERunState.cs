using FSM;
using UnityEngine;

namespace Enemy
{
    public class ERunState : EAllStates
    {
      
        private string a_Run = "Run";
        private float _speed = 5.4f;
        private Vector2 _direction;
        public ERunState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }
        
        public override void Enter()
        {
            //Change to run animation
            _animator.SetTrigger(a_Run);
            base.Enter();
        }

        public override void UpdateLogic()
        {
            _direction = ((Vector2)_target.transform.position - (Vector2)_rigidbody2D.position).normalized;
            if (Vector2.Distance(_target.transform.position, _rigidbody2D.position) < _attackRange)
            {
                _eStateMachine.ChangeState(_eStateMachine._eAttackState);
            }
            base.UpdateLogic();
        }

        public override void UpdatePhysics()
        {
            _rigidbody2D.velocity = _direction * _speed;
            base.UpdatePhysics();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}