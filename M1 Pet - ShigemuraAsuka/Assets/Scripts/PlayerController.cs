using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 3;
    public float rotationSpeed = 6;

    private Vector3 targetVector = new Vector3();

    void LateUpdate() 
    {
        // Mouse Look
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
            targetVector = hit.point;
        }

        Vector3 targetDirection = targetVector - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * rotationSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection, new Vector3(0, 1, 0));

        // Movement
        float movement = movementSpeed * Time.deltaTime;
        transform.position += transform.forward * Input.GetAxis("Vertical") * movement;
        transform.position += transform.right * Input.GetAxis("Horizontal") * movement;
    }
}
