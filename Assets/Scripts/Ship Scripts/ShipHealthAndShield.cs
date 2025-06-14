using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthAndShield : MonoBehaviour
{
    public HealthBar healthBar;
    public ShieldBar shieldBar;

    void Start()
    {
        if (ShipStats.Instance.GetCurrentHealth() <=0 || ShipStats.Instance.GetCurrentHealth() > ShipStats.Instance.GetMaxHealth())
        {
            ShipStats.Instance.SetCurrentHealth(ShipStats.Instance.GetMaxHealth());
        }
        if (ShipStats.Instance.GetCurrentShield() <= 0 || ShipStats.Instance.GetCurrentShield() > ShipStats.Instance.GetMaxShield())
        {
            ShipStats.Instance.SetCurrentShield(ShipStats.Instance.GetMaxShield());
        }

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(ShipStats.Instance.GetMaxHealth());
            healthBar.SetHealth(ShipStats.Instance.GetCurrentHealth());
        }
        if (shieldBar != null)
        {
            shieldBar.SetMaxShield(ShipStats.Instance.GetMaxShield());
            shieldBar.SetShield(ShipStats.Instance.GetCurrentShield());
        }
    }

    void Update()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(ShipStats.Instance.GetCurrentHealth());
        }
        if (shieldBar != null)
        {
            shieldBar.SetShield(ShipStats.Instance.GetCurrentShield());
        }
    }
}
