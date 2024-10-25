using UnityEngine;

namespace drland.AlienSlayer
{
    public class MeleeEnemyAttackComponent : EnemyAttackComponent
    {
        [SerializeField] private HitBoxCollider[] _collider;

        public override void Init(int damage)
        {
            base.Init(damage);
            _collider[0].Damage = Damage;
            _collider[1].Damage = Damage;
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