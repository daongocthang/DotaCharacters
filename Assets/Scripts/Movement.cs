using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private float rotateSpeedMovement = 0.1f;
    private NavMeshAgent _agent;
    private float _rotateVelocity;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit, Mathf.Infinity))
            {
                // MOVEMENT
                _agent.SetDestination(hit.point);

                // ROTATION
                var rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                var rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref _rotateVelocity,
                    rotateSpeedMovement * (Time.deltaTime * 5));
                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
        }
    }
}