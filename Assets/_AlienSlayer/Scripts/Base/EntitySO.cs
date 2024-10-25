using UnityEngine;

namespace drland.AlienSlayer
{
    [CreateAssetMenu(menuName = "EntitySO", fileName = "EntitySO", order = 0)]
    public class EntitySO : ScriptableObject
    {
        public EntityType Type;
        public EntityStats Stats;
        public GameObject Prefab;
        public string Name;
    }
}