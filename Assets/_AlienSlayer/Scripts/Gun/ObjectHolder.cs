using UnityEngine;
using UnityEngine.Serialization;

namespace drland.AlienSlayer
{
    public class ObjectHolder : MonoBehaviour
    {
        [SerializeField] private Transform _holder;

        public void Init(GameObject gameObject)
        {
            Instantiate(gameObject, _holder);
        }

        public void ClearData()
        {
            if (_holder.childCount == 0) return;

            Destroy(_holder.GetChild(0).gameObject);
        }
    }
}   
