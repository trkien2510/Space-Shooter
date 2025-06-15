using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Infomation : MonoBehaviour
{
    public Text txtScore;
    public Text txtHP;
    public Text txtShield;
    public Text txtPower;

    void Update()
    {
        if (txtScore != null && txtHP != null && txtShield != null && txtPower != null)
        {
            txtScore.text = "Score: " + GameManager.Instance.GetScore().ToString();
            txtHP.text = "HP: " + ShipStats.Instance.GetCurrentHealth().ToString() + "/" + ShipStats.Instance.GetMaxHealth().ToString();
            txtShield.text = "Shield: " + ShipStats.Instance.GetCurrentShield().ToString() + "/" + ShipStats.Instance.GetMaxShield().ToString();
            if (ShipStats.Instance.GetBulletCount() >= ShipStats.Instance.GetMaxBullets())
            {
                txtPower.text = "Power: Max";
            }
            else
            {
                txtPower.text = "Power: " + ShipStats.Instance.GetBulletCount().ToString();
            }
        }
    }
}
