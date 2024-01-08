using System.Collections.Generic;
using Core.Observer_Pattern;
using FSM;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using StateMachine = FSM.StateMachine;

namespace Player
{
    public class PAllStates : BaseState
    {
        protected static PStateMachine _pStateMachine;
        protected static AudioSource _audioSource;
        protected static Collider2D _collider2D;
        protected static Rigidbody2D _rigidbody2D;
        protected static Animator _animator;
        protected static SpriteRenderer _spriteRenderer;
        protected static AudioClip _walkingSound;
        protected static AudioClip _hurtingSound;
        protected static Transform _transform;
        protected static ParticleSystem _dustEffect;

        protected static float _horizontalInput; 
        protected static float _verticalInput; 
        protected static Vector2 _direction; //the direction character is facing 

        protected static Vector2 _prevDir = new Vector2(); //The previous player's direction. This has to be static to prevent different State using different _preDir
        protected static string a_floPosX = "floPosX";
        protected static string a_floPosY = "floPosY";
        
        protected static float _timeCountAttackCombo = 0; //the time amount has passed since the last attack
        protected static float _countAttackComboDefaultValue = -1f; //Initial value for the _countAttackCombo
        protected static float _countAttackCombo = _countAttackComboDefaultValue; // the current value represent for the attack animation's threshold
        protected static float _timeLimit = 3.5f; //max interval time between 2 attack in a combo

        
        protected static int _dirListCap = 12; //The capacity of _dirlist
        
        //Contain player's direction coordinates in 12 latest frames
        protected static List<KeyValuePair<float, float>> _dirList = new List<KeyValuePair<float, float>>() { };  
        
        protected static int _dirListId;//The index variable used for _posList
        protected static int _lives = 3;
        protected static float _countTimeAlive = 0f;
        
        
        

        public PAllStates(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }
            
        public override void Enter()
        {
            base.Enter();
            _pStateMachine = (PStateMachine) stateMachine;

            _audioSource = _pStateMachine._audioSource;
            _collider2D = _pStateMachine._collider2D;
            _rigidbody2D = _pStateMachine._rigidbody2D;
            _animator = _pStateMachine._animator;
            _spriteRenderer = _pStateMachine._spriteRenderer;
            _walkingSound = _pStateMachine._walkingSound;
            _hurtingSound = _pStateMachine._hurtingSound;
            _transform = _pStateMachine._transform;
            _dustEffect = _pStateMachine._dustEffect;
            
            //Initial value for the directionList
            for (int i = 0; i < _dirListCap; i++)
                _dirList.Add(new KeyValuePair<float, float>(0f, 0f));
            
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pStateMachine.NotifyObservers(IEvent.OnGamePause);
            }
            
            _countTimeAlive += Time.deltaTime;
            
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            
            //Add player's direction coordinate of this frame to the _posList
            if (_dirListId >= _dirListCap)
            {
                _dirListId = 0;
            }
            else
            {
                
                Debug.Assert(_dirListId < _dirListCap, "Out of range");
                _dirList[_dirListId] = new KeyValuePair<float, float>(_horizontalInput, _verticalInput);
                _dirListId++;    
            }
            
            _direction.x = _horizontalInput;
            _direction.y = _verticalInput;

            _timeCountAttackCombo += Time.deltaTime;
        }


        public void PlayerGetHurt(int damage)
        {
            _lives -= damage;
            if (_lives <= 0)
                _pStateMachine.ChangeState(_pStateMachine._pDieState);
            
            _pStateMachine.NotifyObservers(IEvent.OnPlayerGetHurt);

            _audioSource.clip = _hurtingSound;
            _audioSource.loop = false;
            _audioSource.pitch = 1f;
            _audioSource.volume = .9f;
            _audioSource.Play();
            
        }

        public void ResetStat()
        {
            _lives = 3;
            _countTimeAlive = 0f;
            _countAttackCombo = 0;
            _timeCountAttackCombo = 0f;
        }
    }
}