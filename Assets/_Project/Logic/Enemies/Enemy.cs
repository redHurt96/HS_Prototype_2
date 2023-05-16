using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Time;
using static UnityEngine.Vector3;

namespace _Project.Logic.Enemies
{
    [Serializable]
    public class Enemy
    {
        public Transform[] PatrolPoints;
        
        [Space]
        [SerializeField] private float _chaseDistance = 10f;
        [SerializeField] private float _chaseUpdateTime = .5f;
        [SerializeField] private float _attackRange = 2f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _attackDamage = 10f;
        
        [Space]
        [SerializeField] private Transform _transform;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private PlayerStats _target;
        private float _lastChaseTime;
        private float _lastAttackTime;

        public float ChaseDistance => _chaseDistance;

        public bool HasTarget() => 
            _target != null;

        public bool HasNoTarget() => !HasTarget();

        public bool CanAttack() =>
            _target != null 
            && Distance(_target.transform.position, _transform.position) <= _attackRange
            && time > _lastAttackTime + _attackCooldown;

        public bool FarFromTarget() => !CanAttack();

        public void SetTarget(PlayerStats target) => 
            _target = target;

        public void ClearTarget() => 
            _target = null;

        public void MoveTo(Transform targetPoint)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(targetPoint.position);
        }

        public bool ReachTarget() => 
            _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;

        public void Stop() => 
            _navMeshAgent.isStopped = true;

        public void MoveToTarget()
        {
            if (_lastChaseTime + _chaseUpdateTime < time)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_target.transform.position);
                _lastChaseTime = time;
            }
        }

        public void Attack()
        {
            _target.Damage(_attackDamage);
            _lastAttackTime = time;
        }
    }
}