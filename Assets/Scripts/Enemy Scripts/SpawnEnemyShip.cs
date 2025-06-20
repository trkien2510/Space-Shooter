using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyShip : MonoBehaviour
{
    [SerializeField] private List<GameObject> waypointGroup;

    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnDelay = 1f;

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

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private List<Transform> GetRandomWaypoint()
    {
        int rand = Random.Range(0, waypointGroup.Count);
        List<Transform> children = new List<Transform>();

        Transform parentTransform = waypointGroup[rand].transform;
        foreach (Transform child in parentTransform)
        {
            children.Add(child);
        }
        return children;
    }
}
