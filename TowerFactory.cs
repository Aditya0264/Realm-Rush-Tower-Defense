using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int TowerLimit = 5;
    [SerializeField] Transform TowerParentTransform;
    Queue<Tower> towerQueue = new Queue<Tower>(); 

      

    public void AddTower(Waypoint basewaypoint)
    {
        int numTowers = towerQueue.Count;

        if (numTowers < TowerLimit)
        {
            InstantiateNewTowers(basewaypoint);
        }
        else
        {
            existingTower(basewaypoint);
        }
    }

    
    private void InstantiateNewTowers(Waypoint basewaypoint)
    {
        var newTower=Instantiate(towerPrefab, basewaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = TowerParentTransform;
        basewaypoint.isPlaceable = false;
        
        
        newTower.baseWaypoint = basewaypoint;
        basewaypoint.isPlaceable = false;

        towerQueue.Enqueue(newTower);
    }


    private void existingTower(Waypoint newbaseWaypoint)
    {

        var oldTower = towerQueue.Dequeue();

        oldTower.baseWaypoint.isPlaceable = true;
        newbaseWaypoint.isPlaceable = false;

        oldTower.baseWaypoint = newbaseWaypoint;
        oldTower.transform.position = newbaseWaypoint.transform.position;

        towerQueue.Enqueue(oldTower);
    }


}