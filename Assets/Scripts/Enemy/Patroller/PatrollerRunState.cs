using FSM;
using UnityEngine;

namespace Enemy.Patroller
{
    public class PatrollerRunState : PatrollerAllStates
    {
        
        private string a_isRunning = "isRunning";
        public PatrollerRunState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }

        public override void Enter()
        {
            Debug.Log("HOHOHOHJO");
            _animator.SetTrigger(a_isRunning);
            
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