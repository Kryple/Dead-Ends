using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FSM;
using Pathfinding;
using UnityEngine;

namespace Enemy
{
    public class EAllStates : BaseState
    {
        protected EStateMachine _eStateMachine;
        protected AudioSource _audioSource;
        protected Animator _animator;
        protected Rigidbody2D _rigidbody2D;
        protected Transform _target;
        protected Seeker _seeker;
        protected Transform _self;

        private static float _timeElapsed = 0f;
        private static float _coolDownForNextAttack = 3f;
        
        public EAllStates(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            _eStateMachine = (EStateMachine) stateMachine;
            _audioSource = _eStateMachine._audioSource;
            _animator = _eStateMachine._animator;
            _rigidbody2D = _eStateMachine._rigidbody2D;
            _target = _eStateMachine._target;
            _seeker = _eStateMachine._seeker;
            _self = _eStateMachine._self;
        }
        
        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            _timeElapsed += Time.deltaTime;
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

        
        
    }
}