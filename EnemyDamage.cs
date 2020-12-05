using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem HitParticlePrefab;
    [SerializeField] ParticleSystem DeathParticlePrefab;
    [SerializeField] AudioClip enemyHitSfx;
    [SerializeField] AudioClip enemyDeathSfx;
   


    AudioSource myAudioSource;
  
     private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
      
    }
    private void OnParticleCollision(GameObject other)
    {
        print("I am hit");
        processHit();
        if(hitPoints<=1)
        {
            killEnemy();
        }
    }

    private void killEnemy()
    {
        var vfx=Instantiate(DeathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);
        AudioSource.PlayClipAtPoint(enemyDeathSfx,Camera.main.transform.position);
        Destroy(gameObject);
      

        
    }

  

    void processHit()
    {
        hitPoints = hitPoints - 1;
        HitParticlePrefab.Play();
        myAudioSource.PlayOneShot(enemyHitSfx);
    }
    




}
