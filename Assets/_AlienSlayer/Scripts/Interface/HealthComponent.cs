using System;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHasProgress
    {
        private StatsManager _statsManager;
        public event IDamageable.TakeDamageEvent OnTakeDamage;
        public event IDamageable.DeathEvent OnDeath;
        public bool IsDead => _isDead;
        private bool _isDead;

        public void Init(StatsManager statsManager)
        {
            _statsManager = statsManager;
        }
        public void TakeDamage(int damage)
        {
            if (_isDead) return;
            
            var damageTaken = Mathf.Clamp(damage, 0, _statsManager.Current.Health);
            _statsManager.Current.Health -= damageTaken;
            if (damageTaken != 0)
            {
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                {
                    CurrentValue = _statsManager.Current.Health,
                    ProgressNormalized = (float) _statsManager.Current.Health / _statsManager.Max.Health
                });
                OnTakeDamage?.Invoke(damageTaken);
            }

            if (_statsManager.Current.Health == 0)
            {
                _isDead = true;
                Death();
            }
        }

        private void Death()
        {
            OnDeath?.Invoke();
        }

        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    }
}