using AI.FluentFiniteStateMachine;

namespace _Project.Logic.Enemies.States
{
    internal class Stand : State<Enemy>, IEnterState
    {
        public Stand(Enemy context) : base(context) {}

        public void OnEnter() => 
            Context.StopAtPatrolPoint();
    }
}