using AI.FluentFiniteStateMachine;
using UnityEngine;

namespace _Project.Logic.Enemies.States
{
    internal class Patrol : State<Enemy>, IEnterState
    {
        private Transform TargetPoint => Context.PatrolPoints[_targetPointIndex];

        private int _targetPointIndex;

        public Patrol(Enemy context) : base(context) {}

        public void OnEnter()
        {
            if (Context.PatrolPoints.Length == 0)
                return;
            
            _targetPointIndex = _targetPointIndex == Context.PatrolPoints.Length - 1
                ? 0
                : _targetPointIndex + 1;

            Context.MoveTo(TargetPoint);
        }
    }
}