using UnityEngine;

namespace drland.AlienSlayer
{
    public class HitBox : MonoBehaviour
    {
        public HealthComponent HealthComponent;

        public void TakeDamage(int damage)
        {
            HealthComponent.TakeDamage(damage);
        }
    }
}