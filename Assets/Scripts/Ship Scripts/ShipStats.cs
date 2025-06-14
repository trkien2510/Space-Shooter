using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    public static ShipStats Instance;

    public int bulletCount;
    public int maxBullets = 17;
    public float bulletSpeed = 10f;
    public int bulletDamage = 5;
    public float currentHealth;
    public float maxHealth = 200;
    public float currentShield;
    public float maxShield = 50;

    public int randBulletColor;

    private void Awake()
    {
        Instance = this;
        bulletCount = 1;
        currentHealth = maxHealth;
        currentShield = maxShield;
        randBulletColor = Random.Range(0, 6);
    }
    void Start()
    {
        bulletCount = 1;
    }

    void Update()
    {
        if (bulletCount > 17)
        {
            bulletCount = 17;
        }
    }

    public void AddPower(string type)
    {
        if (type == "HPItem")
        {
            currentHealth += 20;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        if (type == "ArmorItem")
        {
            currentShield += 20;
            if (currentShield > maxShield)
            {
                currentShield = maxShield;
            }
        }
        if (type == "UpdateItem")
        {
            bulletCount += 1;
            if (bulletCount > maxBullets)
            {
                bulletCount = maxBullets;
            }
        }
    }
}
