using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class World 
{
    public static readonly World instance = new World();

    private static readonly GameObject[] hidingSpots;

    static World() 
    {
        hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }

    private World() { }

    public static World Instance 
    {
        get { return instance; }
    }
    
    public GameObject[] GetHidingSpots() 
    {
        return hidingSpots;
    }
}
