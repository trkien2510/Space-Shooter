using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShooter : MonoBehaviour
{
    private float shootInterval;
    private float shootTimer = 0f;

    void Start()
    {
        shootInterval = Random.Range(3f, 7f);
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            FireBullets();

            shootInterval = Random.Range(3f, 7f);
        }
    }

    private void FireBullets()
    {
        GameObject bulletObj = ObjectPooler.Instance.SpawnObject("EnemyBullet", transform.position, Quaternion.Euler(0, 0, -90));
        if (bulletObj != null)
        {
            bulletObj.GetComponent<EnemyBullet>().InitializeBullet();
        }
    }
}
