using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace drland.AlienSlayer
{
    public interface IDamageable
    {
        public delegate void TakeDamageEvent(int damage);
        public event TakeDamageEvent OnTakeDamage;

        public delegate void DeathEvent();
        public event DeathEvent OnDeath;

        public void TakeDamage(int damage);
    }
}
