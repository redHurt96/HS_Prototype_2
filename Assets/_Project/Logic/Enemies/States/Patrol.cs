using AI.FluentFiniteStateMachine;
using UnityEngine;

namespace _Project.Logic.Enemies.States
{
    internal class Patrol : State<Enemy>, IEnterState, IUpdateState, IExitState
    {
        private Transform TargetPoint => Context.PatrolPoints[_targetPointIndex];
        
        private int _targetPointIndex;

        public Patrol(Enemy context) : base(context) {}

        public void OnEnter()
        {
            if (Context.PatrolPoints.Length == 0)
                return;
            
            _targetPointIndex = 0;
            Context.MoveTo(TargetPoint);
        }

        public void OnUpdate()
        {
            if (Context.PatrolPoints.Length == 0)
                return;
            
            if (Context.ReachTarget())
            {
                _targetPointIndex = _targetPointIndex == Context.PatrolPoints.Length - 1
                    ? 0
                    : _targetPointIndex++;

                Context.MoveTo(TargetPoint);
            }
        }

        public void OnExit() => 
            Context.Stop();
    }
}