using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Infomation : MonoBehaviour
{
    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtScoreToComplete;
    [SerializeField] private Slider sliderScoreToComplete;

    [SerializeField] private Text txtHP;
    [SerializeField] private Text txtShield;
    [SerializeField] private Text txtPower;

    void Update()
    {
        txtScore.text = "Score: " + GameManager.Instance.GetCurrentScore().ToString();
        txtScoreToComplete.text = GameManager.Instance.GetScoresToCompleteLevel()[SceneManager.GetActiveScene().buildIndex - 1].ToString();
        sliderScoreToComplete.value = GameManager.Instance.GetCurrentScore();
        sliderScoreToComplete.maxValue = (float)GameManager.Instance.GetScoresToCompleteLevel()[SceneManager.GetActiveScene().buildIndex - 1];

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
