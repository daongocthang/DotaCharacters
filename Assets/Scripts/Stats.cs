using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float attackDmg;
    public float attackSpeed;
    public float attackTime;

    private HeroCombat _heroCombat;

    private GameObject _player;
    public float expValue;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _heroCombat = _player.GetComponent<HeroCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            _heroCombat.enemy = null;
            _heroCombat.performMeleeAttack = false;

            // Give Exp
            _player.GetComponent<LevelUpStats>().SetExperience(expValue);
        }
    }
}