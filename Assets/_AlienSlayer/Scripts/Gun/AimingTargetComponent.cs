using UnityEngine;

namespace drland.AlienSlayer
{
    public class AimingTargetComponent : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius = 10f; 
        [SerializeField] private float _rotationSpeed = 5f;    
        [SerializeField] private LayerMask _enemyLayer;    
        private Transform _currentTarget;                   

        private void Update()
        {
            DetectEnemies();  

            if (_currentTarget != null)
            {
                AimAtTarget();
            }
        }

        private void DetectEnemies()
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, _detectionRadius, _enemyLayer);

            if (enemiesInRange.Length > 0)
            {
                Transform closestEnemy = enemiesInRange[0].transform;
                float shortestDistance = Vector3.Distance(transform.position, closestEnemy.position);

                foreach (Collider enemy in enemiesInRange)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        closestEnemy = enemy.transform;
                        shortestDistance = distanceToEnemy;
                    }
                }

                _currentTarget = closestEnemy;
            }
            else
            {
                _currentTarget = null;
            }
        }

        private void AimAtTarget()
        {
            Vector3 directionToTarget = (_currentTarget.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
    }
}