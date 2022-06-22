using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private float rotateSpeedMovement = 0.1f;
    public NavMeshAgent agent;
    public float rotateVelocity;
    private Camera _camera;

    private HeroCombat _heroCombat;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _camera = Camera.main;
        _heroCombat = GetComponent<HeroCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_heroCombat.enemy != null)
        {
            if (_heroCombat.enemy.GetComponent<HeroCombat>() != null)
            {
                if (_heroCombat.enemy.GetComponent<HeroCombat>().isHeroAlive)
                {
                    _heroCombat.enemy = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    // MOVEMENT
                    agent.SetDestination(hit.point);
                    _heroCombat.enemy = null;
                    agent.stoppingDistance = 0;

                    // ROTATION
                    var rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                    var rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        rotationToLookAt.eulerAngles.y,
                        ref rotateVelocity,
                        rotateSpeedMovement * (Time.deltaTime * 5));
                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                }
            }
        }
    }
}