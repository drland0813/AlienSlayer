using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace drland.AlienSlayer
{
    public enum EntityType
    {
        Player,
        Zombie,
        Boss
    }

    public abstract class BaseEntity : MonoBehaviour
    {
        [SerializeField] public EntitySO _entitySO;
        public StatsManager StatsManager;
        public AnimatorManager AnimatorManager;
        [SerializeField] protected EntityStats _currentStats;

        protected virtual void Awake()
        {
            AnimatorManager = GetComponent<AnimatorManager>();
            StatsManager = new StatsManager(_entitySO.Stats);
        }

        public void Init()
        {
            AnimatorManager.Init();
        }

        private void Update()
        {
            _currentStats = StatsManager.Current;
        }
    }
}

