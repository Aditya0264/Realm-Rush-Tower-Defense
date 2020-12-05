using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Paramaters or Member variables or Instance variable
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 20f;
    [SerializeField] ParticleSystem projectileParticle;

    //State
    Transform targetEnemy;

    public Waypoint baseWaypoint;

    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            fireAtEnemy();
        }
        else
        {
            shootAtEnemy(false);
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) 
        { 
            return;
        }
        Transform closetEnemy = sceneEnemies[0].transform;

        foreach(EnemyDamage testEnemy in sceneEnemies)
        {
            closetEnemy = getClosest(closetEnemy, testEnemy.transform);
        }
        targetEnemy = closetEnemy;
    }


    private Transform getClosest(Transform transformA,Transform transformB)
    {
        var disToA = Vector3.Distance(transform.position, transformA.position);
        var disToB = Vector3.Distance(transform.position, transformB.position);

        if(disToA<disToB)
        {
            return transformA;
        }
        return transformB;
    }




    private void fireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange)
        {
            shootAtEnemy(true);
        }
        else
        {
            shootAtEnemy(false);
        }
        
    }
    private void shootAtEnemy(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
