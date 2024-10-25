using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;    
        [SerializeField] private Transform _enemyHolder;    
        [SerializeField] private float _spawnRadius = 20f;      
        [SerializeField] private float _spawnInterval = 5f;    
        [SerializeField] private int _maxEnemies = 10;         
        [SerializeField] private LayerMask _groundLayer;
        private int _currentEnemyCount = 0;
        private bool _isRunning;
        private List<Enemy> _enemies;
        private void Start()
        {
            _isRunning = true;
            _enemies = new List<Enemy>();
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            if (!Player.Instance)
            {
                foreach (var enemy in _enemies)
                {
                    enemy.Stop();
                }
                yield break;
            }
            
            while (_isRunning)
            {
                if (_currentEnemyCount < _maxEnemies)
                {
                    yield return new WaitForSeconds(_spawnInterval);
                    SpawnEnemy();
                }
                yield return null;
            }
        }

        private void SpawnEnemy()
        {
            Vector3 spawnPosition = GetRandomPositionInRadius();

            if (Physics.Raycast(spawnPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                var enemy = Instantiate(_enemyPrefab, hit.point, Quaternion.identity, _enemyHolder);
                _currentEnemyCount++;
                _enemies.Add(enemy);
                enemy.GetComponent<Enemy>().OnDeath += () =>
                {
                    _currentEnemyCount--;
                    _enemies.Remove(enemy);
                };
            }
        }
        
        private Vector3 GetRandomPositionInRadius()
        {
            Vector3 randomDirection = Random.insideUnitSphere * _spawnRadius;
            randomDirection += transform.position;

            return randomDirection;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        }
    }
}