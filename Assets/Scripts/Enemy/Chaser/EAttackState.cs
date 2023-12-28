using System.Threading.Tasks;
using FSM;
using UnityEngine;
using Core.Observer_Pattern;

namespace Enemy.Chaser
{
    public class EAttackState : EAllStates
    {
        private string a_Attack = "Attack";
        
        
        
        public EAttackState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }
        
        public override void Enter()
        {
            base.Enter();

            _audioSource.clip = _biteSFX;
            _audioSource.volume = 0.075f;
            
            _animator.SetTrigger(a_Attack);
            AttackMotion(_self.position);
            ChangeToRun();

        }
        
        public async void ChangeToRun()
        {
            await Task.Delay(1000);
            _eStateMachine.ChangeState(_eStateMachine._eRunState);
        }


        public override void UpdateLogic()
        {
            base.UpdateLogic();
            
            
            
        }
        
        
        

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }
        
        //The motion of enemy when attack the player
        public void AttackMotion(Vector3 originalPosition)
        {
            //cnt: Count the number of times the Coroutine below is called. We only it to be called twice, one to leap to the target and one to lerp back
            LerpToTarget(originalPosition, _player, 0.5f);

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
            
            _eStateMachine.NotifyObservers(IEvent.OnPlayerGetHurt);
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