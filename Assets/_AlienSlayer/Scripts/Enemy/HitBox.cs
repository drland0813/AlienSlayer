using UnityEngine;

namespace drland.AlienSlayer
{
    public class HitBox : MonoBehaviour
    {
        public HealthComponent HealthComponent;

        public void TakeDamage(int damage, Vector3 direction)
        {
            HealthComponent.TakeDamage(damage);
        }
    }
}