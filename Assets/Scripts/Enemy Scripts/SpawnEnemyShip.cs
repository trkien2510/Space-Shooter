using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyShip : MonoBehaviour
{
    public List<Transform> waypointGroup1;
    public List<Transform> waypointGroup2;
    public List<Transform> waypointGroup3;

    public float spawnInterval = 5f;
    public float spawnDelay = 1f;

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
                spawnInterval = 5f + selectedPath.Count * 0.5f;
                Vector3 spawnOrigin = selectedPath[0].position;

                for (int i = 0; i < 5; i++)
                {
                    GameObject enemy = ObjectPooler.Instance.SpawnObject("EnemyShip", spawnOrigin, Quaternion.identity);

                    if (enemy != null)
                    {
                        EnemyShip enemyScript = enemy.GetComponent<EnemyShip>();
                        if (enemyScript != null)
                        {
                            enemyScript.InitializeEnemyShip(new List<Transform>(selectedPath));
                        }
                    }

                    yield return new WaitForSeconds(spawnDelay);
                }
            }
            else
            {
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

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private List<Transform> GetRandomWaypoint()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0: return waypointGroup1;
            case 1: return waypointGroup2;
            case 2: return waypointGroup3;
            default: return null;
        }
    }
}
