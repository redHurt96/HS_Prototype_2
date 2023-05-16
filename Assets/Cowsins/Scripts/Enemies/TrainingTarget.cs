using UnityEngine;

namespace Cowsins.Enemies
{
    public class TrainingTarget : Enemy
    {
        [SerializeField] private float timeToRevive;
    
        private UIController _uiController;
        private CompassElement _compassElement;
        private bool _isDead;
        
        protected override void Start()
        {
            base.Start();
        
            _uiController = UI.GetComponent<UIController>();
            _compassElement = transform.parent.GetComponent<CompassElement>();
        }

        public override void Damage(float damage)
        {
            if (_isDead)
                return;

            base.Damage(damage);
        }

        public override void Die()
        {
            if (_isDead)
                return;

            _isDead = true;

            events.OnDeath.Invoke();

            Invoke(nameof(Revive), timeToRevive);

            if (shieldSlider != null)
                shieldSlider.gameObject.SetActive(false);
        
            if (healthSlider != null)
                healthSlider.gameObject.SetActive(false);

            if (_uiController.displayEvents)
                _uiController.AddKillfeed(name);

            if (_compassElement != null)
                _compassElement.Remove();
        }

        private void Revive()
        {
            _isDead = false;
            health = maxHealth;
            shield = maxShield;

            if (shieldSlider != null) 
                shieldSlider.gameObject.SetActive(true);
        
            if (healthSlider != null) 
                healthSlider.gameObject.SetActive(true);

            if (transform.parent.GetComponent<CompassElement>() != null)
                transform.parent.GetComponent<CompassElement>().Add();
        }
    }
}