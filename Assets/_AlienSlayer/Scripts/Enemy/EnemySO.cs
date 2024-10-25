using System;
using UnityEngine;

namespace drland.AlienSlayer
{
    public enum EnemyAttackType
    {
        Melee,
        Range
    }

    [CreateAssetMenu(menuName = "EntitySO/Enemy", fileName = "EnemySO", order = 1)]
    public class EnemySO : EntitySO
    {

    }
}