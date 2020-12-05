using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0f,120f)]
    [SerializeField] float SecondsBetweenSpawns=2f;
    [SerializeField] EnemyMovement EnemyPrefab;
    [SerializeField] Transform EnemyParentTransform;
    [SerializeField] Text spawnedEnemies;
    [SerializeField] AudioClip spawnedEnemiesSfx;

    int score;
    
    void Start()
    {
        StartCoroutine(RepeadtlySpawnningEnemies());
        spawnedEnemies.text = score.ToString();
    }
    IEnumerator RepeadtlySpawnningEnemies()
    {
    while(true)
        {
            addScore();
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemiesSfx);
            var NewEnemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);   //creating Enemies.
            NewEnemy.transform.parent = EnemyParentTransform;
            print("Spawnning");
            yield return new WaitForSeconds(SecondsBetweenSpawns);
        }
    }

    private void addScore()
    {
        score++;
        spawnedEnemies.text = score.ToString();
    }
}
