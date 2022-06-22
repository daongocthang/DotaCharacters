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
                var enemyPos = enemy.transform.position;
                _movement.agent.SetDestination(enemyPos);
                _movement.agent.stoppingDistance = attackRange;

                // ROTATION
                var rotationToLookAt = Quaternion.LookRotation(enemyPos - transform.position);
                var rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref _movement.rotateVelocity,
                    rotateSpeedForAttack * (Time.deltaTime * 5));
                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            else
            {
                if (attackType == HeroAttackType.Melee)
                {
                    if (performMeleeAttack)
                    {
                        Debug.Log("Attacking to enemy");
                        // Start Coroutine To Attack
                        StartCoroutine(MeleeAttackInterval());    
                    }
                }
            }
        }
    }

    private IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;
        _animator.SetBool("BasicAttack", true);

        yield return new WaitForSeconds(_stats.attackTime / ((100 + _stats.attackTime) * 0.01f));

        if (enemy is null)
        {
            _animator.SetBool("BasicAttack",false);
            performMeleeAttack = true;
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
}