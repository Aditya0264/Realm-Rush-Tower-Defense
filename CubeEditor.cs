using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{ 
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        snapToGrid();
        updateLabel();
    }

    private void snapToGrid()
    {
        int gridSize = waypoint.getGridSize();
        transform.position = new Vector3(waypoint.getGridPos().x*gridSize,
            0f,
           waypoint.getGridPos().y*gridSize);
    }

    private void updateLabel()
    {
 
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = textMesh.text =waypoint.getGridPos().x  + "," + waypoint.getGridPos().y ;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
