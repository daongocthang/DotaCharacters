using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private const float MotionSmoothTime = .1f;

    private NavMeshAgent _agent;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var speed = _agent.velocity.magnitude / _agent.speed;
        anim.SetFloat("Speed", speed, MotionSmoothTime, Time.deltaTime);
    }
}