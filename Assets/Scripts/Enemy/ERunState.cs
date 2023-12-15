using FSM;
using UnityEngine;

namespace Enemy
{
    public class ERunState : EAllStates
    {
      
        private string a_isRunning = "isRunning";
        private float _speed = 5.4f;
        private Vector2 _direction;
        public ERunState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }
        
        public override void Enter()
        {
            //Change to run animation
            _animator.SetTrigger(a_isRunning);
            base.Enter();
        }

        public override void UpdateLogic()
        {
         
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