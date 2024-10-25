using System.Collections;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;    
        [SerializeField] private Transform[] _spawnPoints;    
        [SerializeField] private float _spawnInterval = 5f;    
        [SerializeField] private int _maxEnemies = 10;         

        private int _currentEnemyCount = 0;              
        private float _timer = 0f;

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (_currentEnemyCount < _maxEnemies)
            {
                yield return new WaitForSeconds(_spawnInterval);
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            Transform randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            GameObject enemy = Instantiate(_enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

            _currentEnemyCount++;

            enemy.GetComponent<Enemy>().OnDeath += () => _currentEnemyCount--;
        }

        // Vẽ các điểm spawn trong Scene Editor để dễ quan sát
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            foreach (Transform spawnPoint in _spawnPoints)
            {
                Gizmos.DrawWireSphere(spawnPoint.position, 1f);
            }
        }
    }
}