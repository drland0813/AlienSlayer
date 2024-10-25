using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace drland.AlienSlayer
{
    public class AOEDamage : MonoBehaviour
    {
        [SerializeField] private float _explosionForce;
        [SerializeField] private ParticleSystem _explosionParticleSystem;
        [SerializeField] private float _explosionRadius = 5f;
        [SerializeField] private int _damage;

        private void Start()
        {
            _explosionParticleSystem.Play();
            Explode();
        }

        private void Explode()
        {
            AudioManager.Instance.PlaySFXAtPosition(SFXName.GrenadeExplosion.ToString(), transform.position);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

            foreach (Collider hitCollider in hitColliders)
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Vector3 explosionDirection = (hitCollider.transform.position - transform.position).normalized;

                    Vector3 randomDirection = explosionDirection + new Vector3(
                        Random.Range(-0.5f, 0.5f), 
                        Random.Range(0.1f, 1f),
                        Random.Range(-0.5f, 0.5f)
                    );
                    enemy.HealthComponent.TakeDamage(_damage);
                    enemy.RagdollManager.ApplyForce(randomDirection, _explosionForce);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
    }
}
