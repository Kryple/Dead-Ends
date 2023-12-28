using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Core.Observer_Pattern;
using FSM;
using Pathfinding;
using Player;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using StateMachine = FSM.StateMachine;

namespace Enemy.Chaser
{
    
    public class EStateMachine : StateMachine
    {
        [HideInInspector] public EIdleState _eIdleState;
        [HideInInspector] public ERunState _eRunState;
        [HideInInspector] public EAttackState _eAttackState;
        [HideInInspector] public EDieState _eDieState;
        [HideInInspector] public EAllStates _eAllStates;

        public  AudioSource _audioSource;
        public  Animator _animator;
        public  Collider2D _collider2D;
        public  Rigidbody2D _rigidbody2D;
        public Transform _player;
        public  Seeker _seeker;
        public  Transform _self;

        public  AudioClip _biteSFX;
        
        
        private void Awake()
        {
            
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
            _collider2D = GetComponent<Collider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _player = GameObject.FindWithTag("Player").transform;
            _seeker = GetComponent<Seeker>();
            _self = this.transform;


            _eIdleState = new EIdleState("EIdleState", this);
            _eRunState = new ERunState("ERunState", this);
            _eAttackState = new EAttackState("EAttackState", this);
            _eDieState = new EDieState("EDieState", this);
            _eAllStates = new EAllStates("EAllState", this);
            
            InvokeRepeating("UpdatePathInRunState", 0f, 0.55f);
            
            //PStateMachine observing EStateMachine from now            
            AddObserver(_player.GetComponent<PStateMachine>());
            AddObserver(_eAllStates);
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
        private void OnTriggerStay2D(Collider2D other)
        {
            // Debug.Log("Triggering");
            if (other.CompareTag("Player"))
            {
                NotifyObservers(IEvent.OnPlayerinRange);
            }
        }

        
        
        
        
        
        
    }
}