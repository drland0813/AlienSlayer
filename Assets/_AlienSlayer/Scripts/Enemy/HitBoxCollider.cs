using UnityEngine;

namespace drland.AlienSlayer
{
    public class HitBoxCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("deal");
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(10);
        }
    }
}