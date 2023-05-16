using AI.FluentFiniteStateMachine;

namespace _Project.Logic.Enemies.States
{
    internal class Attack : State<Enemy>, IEnterState
    {
        public Attack(Enemy context) : base(context) {}
        
        public void OnEnter() => 
            Context.Attack();
    }
}