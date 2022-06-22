using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{
    public GameObject selectedHero;
    public bool heroPlayer;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit, Mathf.Infinity))
            {
                var targetable = hit.collider.GetComponent<Targetable>();
                var heroCombat = selectedHero.GetComponent<HeroCombat>();
                if (targetable is null)
                {
                    heroCombat.enemy = null;
                }
                else
                {
                    if (targetable.enemyType == Targetable.EnemyType.PetitDragon)
                    {
                        heroCombat.enemy = targetable.gameObject;
                    }
                }
            }
        }
    }
}