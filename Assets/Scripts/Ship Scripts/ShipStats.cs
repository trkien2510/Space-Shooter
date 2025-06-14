using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    public static ShipStats Instance;

    [Header("Bullet Stats")]
    private int bulletCount;
    private int maxBullets = 17;
    private float bulletSpeed = 10f;
    private int bulletDamage = 5;

    [Header("Health and Shield")]
    private float currentHealth;
    private float maxHealth = 200f;
    private float currentShield;
    private float maxShield = 50f;

    [Header("Bullet Color")]
    private int randBulletColor;

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

    public int GetBulletCount() => bulletCount;
    public void SetBulletCount(int value) => bulletCount = Mathf.Clamp(value, 1, maxBullets);

    public int GetMaxBullets() => maxBullets;
    public void SetMaxBullets(int value) => maxBullets = value;

    public float GetBulletSpeed() => bulletSpeed;
    public void SetBulletSpeed(float value) => bulletSpeed = value;

    public int GetBulletDamage() => bulletDamage;
    public void SetBulletDamage(int value) => bulletDamage = value;

    public float GetCurrentHealth() => currentHealth;
    public void SetCurrentHealth(float value) => currentHealth = Mathf.Clamp(value, 0, maxHealth);

    public float GetMaxHealth() => maxHealth;
    public void SetMaxHealth(float value) => maxHealth = value;

    public float GetCurrentShield() => currentShield;
    public void SetCurrentShield(float value) => currentShield = Mathf.Clamp(value, 0, maxShield);

    public float GetMaxShield() => maxShield;
    public void SetMaxShield(float value) => maxShield = value;

    public int GetRandBulletColor() => randBulletColor;
    public void SetRandBulletColor(int value) => randBulletColor = Mathf.Clamp(value, 0, 5);
}
