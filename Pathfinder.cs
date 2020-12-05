using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;
     bool isrunning = true;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    Waypoint searchCenter; //current search center
     private List<Waypoint>path = new List<Waypoint>();

     
    Vector2Int[] directions =
    {
         Vector2Int.up,
         Vector2Int.right,
         Vector2Int.down,
         Vector2Int.left

    };

    public List<Waypoint>getPath()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath()
    {
        loadBlocks();
   
        BreadthFirstSearch();
        createPath();
    }

    private void createPath()
    {
        SetAsPath(endWaypoint);
        Waypoint previous = endWaypoint.exploredFrom;
        while(previous!=startWaypoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
            
            
        }
        SetAsPath(startWaypoint);
        path.Reverse();
    }
    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }
    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0 && isrunning)
        {
            searchCenter = queue.Dequeue();
            haltifendFound();
            exploreNeighbours(); // here
            searchCenter.isExplored = true;


        }
    }
    private void haltifendFound()
    {
        if (searchCenter == endWaypoint)
        {

            isrunning = false;

        }

    }
    private void exploreNeighbours()
    {
        if (!isrunning)
        {
            return;
        }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.getGridPos() + direction;
            if(grid.ContainsKey(neighbourCoordinates))
            {
                queueNewneighbour(neighbourCoordinates);
            }
           
        }
    }
    private void queueNewneighbour(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //do nothing
        }
        else
        {

            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;

        }
    }

   

    private void loadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.getGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("skipping overlapping block" + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
             
            }
        }
        
    }

   


  

    
   
   
}


