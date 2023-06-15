using UnityEngine;
using UnityEngine.UI;

namespace _Project.Logic.Enemies
{
    public class ObserveUI : MonoBehaviour
    {
        [SerializeField] private Image _fill;

        public void SetProgress(float observeProgress) => 
            _fill.fillAmount = observeProgress;
    }
}