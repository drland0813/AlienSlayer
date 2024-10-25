using UnityEngine;

namespace drland.AlienSlayer
{
    public class HitBoxCollider : MonoBehaviour
    {
        public int Damage;
        private void OnTriggerEnter(Collider other)
        {
            HealthComponent healthComponent = other.GetComponent<HealthComponent>();
            healthComponent?.TakeDamage(Damage);
        }
    }
}