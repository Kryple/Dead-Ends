using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using StateMachine = FSM.StateMachine;

namespace Enemy.Chaser
{
    public class ERunState : EAllStates
    {
      
        private string a_isRunning = "isRunning";
        private float _speed = 560f;
        private float _nextWayPointDist = 3f;

        private Path _path;
        private int _currentWayPoint = 0;
        private bool _hasReachedEndOfPath = false;
        
        
        public ERunState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }
        
        public override void Enter()
        {
            //Change to run animation
            _animator.SetTrigger(a_isRunning);
            base.Enter();
            
            
        }

        public void UpdatePath()
        {
            if (_seeker.IsDone())
                //The last parameter is a function that we would like it to call whenever it's done calculating the path. We need to do this, cuz depend on the complexity of the scene, generating a path might take a little while, and we don't want out game to be hung up with generating a path, it should just do it in the background, and notify us once it's done
                _seeker.StartPath(_rigidbody2D.position, _player.position, OnPathComplete);
                
        }

        void OnPathComplete(Path p)
        {
            //if there is no error with this path
            if (!p.error)
            {
                _path = p;
                _currentWayPoint = 0;
            }
            
             
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            
            
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            
            if (_path == null) return;
            if (_currentWayPoint >= _path.vectorPath.Count)
            {
                _hasReachedEndOfPath = true;
                return;
            }
            else _hasReachedEndOfPath = false;
            
            //Direction to the next way point
            Vector2 direction = ((Vector2)_path.vectorPath[_currentWayPoint] - _rigidbody2D.position).normalized;
            
            //multiply with deltaTime to make sure it not depending on frame rate
            Vector2 force = direction * _speed * Time.deltaTime;
            _rigidbody2D.AddForce(force);    
            
            
            float distance = Vector2.Distance(_rigidbody2D.position, _path.vectorPath[_currentWayPoint]);
            if (distance < _nextWayPointDist)
                _currentWayPoint++;
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        
        
        
    }
}