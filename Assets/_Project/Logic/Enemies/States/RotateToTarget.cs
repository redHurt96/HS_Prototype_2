using AI.FluentFiniteStateMachine;

namespace _Project.Logic.Enemies.States
{
    internal class RotateToTarget : State<Enemy>, IEnterState
    {
        public RotateToTarget(Enemy context) : base(context) {}

        public void OnEnter() => 
            Context.RotateToTarget();
    }
}