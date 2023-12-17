using System.Threading.Tasks;
using FSM;
using UnityEngine;

namespace Enemy
{
    public class EAttackState : EAllStates
    {
        private string a_Attack = "Attack";
        
        
        
        public EAttackState(string name, StateMachine stateMachine) : base(name, stateMachine)
        {
            
        }
        
        public override void Enter()
        {
            base.Enter();
            _animator.SetTrigger(a_Attack);
            _eStateMachine.AttackMotion(_self.position);
            ChangeToRun();

        }
        
        public async void ChangeToRun()
        {
            await Task.Delay(1000);
            _eStateMachine.ChangeState(_eStateMachine._eRunState);
            
            
            
            
        }


        public override void UpdateLogic()
        {
            base.UpdateLogic();
            
            
            
        }


        

        /*
         
         */
        
        

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

        public override void Exit()
        {
            base.Exit();
        }

        
        
    }
}