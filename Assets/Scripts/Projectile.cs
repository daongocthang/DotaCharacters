using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public GameObject target;

    public bool targetSet;
    public Targetable.EnemyType targetType;
    public float velocity = 5.0f;
    public bool stopProjectile;

    private void Update()
    {
        if (target)
        {
            if (target is null)
            {
                Destroy(gameObject);
            }

            transform.position =
                Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);

            if (!stopProjectile)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
                {
                    if (targetType == Targetable.EnemyType.PetitDragon)
                    {
                        target.GetComponent<Stats>().health -= damage;
                        stopProjectile = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}