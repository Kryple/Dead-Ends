using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using FSM;

namespace Enemy
{
    public class EStateMachine : StateMachine
    {

        [HideInInspector] public EIdleState _eIdleState;
        [HideInInspector] public ERunState _eRunState;
        [HideInInspector] public EAttackState _eAttackState;
        [HideInInspector] public EDieState _eDieState;

        public AudioSource _audioSource;
        public Animator _animator;
        public Collider _collider;
        public Rigidbody2D _rigidbody2D;

        public float _attackRange = 0.4f;

        private void Awake()
        {
            
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
                Debug.Log("AudioSource: " + _audioSource);

            _animator = GetComponent<Animator>();
            if (_animator == null)
                Debug.Log("Animtor: " + _animator);

            _collider = GetComponent<Collider>();
            if (_collider == null)
                Debug.Log("Collider: " + _collider);

            _rigidbody2D = GetComponent<Rigidbody2D>();
            if (_rigidbody2D == null)
                Debug.Log("Rigidbody2D: " + _rigidbody2D);


            _eIdleState = new EIdleState("EIdleState", this);
            _eRunState = new ERunState("ERunState", this);
            _eAttackState = new EAttackState("EAttackState", this);
            _eDieState = new EDieState("EDieState", this);

        }


        protected override BaseState GetInitialState()
        {
            return _eIdleState;
        }

        public void ChangeToRun()
        {
            ChangeState(_eRunState);
        }

        public void ChangeToDie()
        {
            ChangeState(_eDieState);
        }




    }
}