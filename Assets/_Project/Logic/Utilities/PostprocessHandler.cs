using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Logic.Utilities
{
    public class PostprocessHandler : MonoBehaviour
    {
        [SerializeField] private Volume _hideVolume;
        [SerializeField] private float _duration;
        
        private Coroutine _currentProcess;

        public void EnableBushesEffect()
        {
            if (_currentProcess != null)
            {
                StopCoroutine(_currentProcess);
                _currentProcess = null;
            }

            _currentProcess = StartCoroutine(Enable());
        }

        public void DisableBushesEffect()
        {
            if (_currentProcess != null)
            {
                StopCoroutine(_currentProcess);
                _currentProcess = null;
            }

            _currentProcess = StartCoroutine(Disable());
        }

        private IEnumerator Enable()
        {
            float time = 0f;

            while (time < _duration)
            {
                _hideVolume.weight = Mathf.Lerp(0f, 1f, time / _duration);
                time += Time.deltaTime;

                yield return null;
            }
        }

        private IEnumerator Disable()
        {
            float time = 0f;

            while (time < _duration)
            {
                _hideVolume.weight = Mathf.Lerp(1f, 0f, time / _duration);
                time += Time.deltaTime;

                yield return null;
            }
        }
    }
}