using UnityEngine;
using UnityEngine.UI;

namespace _Project.Logic.Enemies
{
    public class ObserveTargetProgressUI : MonoBehaviour
    {
        [SerializeField] private EnemyBehaviorInstaller _enemy;
        [SerializeField] private Image _parent;
        [SerializeField] private Image _fill;

        private Enemy Context => _enemy.Context;

        private void Update()
        {
            _parent.gameObject.SetActive(Context.ObserveProgress > 0f);
            _fill.fillAmount = Context.ObserveProgress;
        }
    }
}