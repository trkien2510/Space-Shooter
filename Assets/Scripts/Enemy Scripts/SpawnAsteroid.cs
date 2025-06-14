using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroid : MonoBehaviour
{
    private float baseSmallInterval = 30f;
    private float baseBigInterval = 100f;
    private float currentSmallInterval;
    private float currentBigInterval;

    private float spawnSmallTimer = 0f;
    private float spawnBigTimer = 0f;

    void Start()
    {
        currentSmallInterval = baseSmallInterval;
        currentBigInterval = baseBigInterval;
    }

    void Update()
    {
        spawnSmallTimer += Time.deltaTime;
        spawnBigTimer += Time.deltaTime;

        currentSmallInterval = Mathf.Max(5f, currentSmallInterval - Time.deltaTime);
        currentBigInterval = Mathf.Max(10f, currentBigInterval - Time.deltaTime);

        if (spawnSmallTimer >= currentSmallInterval)
        {
            SpawnAsteroidObject("SmallAsteroid");
            spawnSmallTimer = 0f;
        }
        if (spawnBigTimer >= currentBigInterval)
        {
            SpawnAsteroidObject("BigAsteroid");
            spawnBigTimer = 0f;
        }
    }

    private void SpawnAsteroidObject(string tag)
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), 7f);
        GameObject asteroid = ObjectPooler.Instance.SpawnObject(tag, spawnPosition, Quaternion.identity);
        if (asteroid != null)
        {
            asteroid.GetComponent<Asteroid>().InitializeAsteroid();
        }
    }
}
