using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using FSM;

namespace Player
{
    public class PAttackState : PAllStates
    {
        protected string a_Attack = "booAttack";
        protected string a_AttackCombo = "floAttackCombo";


        public PAttackState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
            
        }
    
        // Enter() is called every time the X key is pressed
        public override void Enter()
        {
            base.Enter();
            _animator.SetBool(a_Attack, true);
            
            
            if (_timeCountAttackCombo < _timeLimit)
            {
                _countAttackCombo += 1f;
                Debug.Log("Count Attack: " + _countAttackCombo);
                if (_countAttackCombo >= 1f) _countAttackCombo = _countAttackComboDefaultValue;

                _timeCountAttackCombo = 0f; //Reset the timer
            }
            else
            {
                _countAttackCombo = _countAttackComboDefaultValue;
                _timeCountAttackCombo = 0f;

            }
     
            _animator.SetFloat(a_AttackCombo, _countAttackCombo);
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            _animator.SetFloat(a_floPosX, _prevDir.x);
            _animator.SetFloat(a_floPosY, _prevDir.y);
            
            if (_timeCountAttackCombo >= _timeLimit)
            {
                Debug.Log("Reach time limit");
                _countAttackCombo = _countAttackComboDefaultValue;
                _timeCountAttackCombo = 0f;
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

        public override void Exit()
        {
            base.Exit();
            _animator.SetBool(a_Attack, false);
        }
    }
}