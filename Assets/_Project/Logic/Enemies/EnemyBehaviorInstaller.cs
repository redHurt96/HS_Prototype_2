using _Project.Logic.Enemies.States;
using AI.FluentFiniteStateMachine;
using UnityEngine;

namespace _Project.Logic.Enemies
{
    public class EnemyBehaviorInstaller : MonoBehaviour
    {
        public Enemy Context => _context;

        [SerializeField] private Enemy _context;
        
        private StateMachine _stateMachine;

        private void Start() =>
            _stateMachine = new StateMachine()
                .AddStates(
                    new Patrol(Context),
                    new Stand(Context),
                    new Chase(Context),
                    new Attack(Context))
                .AddTransitions(
                    new Transition(typeof(Patrol), typeof(Stand), Context.ReachTarget),
                    new Transition(typeof(Stand), typeof(Chase), Context.HasTarget),
                    new Transition(typeof(Stand), typeof(Attack), Context.CanAttack),
                    new Transition(typeof(Stand), typeof(Patrol), Context.WaitEnough),
                    new Transition(typeof(Patrol), typeof(Chase), Context.HasTarget),
                    new Transition(typeof(Patrol), typeof(Attack), Context.CanAttack),
                    new Transition(typeof(Chase), typeof(Patrol), Context.HasNoTarget),
                    new Transition(typeof(Chase), typeof(Attack), Context.CanAttack),
                    new Transition(typeof(Attack), typeof(Patrol), Context.HasNoTarget),
                    new Transition(typeof(Attack), typeof(Chase), Context.FarFromTarget));

        private void Update() => 
            _stateMachine.Update();
    }
}