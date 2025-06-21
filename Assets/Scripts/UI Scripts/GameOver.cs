using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject scoreText;

    private bool shipIsDead = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (ShipStats.Instance.GetCurrentHealth() <= 0 && !shipIsDead)
        {
            shipIsDead = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            transform.GetComponent<UIController>().enabled = false;
            GameObject mainShip = GameObject.FindGameObjectWithTag("Player");
            if (mainShip != null)
            {
                mainShip.GetComponent<Animator>().enabled = true;
                mainShip.GetComponent<ShipFollowMouse>().enabled = false;
                mainShip.GetComponent<BulletShooter>().enabled = false;
            }
            StartCoroutine(ShowGameOverPanel(mainShip));
        }
    }

    IEnumerator ShowGameOverPanel(GameObject ship)
    {
        ship.GetComponent<Animator>().SetBool("Explosion", true);
        yield return new WaitForSeconds(0.3f);
        ship.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        gameOverPanel.SetActive(true);
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + GameManager.Instance.GetCurrentScore().ToString();
    }
}
