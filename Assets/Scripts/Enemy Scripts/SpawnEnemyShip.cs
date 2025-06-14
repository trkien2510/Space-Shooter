using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyShip : MonoBehaviour
{
    public List<Transform> waypoint1;
    public List<Transform> waypoint2;
    public List<Transform> waypoint3;

    public float spawnInterval = 5f;
    public float enemySpacing = 1.5f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            List<Transform> selectedPath = GetRandomWaypoint();

            if (selectedPath != null && selectedPath.Count > 0)
            {
                // Spawn 5 ships, each spaced enemySpacing units apart on X, centered at the waypoint start
                Vector3 spawnOrigin = selectedPath[0].position;
                float totalSpacing = enemySpacing * (5 - 1);
                float startX = spawnOrigin.x - totalSpacing / 2f;

                for (int i = 0; i < 5; i++)
                {
                    Vector3 pos = new Vector3(startX + i * enemySpacing, spawnOrigin.y, spawnOrigin.z);
                    GameObject enemy = ObjectPooler.Instance.SpawnObject("EnemyShip", pos, Quaternion.identity);

                    if (enemy != null)
                    {
                        EnemyShip enemyScript = enemy.GetComponent<EnemyShip>();
                        if (enemyScript != null)
                        {
                            enemyScript.InitializeEnemyShip(selectedPath);
                        }
                    }
                }
            }
            else
            {
                // No waypoint, spawn 1 ship at a random position
                Vector3 randomPos = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
                GameObject enemy = ObjectPooler.Instance.SpawnObject("EnemyShip", randomPos, Quaternion.identity);

                if (enemy != null)
                {
                    EnemyShip enemyScript = enemy.GetComponent<EnemyShip>();
                    if (enemyScript != null)
                    {
                        enemyScript.InitializeEnemyShip();
                    }
                }
            }

            // Wait 5 seconds before next spawn direction selection
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    List<Transform> GetRandomWaypoint()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0: return waypoint1;
            case 1: return waypoint2;
            case 2: return waypoint3;
            case 3: return null;
            default: return null;
        }
    }
}
