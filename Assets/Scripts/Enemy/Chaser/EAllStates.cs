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
        
        //the arrow represent the direction to the enemy
        private GameObject _directionArrow;
        private float _maxArrowScale = 3f;


        private static float _timeElapsed = 0f;
        private readonly float _coolDownForNextAttack = 3f;
        
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
            _directionArrow = _eStateMachine._directionArrow;
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
            UpdatePointingArrow();
        }
        
        public void UpdatePointingArrow()
        {
            //The vector starts from the player's position and ends at the enemy's position
            //vector AB = B - A (vector hướng từ A đến B)
            Vector2 directionVector = (Vector2)(_self.position - _player.position);
            float distance = directionVector.magnitude;
            
            if (distance < 5f)
                _directionArrow.SetActive(false);
            else 
                _directionArrow.SetActive(true);
            
            //calculate the angle of the pointing-arrow
            float rad = (_self.position.x - _player.position.x) / distance;
            Debug.Log("Rad = " + rad);
            
            float angle = Mathf.Acos(rad) * Mathf.Rad2Deg;
            
            Debug.Log("Angle: " + angle);
            float yDiff = _self.position.y - _player.position.y;
            if (yDiff > 0)
                _directionArrow.transform.rotation = Quaternion.Euler(0f, 0f, (angle - 90f));
            else 
                _directionArrow.transform.rotation = Quaternion.Euler(0f, 0f, (-angle - 90f));

            //Resize the pointing arrow
            // float scale = Mathf.Clamp(_maxArrowScale / directionVector.magnitude, 1f, 3f);
            // _directionArrow.transform.localScale = new Vector3(scale, scale, scale);
            
            //calculate the position of the pointing-arrow
            directionVector.Normalize();
            _directionArrow.transform.position = ((Vector2)_player.position + directionVector * 2.2f);
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