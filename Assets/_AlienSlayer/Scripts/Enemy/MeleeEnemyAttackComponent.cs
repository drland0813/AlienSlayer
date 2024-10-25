using UnityEngine;

namespace drland.AlienSlayer
{
    public class MeleeEnemyAttackComponent : EnemyAttackComponent
    {
        [SerializeField] private Collider[] _collider;
        

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("deal");
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(Damage);
        }

        public  void StartDealDamage(int index)
        {
            _collider[index].gameObject.SetActive(true);
        }

        public  void EndDealDamage(int index)
        {
            _collider[index].gameObject.SetActive(false);
        }
    }
}