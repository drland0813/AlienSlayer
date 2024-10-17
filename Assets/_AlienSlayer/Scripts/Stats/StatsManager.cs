using System;

namespace drland.AlienSlayer
{
    [Serializable]
    public class EntityStats
    {
        public float Health;
        public float Speed;
        public float Damage;
        public float Armor;
        public float AttackSpeed;
        public float AttackRange;
    }

    [Serializable]
    public class StatsManager
    {
        private EntityStats _maxStats;
        private EntityStats _currentStats;

        public EntityStats Max => _maxStats;
        public EntityStats Current => _currentStats;

        public StatsManager(EntityStats maxStats)
        {
            _maxStats = maxStats;
            _currentStats = new EntityStats();
        }
    }
}