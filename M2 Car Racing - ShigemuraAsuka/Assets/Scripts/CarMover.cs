using UnityEngine;
using UnityStandardAssets.Utility;

public class CarMover : MonoBehaviour
{
    public Vector3 centerOfMass = new Vector3(0, 0, 0);
    public float acceleration = 3500.0f;
    public float maxVelocity = 20.0f;
    public float steeringAngle = 50.0f;
    public float brakeAngle = 0.5f;
    public float deceleration = 1000.0f;

    public bool fourWheelDrive = false;
    public float distanceToNextWaypoint = 10.0f;

    private WaypointCircuit circuit;
    private int currentWaypointIndex;
    private Rigidbody rb;

    [SerializeField] private WheelCollider[] steeringWheels = null;
    [SerializeField] private WheelCollider[] rearWheels = null;

    public float brakeCollision = 5.0f;
    [SerializeField] private Transform windshield = null;

    private Vector3 relativePos = new Vector3();
    private float forwardAngle = 0.0f;
    private float defaultdeceleration = 0.0f;

    private void Start()
    {
        circuit = GameObject.FindGameObjectWithTag("Track").GetComponent<WaypointCircuit>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
        defaultdeceleration = deceleration;
    }

    private void LateUpdate()
    {
        // Waypoint advancer
        if (circuit.Waypoints.Length <= 0) return;
        GameObject currentWaypoint = circuit.Waypoints[currentWaypointIndex].gameObject;
        Transform lookAhead = currentWaypoint.transform;
        Vector3 lookAt = new Vector3(lookAhead.position.x, transform.position.y, lookAhead.position.z);
        Vector3 direction = transform.position - lookAt;
        if (direction.magnitude < distanceToNextWaypoint)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= circuit.Waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        // Get angle towards waypoint
        relativePos = lookAhead.transform.position - transform.position;
        forwardAngle = Vector3.Angle(relativePos, transform.forward) / 180.0f;

        // Braking      
        if (forwardAngle > brakeAngle || IsAboutToCollide())
        {
            deceleration = defaultdeceleration;
        }
        else
        {
            deceleration = 0;
        }

        // Steer left or right
        if (Vector3.Cross(transform.forward, relativePos).y < 0) forwardAngle *= -1;
        else Mathf.Abs(forwardAngle);
    }

    private void FixedUpdate()
    {
        // Speed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        for (int i = 0; i < rearWheels.Length; i++)
        {
            rearWheels[i].motorTorque = acceleration;
            rearWheels[i].brakeTorque = deceleration;
        }

        if (fourWheelDrive)
        {
            for (int i = 0; i < steeringWheels.Length; i++)
            {
                steeringWheels[i].motorTorque = acceleration;
                steeringWheels[i].brakeTorque = deceleration;
            }
        }

        // Steering
        float angle = steeringAngle * forwardAngle;
        for (int i = 0; i < steeringWheels.Length; i++)
        {
            steeringWheels[i].steerAngle = angle;
        }
    }

    private bool IsAboutToCollide()
    {
        return Physics.Raycast(windshield.position, transform.forward, brakeCollision);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position - centerOfMass, 0.2f);
    }
}
