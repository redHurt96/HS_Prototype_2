using UnityEngine;

namespace _Project.Logic.Utilities
{
    public abstract class CharacterTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement character))
                ExecuteOnEnter(character);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement character))
                ExecuteOnExit(character);
        }

        protected abstract void ExecuteOnEnter(PlayerMovement csPlayerController);
        protected virtual void ExecuteOnExit(PlayerMovement csPlayerController) {}
    }
}