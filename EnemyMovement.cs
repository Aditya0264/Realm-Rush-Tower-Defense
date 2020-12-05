using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;
    [SerializeField]float movementPeriod=.5f;
    [SerializeField] ParticleSystem goalParticle;
 

    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        path = pathfinder.getPath();

        StartCoroutine(FollowPath(path));
    }
    IEnumerator FollowPath(List<Waypoint>path)
    
    {
        print("Starting Patrol...");
        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        selfDestruct();
    }

    private void selfDestruct()
    {
        var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);
        Destroy(gameObject);

    }

}
