using Core.Observer_Pattern;
using Enemy.Chaser;
using FSM;
using UnityEngine;

namespace Enemy.Patroller
{
    public class PatrollerAllStates : BaseState, IObserver
    {
        protected static PatrollerStateMachine _patrollerStateMachine;
        protected static AudioSource _audioSource;
        protected static Animator _animator;
        protected static Rigidbody2D _rigidbody2D;
        protected static Transform _player;
        
        protected static Transform _self;
        protected static AudioClip _biteSFX;
        
        //the arrow represent the direction to the enemy
        private GameObject _directionArrow;

        private float _maxArrowScale = 3f;

        // Enemy's moving speed
        protected static float _speed = 3.4f;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }
        
        //Patrolling-enemies's direction 
        protected Vector2 _direction = new Vector2(1f, 1f);

        private float _timeElapsed = 0f;
        private readonly float  _coolDownForNextAttack = 3f;
        
        

        public PatrollerAllStates(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            _patrollerStateMachine = (PatrollerStateMachine) stateMachine;
            _audioSource = _patrollerStateMachine._audioSource;
            _animator = _patrollerStateMachine._animator;
            _rigidbody2D = _patrollerStateMachine._rigidbody2D;
            _player = _patrollerStateMachine._player;
            
            _self = _patrollerStateMachine._self;
            _biteSFX = _patrollerStateMachine._biteSFX;
            _directionArrow = _patrollerStateMachine._directionArrow;
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
            if (_rigidbody2D.velocity.x >= 0.001f)
                _self.localScale = new Vector3(-1f, 1f, 1f);
            else if (_rigidbody2D.velocity.x <= -0.001f)
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
            float angle = Mathf.Acos(rad) * Mathf.Rad2Deg;
            
            
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
                _patrollerStateMachine.ChangeState(_patrollerStateMachine._patrollerAttackState);
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