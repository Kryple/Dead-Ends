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

            //store the player's direction of the previous 12th frame as the previous direction 
            int tmpDirListId = _dirListId;
            if (tmpDirListId >= _dirListCap - 1) tmpDirListId = 0;
            else tmpDirListId++;
            
            float tmpX = _dirList[tmpDirListId].Key;
            float tmpY = _dirList[tmpDirListId].Value;
            
            SavePrevDir(tmpX, ref _prevDir.x);
            SavePrevDir(tmpY, ref _prevDir.y);
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
            Vector3 scaledDirect = _direction * _speed;
            _transform.Translate(scaledDirect * Time.deltaTime);;
            
            //2nd way:
            //_transform.position = Vector3.MoveTowards(_transform.position, _transform.position + _direction * _speed, Time.deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
            _audioSource.Stop();
            
        }
    }
}