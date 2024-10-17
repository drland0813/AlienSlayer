using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace drland.AlienSlayer
{
    public enum EntityType
    {
        Player,
        BabyAlien,
        Boss
    }

    public abstract class BaseEntity : MonoBehaviour
    {
        [SerializeField] protected EntitySO _entitySO;
        public StatsManager StatsManager;
        public AnimatorManager AnimatorManager;

        protected virtual void Awake()
        {
            AnimatorManager = GetComponent<AnimatorManager>();
            StatsManager = new StatsManager(_entitySO.Stats);
        }

        public virtual void Init()
        {
            AnimatorManager.Init();
        }
    }
}

