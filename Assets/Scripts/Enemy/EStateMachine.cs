using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Core.Observer_Pattern;
using FSM;
using Pathfinding;
using Player;
using UnityEngine.Serialization;

namespace Enemy
{
    
    public class EStateMachine : StateMachine
    {

        [HideInInspector] public EIdleState _eIdleState;
        [HideInInspector] public ERunState _eRunState;
        [HideInInspector] public EAttackState _eAttackState;
        [HideInInspector] public EDieState _eDieState;
        [HideInInspector] public EAllStates _eAllStates;

        public AudioSource _audioSource;
        public Animator _animator;
        public Collider2D _collider2D;
        public Rigidbody2D _rigidbody2D;
        [FormerlySerializedAs("_target")] public Transform _player;
        public Seeker _seeker;
        public Transform _self;
        
        
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
                _eAllStates.ChangeToAttack();
                
            }
        }

        public void AttackMotion(Vector3 originalPosition)
        {
            //cnt: Count the number of times the Coroutine below is called. We only it to be called twice, one to leap to the target and one to lerp back
            int cnt = 0;
            StartCoroutine(LerpToTarget(originalPosition, _player.position, 0.5f, cnt));
           
        }
        
        IEnumerator LerpToTarget(Vector3 origin, Vector3 target, float duration, int cntTimes)
        {
            float timeElapsed = 0.0f;
            
            while (timeElapsed < duration)
            {
                transform.position = Vector3.Lerp(origin, target, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null; // Wait for next frame to update position
            }

            //If this is the second time this Coroutine is called, then break it
            if (cntTimes >= 1)
                yield break;
            
            NotifyObservers(IEvent.OnPlayerGetHurt);
            
            cntTimes++;
            StartCoroutine(LerpToTarget(target, origin, 0.5f, cntTimes));
            
            yield return null;
        }
        
    }
}