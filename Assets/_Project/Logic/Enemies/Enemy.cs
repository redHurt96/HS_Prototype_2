using System;
using Cowsins.Player;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Mathf;
using static UnityEngine.Time;
using static UnityEngine.Vector3;

namespace _Project.Logic.Enemies
{
    [Serializable]
    public class Enemy
    {
        public Transform[] PatrolPoints;
        
        [Header("Patrolling")]
        [SerializeField] private float _stoppingDistance = 1f;
        [SerializeField] private float _standTime = 3f;

        [Header("Target detection")]
        [SerializeField] private float _targetDetectDistance = 10f;
        [SerializeField] private float _targetDetectDistanceInBushes = 3f;
        [SerializeField] private float _targetDetectAngle = 45f;
        [SerializeField] private float _targetDetectSpeed = 1f;
        [SerializeField] private float _targetLostSpeed = 1f;
        [SerializeField] private float _payAttentionObserveProgress = .5f;

        [Header("Hea")]
        [SerializeField] private float _observeProgressFromHearing = .5f;
        
        [Header("Chasing")]
        [SerializeField] private float _chaseUpdateTime = .5f;
        
        [Header("Fight")]
        [SerializeField] private float _attackRange = 2f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _attackDamage = 10f;

        [Space]
        [SerializeField] private Transform _transform;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [Header("Read Only!")] 
        [SerializeField] private float _observeProgress;
        [SerializeField] private PlayerStats _target;
        
        private float _lastChaseTime;
        private float _lastAttackTime;
        private float _lastStandTime;

        public float TargetDetectDistance => _targetDetectDistance;
        public float TargetDetectAngle => _targetDetectAngle;
        public float TargetDetectDistanceInBushes => _targetDetectDistanceInBushes;
        public float ObserveProgress => _observeProgress;

        public bool HasTarget() => _observeProgress >= 1f;
        public bool HasNoTarget() => !HasTarget();
        public bool SeeTarget() => 
            _target != null;

        public bool CanAttack() =>
            HasTarget()
            && Distance(_target.transform.position, _transform.position) <= _attackRange
            && time > _lastAttackTime + _attackCooldown;

        public bool FarFromTarget() => !CanAttack();

        public void MoveTo(Transform targetPoint)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(targetPoint.position);
        }

        public bool ReachPatrolPoint() => 
            _navMeshAgent.path != default && _navMeshAgent.remainingDistance <= _stoppingDistance;

        public void Stop() => 
            _navMeshAgent.isStopped = true;

        public void MoveToTarget()
        {
            if (_lastChaseTime + _chaseUpdateTime < time || Approximately(_lastChaseTime, 0f))
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

        public bool WaitEnough() => 
            _lastStandTime + _standTime < time;

        public void StopAtPatrolPoint()
        {
            Stop();
            _lastStandTime = time;
        }

        public void IncreaseProgress() => 
            _observeProgress = Clamp01(_observeProgress + deltaTime * _targetDetectSpeed);

        public void DecreaseProgress() => 
            _observeProgress = Clamp01(_observeProgress - deltaTime * _targetLostSpeed);

        public void HearTarget() => 
            _observeProgress = Clamp01(_observeProgress + _observeProgressFromHearing);

        public void RotateToTarget()
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_transform.position +
                                         (_target.transform.position - _transform.position).normalized * 2f);
        }

        public bool PayAttentionToTarget() => 
            _observeProgress > _payAttentionObserveProgress;

        public bool LostTarget() => 
            Approximately(_observeProgress, 0f);

        public void InstallTarget(PlayerStats playerStats) => 
            _target = playerStats;
    }
}