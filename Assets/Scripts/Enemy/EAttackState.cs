using FSM;
using UnityEngine;

namespace Enemy
{
    public class EAttackState : EAllStates
    {
        private string a_Attack = "Attack";
        public EAttackState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }
        
        public override void Enter()
        {
            _animator.SetTrigger(a_Attack);
            _rigidbody2D.velocity = new Vector2(0f, 0f);
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}