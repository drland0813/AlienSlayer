using System;
using UnityEngine;

namespace drland.AlienSlayer
{
    public abstract class EnemyAttackComponent : MonoBehaviour
    {
        public EnemyAttackType AttackType;
        public int Damage;

        public virtual void Init(int damage)
        {
            Damage = damage;
        }
    }
}