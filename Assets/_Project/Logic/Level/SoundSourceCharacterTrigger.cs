using _Project.Logic.Enemies;
using Cowsins.Player;
using UnityEngine;
using static UnityEngine.Physics;

namespace _Project.Logic.Utilities
{
    public class SoundSourceCharacterTrigger : CharacterTrigger<PlayerStats>
    {
        [SerializeField] private float _radius;

        protected override void ExecuteOnEnter(PlayerStats target) => 
            ProvideSound();

        private void ProvideSound()
        {
            Collider[] colliders = OverlapSphere(transform.position, _radius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out EnemyBehaviorInstaller behaviorInstaller))
                    behaviorInstaller.Model.HearTarget();
            }
        }
    }
}