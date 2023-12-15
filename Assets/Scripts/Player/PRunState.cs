using FSM;
using UnityEngine;

namespace Player
{
    public class PRunState : PPatrolState
    {
        public PRunState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {

            

        }
        
        public override void Enter()
        {
            base.Enter();
            _audioSource.clip = _walkingSound;
            _audioSource.loop = true;
            _audioSource.pitch = 0.72f;
            _audioSource.volume = 0.85f;
            _audioSource.Play();
            _speed = _runSpeed; 
        }

        public override void UpdateLogic()
        {
            
            base.UpdateLogic();
            

            
            _animator.SetFloat(a_floSpeed, _direction.magnitude);

            int tmpDirListId = _dirListId;
            if (tmpDirListId >= _dirListCap - 1) tmpDirListId = 0;
            else tmpDirListId++;

            Debug.Log("id: " + tmpDirListId);
            float tmpX = _dirList[tmpDirListId].Key;
            float tmpY = _dirList[tmpDirListId].Value;
            
            
   
            // Debug.Log("tmpx: " + tmpX + ", tmpy: " + tmpY);
            SavePrevDir(tmpX, ref _prevDir.x);
            SavePrevDir(tmpY, ref _prevDir.y);
            // Debug.Log("The prev Dir" + _prevDir);
        }
        
        public void SavePrevDir(float a, ref float prev)
        {
            if (a > 0) prev = 1;
            else if (a < 0) prev = -1;
            else prev = 0;
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            _rigidbody2D.AddForce(_direction * _speed);
        }

        public override void Exit()
        {
            base.Exit();
            _audioSource.Stop();
            
            
            
        }
    }
}