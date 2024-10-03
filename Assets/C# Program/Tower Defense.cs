using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefense  : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float range = 1f;
    [SerializeField] private float fireRate = 1f; // Arrows per second
    [SerializeField] private string enemyTag = "Enemy";

    private float fireCountdown = 0f;
    private Transform target;

    void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else
        {
            if (!IsInRange(target))
            {
                target = null;
                return;
            }

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
    }

    bool IsInRange(Transform enemyTransform)
    {
        return Vector3.Distance(transform.position, enemyTransform.position) <= range;
    }

    void Shoot()
    {
        ProjectileArrow.Create(firePoint.position, target, arrowPrefab);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
