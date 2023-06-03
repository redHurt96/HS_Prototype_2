using UnityEngine;

namespace _Project.Logic.Utilities
{
    public abstract class CharacterTrigger<T> : MonoBehaviour where T : Component
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out T target))
                ExecuteOnEnter(target);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out T target))
                ExecuteOnExit(target);
        }

        protected abstract void ExecuteOnEnter(T target);
        protected virtual void ExecuteOnExit(T target) {}
    }
}