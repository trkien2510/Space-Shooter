using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    private float shootInterval = 0.25f;
    private float shootTimer = 0f;

    private void Update()
    {
        shootTimer += Time.deltaTime;
        
        if (shootTimer >= shootInterval)
        {
            FireBullets();
            shootTimer = 0f;
        }
    }

    private void FireBullets()
    {
        int count = ShipStats.Instance.GetBulletCount();

        int large = Mathf.Min(count / 9, 1);
        count -= large * 9;

        int medium = Mathf.Min(count / 3, 2);
        count -= medium * 3;

        int small = Mathf.Min(count, 2);

        List<(string tag, int size)> bulletOrder = new List<(string, int)>();

        for (int i = 0; i < small / 2; i++)
            bulletOrder.Add(("SmallBullet", 1));

        for (int i = 0; i < medium / 2; i++)
            bulletOrder.Add(("MediumBullet", 2));

        if (large == 1)
            bulletOrder.Add(("LargeBullet", 3));

        for (int i = 0; i < medium - medium / 2; i++)
            bulletOrder.Add(("MediumBullet", 2));

        for (int i = 0; i < small - small / 2; i++)
            bulletOrder.Add(("SmallBullet", 1));

        float spacing = 0.2f;
        float totalSpacing = (bulletOrder.Count - 1) * spacing;
        float startX = transform.position.x - totalSpacing / 2f;

        for (int i = 0; i < bulletOrder.Count; i++)
        {
            Vector3 pos = new Vector3(startX + i * spacing, transform.position.y, 0);
            GameObject bulletObj = ObjectPooler.Instance.SpawnObject(bulletOrder[i].tag, pos, Quaternion.Euler(0, 0, 90));
            if (bulletObj != null)
            {
                bulletObj.GetComponent<Bullet>().InitializeBullet(bulletOrder[i].size);
            }
        }

        AudioManager.Instance.PlayBulletSFX();
    }

}
