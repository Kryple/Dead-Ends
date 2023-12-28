using System;
using System.Collections;
using System.Collections.Generic;
using Core.Observer_Pattern;
using Enemy;
using Enemy.Chaser;
using UnityEngine;
using FSM;
using Player;
using UnityEngine.Serialization;

namespace Enemy.Patroller
{
    
    public class PatrollerStateMachine : StateMachine
    {
        [HideInInspector] public PatrollerIdleState _patrollerIdleState;
        [HideInInspector] public PatrollerRunState _patrollerRunState;
        [HideInInspector] public PatrollerAttackState _patrollerAttackState;
        // [HideInInspector] public EDieState _eDieState;
        [HideInInspector] public PatrollerAllStates _patrollerAllStates;

        public  AudioSource _audioSource;
        public  Animator _animator;
        public  Collider2D _collider2D;
        public  Rigidbody2D _rigidbody2D;
        public Transform _player;
        public  Transform _self;
        public  AudioClip _biteSFX;

        
        private void Awake()
        {
            Debug.Log("initial");
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
            _collider2D = GetComponent<Collider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _player = GameObject.FindWithTag("Player").transform;
            
            _self = this.transform;

            //The initialization of all states has to be before the "Start()" method which contains the "GetInitialState()" method -> using "Awake()" method
            _patrollerIdleState = new PatrollerIdleState("PatrollerIdleState", this);
            _patrollerRunState = new PatrollerRunState("PatrollerRunState", this);
            _patrollerAttackState = new PatrollerAttackState("PatrollerAttackState", this);
            // _eDieState = new EDieState("EDieState", this);
            _patrollerAllStates = new PatrollerAllStates("PatrollerAllState", this);
            
            //PStateMachine observing PatrollerStateMachine from now            
            AddObserver(_player.GetComponent<PStateMachine>());
        }

        protected override BaseState GetInitialState()
        {
            Debug.Log("Getinitial");
            return _patrollerIdleState;
        }
        
        public void ChangeToRun()
        {
            ChangeState(_patrollerRunState);
        }
        
        //Make sure that the current script had implemented MonoBehaviour
        private void OnTriggerStay2D(Collider2D other)
        {
            // Debug.Log("Triggering");
            if (other.CompareTag("Player"))
            {
                _patrollerAllStates.ChangeToAttack();
            }
        }

        //The motion of enemy when attack the player
        public void AttackMotion(Vector3 originalPosition)
        {
            //cnt: Count the number of times the Coroutine below is called. We only it to be called twice, one to leap to the target and one to lerp back
            StartCoroutine(LerpToTarget(originalPosition, _player, 0.5f));
           
        }
        
        //Enemy lerp to the player
        IEnumerator LerpToTarget(Vector3 origin, Transform target, float duration)
        {
            float timeElapsed = 0.0f;
            
            while (timeElapsed < duration)
            {
                transform.position = Vector3.Lerp(origin, target.position, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null; // Wait for next frame to update position
            }
            
            NotifyObservers(IEvent.OnPlayerGetHurt);
            _audioSource.Play();
            StartCoroutine(LerpToOriginalPosition(_self.position, origin, 0.5f));
        }
        
        //Enemy lerp back to the origin position
        IEnumerator LerpToOriginalPosition(Vector3 origin, Vector3 target, float duration)
        {
            float timeElapsed = 0.0f;
            
            while (timeElapsed < duration)
            {
                transform.position = Vector3.Lerp(origin, target, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null; // Wait for next frame to update position
            }
        }
    }

}