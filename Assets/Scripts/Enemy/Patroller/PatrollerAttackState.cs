using System.Threading.Tasks;
using Core.Observer_Pattern;
using FSM;
using UnityEngine;

namespace Enemy.Patroller
{
    public class PatrollerAttackState : PatrollerAllStates
    {
        private string a_Attack = "Attack";
        public PatrollerAttackState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _audioSource.clip = _biteSFX;
            _audioSource.volume = 0.075f;
            
            _animator.SetTrigger(a_Attack);
            LerpToTarget(_self.position, _player, 0.5f);;
            ChangeToRun();
        }

        public async void ChangeToRun()
        {
            await Task.Delay(1000);
            _patrollerStateMachine.ChangeState(_patrollerStateMachine._patrollerRunState);
        }
        
        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }
        
        //Enemy lerp to the player
        public async void LerpToTarget(Vector3 origin, Transform target, float duration)
        {
            float timeElapsed = 0.0f;
            
            while (timeElapsed < duration)
            {
                _self.position = Vector3.Lerp(origin, target.position, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                await Task.Yield(); // Wait for next frame to update position
            }
            
            _patrollerStateMachine.NotifyObservers(IEvent.OnPlayerGetHurt);
            _audioSource.Play();
            LerpToOriginalPosition(_self.position, origin, 0.5f);
        }
        
        //Enemy lerp back to the origin position
        public async void LerpToOriginalPosition(Vector3 origin, Vector3 target, float duration)
        {
            float timeElapsed = 0.0f;
            
            while (timeElapsed < duration)
            {
                _self.position = Vector3.Lerp(origin, target, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                await Task.Yield(); // Wait for next frame to update position
            }
        }

        public override void Exit()
        {
            base.Exit();
            _audioSource.Stop();
        }
        
        
    }
}