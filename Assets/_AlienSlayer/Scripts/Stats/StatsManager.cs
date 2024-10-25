using System;

namespace drland.AlienSlayer
{
    [Serializable]
    public class EntityStats
    {
        public int Health;
        public float Speed;
        public int Damage;
        public float Armor;
        public float AttackSpeed;
        public float AttackRange;

        public EntityStats(EntityStats stats)
        {
            Health = stats.Health;
            Speed = stats.Speed;
            Damage = stats.Damage;
            Armor = stats.Armor;
            AttackSpeed = stats.AttackSpeed;
            AttackRange = stats.AttackRange;

        }
    }

    public class StatsManager
    {
        private EntityStats _maxStats;
        private EntityStats _currentStats;

        public EntityStats Max => _maxStats;
        public EntityStats Current => _currentStats;

        public StatsManager(EntityStats maxStats)
        {
            _maxStats = new EntityStats(maxStats);
            _currentStats = new EntityStats(maxStats);
        }
    }
}