using _Project.Logic.Enemies.States;
using AI.FluentFiniteStateMachine;
using Cowsins.Player;
using UnityEngine;

namespace _Project.Logic.Enemies
{
    public class EnemyBehaviorInstaller : MonoBehaviour
    {
        public Enemy Context => _context;

        [SerializeField] private Enemy _context;
        
        private StateMachine _stateMachine;

        private void Start()
        {
            _context.InstallTarget(FindObjectOfType<PlayerStats>());
            
            _stateMachine = new StateMachine()
                .AddStates(
                    new Patrol(Context),
                    new Stand(Context),
                    new RotateToTarget(Context),
                    new Chase(Context),
                    new Attack(Context))
                .AddTransitions(
                    new Transition(typeof(Patrol), typeof(RotateToTarget), Context.PayAttentionToTarget),
                    new Transition(typeof(Patrol), typeof(Stand), Context.ReachPatrolPoint),
                    new Transition(typeof(Patrol), typeof(Chase), Context.HasTarget),
                    new Transition(typeof(Patrol), typeof(Attack), Context.CanAttack),
                    new Transition(typeof(Stand), typeof(Patrol), Context.WaitEnough),
                    new Transition(typeof(Stand), typeof(RotateToTarget), Context.PayAttentionToTarget),
                    new Transition(typeof(Stand), typeof(Chase), Context.HasTarget),
                    new Transition(typeof(Stand), typeof(Attack), Context.CanAttack),
                    new Transition(typeof(Chase), typeof(RotateToTarget), Context.HasNoTarget),
                    new Transition(typeof(Chase), typeof(Attack), Context.CanAttack),
                    new Transition(typeof(Attack), typeof(Chase), Context.FarFromTarget),
                    new Transition(typeof(Attack), typeof(RotateToTarget), Context.HasNoTarget),
                    new Transition(typeof(RotateToTarget), typeof(Patrol), Context.LostTarget),
                    new Transition(typeof(RotateToTarget), typeof(Chase), Context.HasTarget),
                    new Transition(typeof(RotateToTarget), typeof(Attack), Context.CanAttack))
                .Run();
        }

        private void Update() => 
            _stateMachine.Update();
    }
}