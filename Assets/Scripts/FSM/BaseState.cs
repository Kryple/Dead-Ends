using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class BaseState
    {
        public string name; //Dùng để debug: cho biêt tên của State hiện tại 
        protected StateMachine stateMachine;
        public BaseState(string name, StateMachine stateMachine)
        {
            this.name = name;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            Debug.Log("Entering " + name);  
        }

        public virtual void UpdateLogic()
        {
            // Debug.Log(name + " UpdateLogic" );
        }

        public virtual void UpdatePhysics()
        {
            // Debug.Log(name + " UpdatePhysics");
        }

        public virtual void Exit()
        {
            // Debug.Log(name + " Exit");
        }

    }
}