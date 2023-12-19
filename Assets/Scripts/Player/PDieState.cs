using System.Collections.Generic;
using Core.Observer_Pattern;
using FSM;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Player
{
    public class PDieState : PAllStates
    {
        public PDieState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _pStateMachine.NotifyObservers(IEvent.OnPlayerDie);
            _pStateMachine._pAllStates.ResetStat();
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