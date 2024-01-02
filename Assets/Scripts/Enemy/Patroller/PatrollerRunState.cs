using System;
using System.Collections;
using FSM;
using UnityEngine;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Task = System.Threading.Tasks.Task;

namespace Enemy.Patroller
{
    public class PatrollerRunState : PatrollerAllStates
    {
        
        private string a_isRunning = "isRunning";
        private float _changeDirectionCoolDown = 2.3f;
        private float _timeElapsed = 0f;
        
        public PatrollerRunState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }

        public override void Enter()
        {
            _animator.SetTrigger(a_isRunning);
            
            base.Enter();
            
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            
            //If i use Translate, the result is bad
            // _self.Translate(_speed * _direction * Time.deltaTime);
            _rigidbody2D.AddForce(_speed * _direction);
            ChangeDirection(_changeDirectionCoolDown);
        }

        private async void ChangeDirection(float coolDownTime)
        {
            //The async still can run, even when the game has stopped -> use Application.isPlaying
            while (Application.isPlaying)
            {
                if (_timeElapsed > coolDownTime)
                {
                    Vector2 rand = new Vector2(Random.Range(-1f, 1f) * 3, Random.Range(-1f, 1f) * 3);
                    _direction = (_direction + rand).normalized;
                    // Debug.Log("Direct: " + _direction);
                    _timeElapsed = 0f;
                }

                _timeElapsed += Time.deltaTime; 
                await Task.Yield();
                
                
            }
            
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