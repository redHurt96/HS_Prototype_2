using _Project.Logic.Enemies.States;
using AI.FluentFiniteStateMachine;
using Cowsins.Player;
using UnityEngine;

namespace _Project.Logic.Enemies
{
    public class EnemyBehaviorInstaller : MonoBehaviour
    {
        public Enemy Model => _model;

        [SerializeField] private Enemy _model;
        
        private StateMachine _stateMachine;

        private void Start()
        {
            _model.InstallTarget(FindObjectOfType<PlayerStats>());
            
            _stateMachine = new StateMachine()
                .AddStates(
                    new Patrol(Model),
                    new Stand(Model),
                    new RotateToTarget(Model),
                    new Chase(Model),
                    new Attack(Model))
                .AddTransitions(
                    new Transition(typeof(Patrol), typeof(RotateToTarget), Model.PayAttentionToTarget),
                    new Transition(typeof(Patrol), typeof(Stand), Model.ReachPatrolPoint),
                    new Transition(typeof(Patrol), typeof(Chase), Model.HasTarget),
                    new Transition(typeof(Patrol), typeof(Attack), Model.CanAttack),
                    new Transition(typeof(Stand), typeof(Patrol), Model.WaitEnough),
                    new Transition(typeof(Stand), typeof(RotateToTarget), Model.PayAttentionToTarget),
                    new Transition(typeof(Stand), typeof(Chase), Model.HasTarget),
                    new Transition(typeof(Stand), typeof(Attack), Model.CanAttack),
                    new Transition(typeof(Chase), typeof(RotateToTarget), Model.HasNoTarget),
                    new Transition(typeof(Chase), typeof(Attack), Model.CanAttack),
                    new Transition(typeof(Attack), typeof(Chase), Model.FarFromTarget),
                    new Transition(typeof(Attack), typeof(RotateToTarget), Model.HasNoTarget),
                    new Transition(typeof(RotateToTarget), typeof(Patrol), Model.LostTarget),
                    new Transition(typeof(RotateToTarget), typeof(Chase), Model.HasTarget),
                    new Transition(typeof(RotateToTarget), typeof(Attack), Model.CanAttack))
                .Run();
        }

        private void Update() => 
            _stateMachine.Update();
    }
}