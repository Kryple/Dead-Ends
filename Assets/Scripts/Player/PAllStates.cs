using System.Collections.Generic;
using Core.Observer_Pattern;
using FSM;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

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
        protected static Transform _transform;

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
            _transform = _pStateMachine._transform;


            // _pStateMachine.CheckComponentNull(_audioSource);
            // _pStateMachine.CheckComponentNull(_collider2D);
            // _pStateMachine.CheckComponentNull(_rigidbody2D);
            // _pStateMachine.CheckComponentNull(_animator);
            // _pStateMachine.CheckComponentNull(_spriteRenderer);
            
            
            //Initial value for the directionList
            for (int i = 0; i < _dirListCap; i++)
                _dirList.Add(new KeyValuePair<float, float>(0f, 0f));
            
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            
            
            
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            
            //Add player's direction coordinate of this frame to the _posList
            if (_dirListId >= _dirListCap)
            {
                _dirListId = 0;
            }
            else
            {
                // Debug.Log("index: " + _dirListId + "_dircap" + _dirListCap);
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
            Debug.Log("Live: " + _lives);
            _lives -= damage;
            if (_lives <= 0)
                _pStateMachine.ChangeState(_pStateMachine._pDieState);
            
            _pStateMachine.NotifyObservers(IEvent.OnPlayerGetHurt);
        }

        public void ResetStat()
        {
            _lives = 3;
        }
    }
}