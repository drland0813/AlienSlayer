using UnityEngine;

namespace drland.AlienSlayer
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] private StatsManager _statsManager;
        public event IDamageable.TakeDamageEvent OnTakeDamage;
        public event IDamageable.DeathEvent OnDeath;
        public void TakeDamage(int damage)
        {
            var damageTaken = Mathf.Clamp(damage, 0, _statsManager.Current.Health);
            _statsManager.Current.Health -= damageTaken;

            if (damageTaken != 0)
            {
                OnTakeDamage?.Invoke(damageTaken);
            }

            if (_statsManager.Current.Health == 0)
            {
                OnDeath?.Invoke(transform.position);
                Death();
            }
        }

        private void Death()
        {
            OnDeath?.Invoke(transform.position);
        }
    }
}