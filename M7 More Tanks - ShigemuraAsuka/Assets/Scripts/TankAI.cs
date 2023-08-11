using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : Tank
{
    private Animator anim;
    private GameObject player;
    private Transform playerTransform;

    public float wayPointAccuracy = 3.0f;
    public GameObject[] wayPoints;
    public int currentWaypoint;
    public Transform curWayTransform;

    private void Start()
    {
        InitializeComponents();
        InitializeWaypoints();
    }

    private void InitializeComponents()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        playerTransform = player.transform;
    }

    private void InitializeWaypoints()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("waypoint");
        currentWaypoint = 0;
    }

    private void Update()
    {
        UpdateDistanceToPlayer();
        UpdateHealthPercentage();
    }

    private void UpdateDistanceToPlayer()
    {
        if (player == null)
        {
            anim.SetFloat("distance", 9999999);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        anim.SetFloat("distance", distanceToPlayer);
    }

    private void UpdateHealthPercentage()
    {
        anim.SetFloat("hpPercent", GetHealthPercentage());
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
