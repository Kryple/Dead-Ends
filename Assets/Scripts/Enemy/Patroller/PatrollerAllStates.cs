using Core.Observer_Pattern;
using Enemy.Chaser;
using FSM;
using UnityEngine;

namespace Enemy.Patroller
{
    public class PatrollerAllStates : BaseState, IObserver
    {
        protected static PatrollerStateMachine _patrollerStateMachine;
        protected static AudioSource _audioSource;
        protected static Animator _animator;
        protected static Rigidbody2D _rigidbody2D;
        protected static Transform _player;
        
        protected static Transform _self;
        protected static AudioClip _biteSFX;

        private static float _timeElapsed = 0f;
        private static float _coolDownForNextAttack = 3f;
        
        

        public PatrollerAllStates(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            _patrollerStateMachine = (PatrollerStateMachine) stateMachine;
            _audioSource = _patrollerStateMachine._audioSource;
            _animator = _patrollerStateMachine._animator;
            _rigidbody2D = _patrollerStateMachine._rigidbody2D;
            _player = _patrollerStateMachine._player;
            
            _self = _patrollerStateMachine._self;
            _biteSFX = _patrollerStateMachine._biteSFX;
        }


        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            _timeElapsed += Time.deltaTime;
            
            //Change the enemy's face direction depends on the force apply to it 
            if (_rigidbody2D.totalForce.x >= 0.001f)
                _self.localScale = new Vector3(-1f, 1f, 1f);
            else if (_rigidbody2D.totalForce.x <= -0.001f)
                _self.localScale = new Vector3(1f, 1f, 1f);
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        public void ChangeToAttack()
        {
            if (_timeElapsed > _coolDownForNextAttack)
            {
                _patrollerStateMachine.ChangeState(_patrollerStateMachine._patrollerAttackState);
                _timeElapsed = 0f;
            }
            
        }

        public void OnNotify(IEvent @event)
        {
            switch (@event)
            {
                case IEvent.OnPlayerinRange:
                    ChangeToAttack();
                    break;
            }
        }
    }
}