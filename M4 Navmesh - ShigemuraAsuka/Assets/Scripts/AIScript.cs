﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour 
{
    public float trackDistance = 30.0f;
    public float stoppingDistance = 3.0f;

    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 target;
    private void Start() 
	{
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent.stoppingDistance = stoppingDistance;
    }

    private void Update() 
    {
        if (player == null) return;

        if (Vector3.Distance(player.transform.position, transform.position) <= trackDistance) {
            target = player.transform.position;
        }

        agent.SetDestination(target);
    }
}