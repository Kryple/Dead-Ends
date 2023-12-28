using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Observer_Pattern;
using FSM;
using Pathfinding;
using UnityEngine;

namespace Enemy.Chaser
{
    public class EAllStates : BaseState, IObserver
    {
        protected static EStateMachine _eStateMachine;
        protected static AudioSource _audioSource;
        protected static Animator _animator;
        protected static Rigidbody2D _rigidbody2D;
        protected static Transform _player;
        protected static Seeker _seeker;
        protected static Transform _self;
        protected static AudioClip _biteSFX;

        private static float _timeElapsed = 0f;
        private static float _coolDownForNextAttack = 3f;
        
        public EAllStates(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            _eStateMachine = (EStateMachine) stateMachine;
            _audioSource = _eStateMachine._audioSource;
            _animator = _eStateMachine._animator;
            _rigidbody2D = _eStateMachine._rigidbody2D;
            _player = _eStateMachine._player;
            _seeker = _eStateMachine._seeker;
            _self = _eStateMachine._self;
            _biteSFX = _eStateMachine._biteSFX;
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
                _eStateMachine.ChangeState(_eStateMachine._eAttackState);
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