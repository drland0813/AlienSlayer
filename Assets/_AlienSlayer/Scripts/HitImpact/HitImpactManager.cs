using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Drland.Common.Utils;
using Drland.Ultils;
using UnityEngine;

namespace drland.AlienSlayer
{
    
    public class HitImpactManager : Singleton<HitImpactManager>
    {
        [SerializeField] private ObjectHolder _hitImpactSample;
        [SerializeField] private Transform _hitImpactHolder;
        [SerializeField] private List<HitImpactSO> _hitImpactSOList;

        private ObjectPool<ObjectHolder> _impactHitPool;

        private void Start()
        {
            _impactHitPool = new ObjectPool<ObjectHolder>(_hitImpactSample);
        }

        private HitImpactSO GetHitImpactSO(BulletName name)
        {
           return _hitImpactSOList.FirstOrDefault(i => i.Name == name);
        }
        

        public void CreateHitImpact(BulletName hitImpactName, RaycastHit raycastHit, Vector3 endPosition)
        {
            bool isCollide = raycastHit.collider;
            var mask = isCollide ? raycastHit.collider.gameObject.layer : 0;
            var lookRotation = isCollide ? raycastHit.normal : endPosition;
            var hitImpactSO = GetHitImpactSO(hitImpactName);

            var prefab = hitImpactSO.GetHitImpactCollisionPrefab(mask);
            var impactHit = _impactHitPool.Get(_hitImpactHolder);
            impactHit.Init(prefab);
            impactHit.transform.position = endPosition;
            impactHit.transform.rotation = Quaternion.LookRotation(raycastHit.normal);
            StartCoroutine(WaitUtils.WaitToDo(hitImpactSO.LifeTime, () =>
            {
                impactHit.ClearData();
                _impactHitPool.Store(impactHit);
            }));
        }
    }
}
