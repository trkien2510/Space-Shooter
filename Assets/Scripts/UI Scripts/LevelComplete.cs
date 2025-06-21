using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject scoreText;
    private int currentScore;
    private int currentLevel;

    void Awake()
    {
        currentScore = 0;
        currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        levelCompletePanel.SetActive(false);
    }

    void Update()
    {
        ShowLevelComplete();
        SetActiveLevel();
    }

    private void ShowLevelComplete()
    {
        if (GameManager.Instance != null)
        {
            currentScore = GameManager.Instance.GetCurrentScore();
        }

        if (currentScore >= GameManager.Instance.GetScoresToCompleteLevel()[currentLevel])
        {
            SavaSystem.SaveGame();
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

            SetActiveLevel();
            SavaSystem.SaveGame();
            StartCoroutine(LevelCompleteRoutin());
        }
    }

    IEnumerator LevelCompleteRoutin()
    {
        yield return new WaitForSeconds(2f);
        levelCompletePanel.SetActive(true);
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + currentScore;
    }

    private void SetActiveLevel()
    {
        int requiredScore = GameManager.Instance.GetScoresToCompleteLevel()[currentLevel];

        if ((float)GameManager.Instance.GetLevelScore()[currentLevel] / requiredScore >= 0.8f)
        {
            if (currentLevel < 4)
            {
                List<bool> planets = GameManager.Instance.GetPlanetsVisited();
                planets[currentLevel] = true;
                GameManager.Instance.SetPlanetsVisited(planets);
            }
            else if (currentLevel < 8)
            {
                List<bool> stars = GameManager.Instance.GetStarsVisited();
                stars[currentLevel - 4] = true;
                GameManager.Instance.SetStarsVisited(stars);
            }
            else if (currentLevel < 12)
            {
                List<bool> galaxies = GameManager.Instance.GetGalaxiesVisited();
                galaxies[currentLevel - 8] = true;
                GameManager.Instance.SetGalaxiesVisited(galaxies);
            }
        }
    }
}
