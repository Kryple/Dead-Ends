using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FSM;
using UnityEngine;

namespace Enemy
{
    public class EAllStates : BaseState
    {
        protected EStateMachine _eStateMachine;
        protected AudioSource _audioSource;
        protected Animator _animator;
        protected Rigidbody2D _rigidbody2D;
        protected GameObject _target;
        

        protected float _attackRange;
        public EAllStates(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            _eStateMachine = (EStateMachine) stateMachine;

            _audioSource = _eStateMachine._audioSource;
            _animator = _eStateMachine._animator;
            
            _rigidbody2D = _eStateMachine._rigidbody2D;

            _target = _eStateMachine._target;
            _attackRange = _eStateMachine._attackRange;

        }
        
        public override void Enter()
        {
            base.Enter();
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