using UnityEngine;
using static UnityEngine.Vector3;

namespace _Project.Logic.Enemies
{
    public class EnemyTargetAssigner : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private EnemyBehaviorInstaller _enemyBehaviorInstaller;

        private Enemy Context => _enemyBehaviorInstaller.Context;
        
        private void Update()
        {
            if (Distance(_playerStats.transform.position, transform.position) < Context.ChaseDistance)
            {
                Context.SetTarget(_playerStats);
            }
            else
            {
                Context.ClearTarget();
            }
        }
    }
}