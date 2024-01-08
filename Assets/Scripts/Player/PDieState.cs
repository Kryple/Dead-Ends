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
            int currentScore = (int)_countTimeAlive * 10;
            if (currentScore > PlayerPrefs.GetInt("HighScore"));
                PlayerPrefs.SetInt("HighScore", currentScore);
            
            _pStateMachine.NotifyObservers(IEvent.OnPlayerDie);
            
            ResetStat();
            Time.timeScale = 0;
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