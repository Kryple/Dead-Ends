using FSM;
using UnityEngine;

namespace Enemy
{
    public class EIdleState : EAllStates
    {
      
        
        public EIdleState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
           
           
            
        }
        
        
        
        public override void Enter()
        {
            _eStateMachine.ChangeState(_eStateMachine._eRunState);
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