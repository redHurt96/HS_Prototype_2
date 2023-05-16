using AI.FluentFiniteStateMachine;

namespace _Project.Logic.Enemies.States
{
    internal class Chase : State<Enemy>, IEnterState, IUpdateState, IExitState
    {
        public Chase(Enemy context) : base(context) {}
        
        public void OnEnter() => 
            Context.MoveToTarget();

        public void OnUpdate() => 
            Context.MoveToTarget();

        public void OnExit() => 
            Context.Stop();
    }
}