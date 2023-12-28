using FSM;
using UnityEngine;

namespace Enemy.Chaser
{
    public class EDieState : EAllStates
    {
        private string e_Die = "Die";
        public EDieState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }
        
        public override void Enter()
        {
            _animator.SetTrigger(e_Die);
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