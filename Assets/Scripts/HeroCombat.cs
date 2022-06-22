using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCombat : MonoBehaviour
{
    public enum HeroAttackType
    {
        Melee,
        Ranged
    };

    public HeroAttackType attackType;

    public GameObject enemy;
    public float attackRange;
    public float rotateSpeedForAttack;

    private Movement _movement;
    private Stats _stats;
    private Animator _animator;

    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;

    [Header("Ranged Variable")] public bool performRangedAttack = true;

    public GameObject prefabProjectile;
    public Transform projectileSpawnPos;

    private Vector3 _enemyPos;

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
        _stats = GetComponent<Stats>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) > attackRange)
            {
                _enemyPos = enemy.transform.position;
                _movement.agent.SetDestination(_enemyPos);
                _movement.agent.stoppingDistance = attackRange;
            }
            else
            {
                // MELEE CHARACTER
                if (attackType == HeroAttackType.Melee)
                {
                    LookAtEnemy();
                    
                    if (performMeleeAttack)
                    {
                        Debug.Log("Attacking to enemy");
                        // Start Coroutine To Attack
                        StartCoroutine(MeleeAttackInterval());
                    }
                }

                // RANGED CHARACTER
                if (attackType == HeroAttackType.Ranged)
                {
                    LookAtEnemy();

                    if (performRangedAttack)
                    {
                        Debug.Log("Attacking to enemy");
                        // Start Coroutine To Attack
                        StartCoroutine(RangedAttackInterval());
                    }
                }
            }
        }
    }
    private void LookAtEnemy()
    {
        // ROTATION
        var rotationToLookAt = Quaternion.LookRotation(_enemyPos - transform.position);
        var rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y,
            ref _movement.rotateVelocity,
            rotateSpeedForAttack * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

    private IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;
        _animator.SetBool("BasicAttack", true);

        yield return new WaitForSeconds(_stats.attackTime / ((100 + _stats.attackTime) * 0.01f));

        if (enemy is null)
        {
            _animator.SetBool("BasicAttack", false);
            performMeleeAttack = true;
        }
    }
    private IEnumerator RangedAttackInterval()
    {
        performRangedAttack = false;
        _animator.SetBool("BasicAttack", true);

        yield return new WaitForSeconds(_stats.attackTime / ((100 + _stats.attackTime) * 0.01f));

        if (enemy is null)
        {
            _animator.SetBool("BasicAttack", false);
            performRangedAttack = true;
        }
    }

    public void MeleeAttack()
    {
        if (enemy != null)
        {
            if (enemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.PetitDragon)
            {
                enemy.GetComponent<Stats>().health -= _stats.attackDmg;
            }

            performMeleeAttack = true;
        }
    }
    public void RangedAttack()
    {
        if (enemy != null)
        {
            if (enemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.PetitDragon)
            {
                SpawnProjectile(Targetable.EnemyType.PetitDragon,enemy);
            }

            performRangedAttack = true;
        }
    }

    private void SpawnProjectile(Targetable.EnemyType enemyType, GameObject target)
    {
        float dmg = _stats.attackDmg;
        Instantiate(prefabProjectile, projectileSpawnPos.transform.position, Quaternion.identity);

        if (enemyType == Targetable.EnemyType.PetitDragon)
        {
            var proj= prefabProjectile.GetComponent<Projectile>();
            proj.target = target;
            proj.targetType = enemyType;
            proj.targetSet = true;
        }
    }
}