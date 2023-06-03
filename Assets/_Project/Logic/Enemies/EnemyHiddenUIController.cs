using UnityEngine;
using static UnityEngine.GameObject;
using static UnityEngine.Resources;

namespace _Project.Logic.Enemies
{
    public class EnemyHiddenUIController : MonoBehaviour
    {
        private Enemy Model => _installer.Model;
        
        [SerializeField] private Renderer _renderer;
        [SerializeField] private EnemyBehaviorInstaller _installer;

        private Transform _uiParent;
        private ObserveUI _ui;

        private void Start()
        {
            _uiParent = Find("EnemiesObserveUI").transform;
            _ui = Instantiate(Load<ObserveUI>("ObserveUI"), _uiParent);
            _ui.gameObject.SetActive(false);
        }

        private void Update()
        {
            _ui.gameObject.SetActive(!_renderer.isVisible && Model.ObserveProgress > 0f);
            _ui.SetProgress(Model.ObserveProgress);
        }
    }
}