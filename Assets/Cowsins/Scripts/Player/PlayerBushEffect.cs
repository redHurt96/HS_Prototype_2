using System;
using _Project.Logic.Utilities;
using UnityEngine;

namespace Cowsins.Player
{
    public class PlayerBushEffect : MonoBehaviour
    {
        [SerializeField] private PlayerStats _stats;

        private PostprocessHandler _postProcess;
        private IDisposable _disposable;

        private bool _cachedState;

        private void Start()
        {
            _postProcess = FindObjectOfType<PostprocessHandler>();
            _cachedState = _stats.IsInBushes;
        }

        private void Update()
        {
            if (_cachedState != _stats.IsInBushes)
                InteractWithBush(_stats.IsInBushes);

            _cachedState = _stats.IsInBushes;
        }

        private void InteractWithBush(bool inBush)
        {
            if (inBush)
                _postProcess.EnableBushesEffect();
            else
                _postProcess.DisableBushesEffect();
        }
    }
}