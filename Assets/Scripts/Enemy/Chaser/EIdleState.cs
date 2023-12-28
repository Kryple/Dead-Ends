using System.Threading.Tasks;
using FSM;
using UnityEngine;

namespace Enemy.Chaser
{
    public class EIdleState : EAllStates
    {
      
        
        public EIdleState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
           
           
            
        }
        
        
        
        public override void Enter()
        {
            base.Enter();
            ChangeToRun();
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

        async void ChangeToRun()
        {
            // wait for the enemy to be spawned, then change to its runState
            await Task.Delay(230);
            _eStateMachine.ChangeState(_eStateMachine._eRunState);
        }
    }
}