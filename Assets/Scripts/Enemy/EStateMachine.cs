using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using FSM;
using Pathfinding;

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
        public Collider2D _collider2D;
        public Rigidbody2D _rigidbody2D;
        public Transform _target;
        public Seeker _seeker;
        
        private void Awake()
        {
            
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
            _collider2D = GetComponent<Collider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _target = GameObject.FindWithTag("Player").transform;
            _seeker = GetComponent<Seeker>();


            _eIdleState = new EIdleState("EIdleState", this);
            _eRunState = new ERunState("ERunState", this);
            _eAttackState = new EAttackState("EAttackState", this);
            _eDieState = new EDieState("EDieState", this);
            
            InvokeRepeating("UpdatePathInRunState", 0f, 0.55f);

        }
        
        


        protected override BaseState GetInitialState()
        {
            return _eIdleState;
        }

        public void UpdatePathInRunState()
        {
            _eRunState.UpdatePath();
        }

        public void ChangeToRun()
        {
            ChangeState(_eRunState);
        }

        public void ChangeToDie()
        {
            ChangeState(_eDieState);
        }

        //Make sure that the current script had implemented MonoBehaviour
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Triggering");
            if (other.CompareTag("Player"))
            {
                ChangeState(_eAttackState);
            }
        }
    }
}