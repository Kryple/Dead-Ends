using FSM;
using UnityEditor;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy.Patroller
{
    public class PatrollerIdleState : PatrollerAllStates
    {
        public PatrollerIdleState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Idle state");
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
            await Task.Delay(230);
            Debug.Log("Idle change to run");
            _patrollerStateMachine.ChangeState(_patrollerStateMachine._patrollerRunState);
        }
    }
}