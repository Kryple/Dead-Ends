using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Core.Observer_Pattern;
using FSM;
using UnityEngine.Serialization;

namespace Player
{
    //Extend IObserver to become an Observer
    public class PStateMachine : StateMachine, IObserver
    {

        [HideInInspector] public PIdleState _pIdleState;
        [HideInInspector] public PRunState _PRunState;
        [HideInInspector] public PDashState _pDashState;
        [HideInInspector] public PAttackState _pAttackState;
        [HideInInspector] public PAllStates _pAllStates;

        public AudioSource _audioSource;
        [FormerlySerializedAs("Something")] public Collider2D _collider2D;
        public Rigidbody2D _rigidbody2D;
        public Animator _animator;
        public SpriteRenderer _spriteRenderer;
        public AudioClip _walkingSound;
        public Transform _transform;
        

        private void Awake()
        {
            TryGetComponent<AudioSource>(out _audioSource);
            TryGetComponent<Collider2D>(out _collider2D);
            TryGetComponent<Rigidbody2D>(out _rigidbody2D);
            TryGetComponent<Animator>(out _animator);
            TryGetComponent<SpriteRenderer>(out _spriteRenderer);
            TryGetComponent<Transform>(out _transform);
            
            // _audioSource = GetComponent<AudioSource>();
            // _collider2D = GetComponent<Collider2D>();
            // _rigidbody2D = GetComponent<Rigidbody2D>();
            // _animator = GetComponent<Animator>();
            // _spriteRenderer = GetComponent<SpriteRenderer>();


            CheckComponentNull(_audioSource);
            CheckComponentNull(_collider2D);
            CheckComponentNull(_rigidbody2D);
            CheckComponentNull(_animator);
            CheckComponentNull(_spriteRenderer);


            _pIdleState = new PIdleState("PIdleState", this);
            _PRunState = new PRunState("PRunState", this);
            _pDashState = new PDashState("PDashState", this);
            _pAttackState = new PAttackState("PAttackState", this);
            _pAllStates = new PAllStates("PAllStates", this);

        }
        
        public void CheckComponentNull(Component component)
        {
            if (component == null)
            {
                Debug.Log(component.name + component);
            }
        }


        protected override BaseState GetInitialState()
        {
            return _pIdleState;
        }

        public void ChangeToIdle()
        {
            ChangeState(_pIdleState);
        }

        public void OnNotify(IEvent @event)
        {
            if (@event == IEvent.OnPlayerGetHurt)
            {
                _pAllStates.PlayerGetHurt(1);
            }
        }

        



    }
}
