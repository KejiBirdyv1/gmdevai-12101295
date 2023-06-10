using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public float movementSpeed = 4;
    public float rotationSpeed = 3;

    public GameObject objectToFollow;
    public float distance = 2;

    private Vector3 targetDirection;

    void LateUpdate() 
    {
        Vector3 targetPosition = new Vector3(objectToFollow.transform.position.x, transform.position.y, objectToFollow.transform.position.z);
        targetDirection = targetPosition - transform.position;

        // Look Rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);

        if (targetDirection.magnitude > distance) 
        {
        // Movement Lerp
            transform.position = Vector3.Lerp(transform.position, targetPosition,Time.deltaTime * (movementSpeed / 2));
        }
    }
}
