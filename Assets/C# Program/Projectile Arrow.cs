using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArrow : MonoBehaviour
{
    public float speed = 1f;
    public float damage = 1f;
    public float lifetime = 5f;

    private Transform target;
    private Vector3 lastKnownPosition;

    public void Initialize(Transform target)
    {
        this.target = target;
        this.lastKnownPosition = target.position;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target != null)
        {
            lastKnownPosition = target.position;
        }

        Vector3 direction = (lastKnownPosition - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        // Rotate the arrow to face the direction it's moving
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Check if the arrow has reached the target position
        if (Vector3.Distance(transform.position, lastKnownPosition) < 0.1f)
        {
            if (target == null)
            {
                // Target was destroyed, so destroy the arrow
                Destroy(gameObject);
            }
            else
            {
                // We've reached the target, apply damage
                HitTarget();
            }
        }
    }

    void HitTarget()
    {
        DamageableEntity targetEntity = target.GetComponent<DamageableEntity>();
        if (targetEntity != null)
        {
            targetEntity.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    public static void Create(Vector3 position, Transform target, GameObject arrowPrefab)
    {
        GameObject arrowObject = Instantiate(arrowPrefab, position, Quaternion.identity);
        ProjectileArrow arrow = arrowObject.GetComponent<ProjectileArrow>();
        if (arrow != null)
        {
            arrow.Initialize(target);
        }
    }
}