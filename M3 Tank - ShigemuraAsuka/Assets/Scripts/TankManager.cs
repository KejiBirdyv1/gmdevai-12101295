using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankManager : MonoBehaviour 
{
    public GameObject[] tanks;
    private GameObject currentTank;

    private void Start() 
    {
        SelectTank(0);
    }

    private void Update() 
    {
        if (currentTank == null) return;
    }

    public void TankGoToWaypoint(int waypointIndex) 
    {
        currentTank.GetComponent<FollowPath>().GoToWaypoint(waypointIndex);
    }

    public void SelectTank(int tankIndex) 
    {
        if (tankIndex >= 0 && tankIndex < tanks.Length) 
        {
            currentTank = tanks[tankIndex];
        }
    }
}
