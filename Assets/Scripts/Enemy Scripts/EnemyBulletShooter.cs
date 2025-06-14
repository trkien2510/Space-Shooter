using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShooter : MonoBehaviour
{
    private float shootInterval = 4f;
    private float shootTimer = 0f;
    private bool isShooting = false;

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval && !isShooting)
        {
            isShooting = true;
            shootTimer = 0f;
            FireBullets();
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
