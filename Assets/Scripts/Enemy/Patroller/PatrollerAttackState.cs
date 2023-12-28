using System.Threading.Tasks;
using FSM;

namespace Enemy.Patroller
{
    public class PatrollerAttackState : PatrollerAllStates
    {
        private string a_Attack = "Attack";
        public PatrollerAttackState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _audioSource.clip = _biteSFX;
            _audioSource.volume = 0.075f;
            
            _animator.SetTrigger(a_Attack);
            _patrollerStateMachine.AttackMotion(_self.position);
            ChangeToRun();
        }

        public async void ChangeToRun()
        {
            await Task.Delay(1000);
            _patrollerStateMachine.ChangeState(_patrollerStateMachine._patrollerRunState);
        }
        
        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

        public override void Exit()
        {
            base.Exit();
            _audioSource.Stop();
        }
    }
}