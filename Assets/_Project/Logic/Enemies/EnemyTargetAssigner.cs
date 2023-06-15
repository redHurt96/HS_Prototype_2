using Cowsins.Player;
using UnityEngine;
using static UnityEngine.Color;
using static UnityEngine.Physics;
using static UnityEngine.Vector3;

namespace _Project.Logic.Enemies
{
    public class EnemyTargetAssigner : MonoBehaviour
    {
        [SerializeField] private EnemyBehaviorInstaller _enemyBehaviorInstaller;
        
        private PlayerStats _playerStats;

        private Enemy Context => _enemyBehaviorInstaller.Model;

        private void Start() => 
            _playerStats = FindObjectOfType<PlayerStats>();

        private void Update()
        {
            bool seeTarget = CloseEnough()
                             && InFrontOfTarget()
                             && TargetInSight();
            
            Debug.DrawLine(transform.position, _playerStats.transform.position, seeTarget ? red : green);
            
            if (seeTarget)
                Context.IncreaseProgress();
            else
                Context.DecreaseProgress();
        }
        
        private bool CloseEnough() =>
            _playerStats.IsInBushes
                ? Distance(transform.position, _playerStats.transform.position) <= Context.TargetDetectDistanceInBushes
                : Distance(transform.position, _playerStats.transform.position) <= Context.TargetDetectDistance;

        private bool InFrontOfTarget() => 
            Angle(transform.forward, (_playerStats.transform.position - transform.position).normalized) < Context.TargetDetectAngle;

        private bool TargetInSight() =>
            Raycast(transform.position, (_playerStats.transform.position - transform.position).normalized, out RaycastHit hit)
            && hit.transform.TryGetComponent(out PlayerStats controller);
    }
}