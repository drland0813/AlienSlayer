using UnityEngine;

namespace drland.AlienSlayer
{
    public class AimingTargetComponent : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius = 10f; // Tầm bắn của súng
        [SerializeField] private float _rotationSpeed = 5f;    // Tốc độ xoay của súng
        [SerializeField] private LayerMask _enemyLayer;        // Layer của kẻ địch
        private Transform _currentTarget;                     // Mục tiêu hiện tại

        private void Update()
        {
            DetectEnemies();  // Kiểm tra xem có kẻ địch trong tầm không

            if (_currentTarget != null)
            {
                AimAtTarget(); // Ngắm vào kẻ địch nếu có mục tiêu
            }
        }

        private void DetectEnemies()
        {
            // Tìm tất cả các Collider của kẻ địch trong phạm vi detectionRadius
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, _detectionRadius, _enemyLayer);

            // Nếu tìm thấy kẻ địch, chọn kẻ địch gần nhất làm mục tiêu
            if (enemiesInRange.Length > 0)
            {
                Transform closestEnemy = enemiesInRange[0].transform;
                float shortestDistance = Vector3.Distance(transform.position, closestEnemy.position);

                // Tìm mục tiêu gần nhất
                foreach (Collider enemy in enemiesInRange)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        closestEnemy = enemy.transform;
                        shortestDistance = distanceToEnemy;
                    }
                }

                _currentTarget = closestEnemy; // Cập nhật mục tiêu
            }
            else
            {
                _currentTarget = null; // Không có mục tiêu
            }
        }

        private void AimAtTarget()
        {
            // Tính toán hướng từ súng đến kẻ địch
            Vector3 directionToTarget = (_currentTarget.position - transform.position).normalized;

            // Tính toán góc quay cần thiết để súng quay về phía kẻ địch
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

            // Xoay súng từ từ về hướng kẻ địch với tốc độ rotationSpeed
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }

        private void OnDrawGizmosSelected()
        {
            // Vẽ một hình cầu để hiển thị tầm phát hiện của súng trong Unity Editor
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
    }
}