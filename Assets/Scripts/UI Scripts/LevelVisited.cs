using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelVisited : MonoBehaviour
{
    [SerializeField] private Image planet1;
    [SerializeField] private Image planet2;
    [SerializeField] private Image planet3;
    [SerializeField] private Image planet4;
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;
    [SerializeField] private Image star4;
    [SerializeField] private Image galaxy1;
    [SerializeField] private Image galaxy2;
    [SerializeField] private Image galaxy3;
    [SerializeField] private Image galaxy4;

    private void Start()
    {
        SavaSystem.LoadGame();
    }

    private void Update()
    {
        UpdateVisitedLevels();
        UpdatePercentComplete();
    }

    private void UpdatePercentComplete()
    {
        planet1.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[0] * 100:F0}%";
        planet2.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[1] * 100:F0}%";
        planet3.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[2] * 100:F0}%";
        planet4.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[3] * 100:F0}%";
        star1.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[4] * 100:F0}%";
        star2.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[5] * 100:F0}%";
        star3.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[6] * 100:F0}%";
        star4.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[7] * 100:F0}%";
        galaxy1.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[8] * 100:F0}%";
        galaxy2.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[9] * 100:F0}%";
        galaxy3.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[10] * 100:F0}%";
        galaxy4.gameObject.GetComponentInChildren<Text>().text = $"{PercentComplete()[11] * 100:F0}%";
    }

    private List<float> PercentComplete()
    {
        List<int> levelScore = GameManager.Instance.GetLevelScore();
        List<int> scoresToCompleteLevel = GameManager.Instance.GetScoresToCompleteLevel();
        List<float> percentComplete = new List<float>();
        for (int i = 0; i < levelScore.Count; i++)
        {
            float percent = (float)levelScore[i] / scoresToCompleteLevel[i];
            percentComplete.Add(Mathf.Min(percent, 1f));
        }
        return percentComplete;
    }

    private void UpdateVisitedLevels()
    {
        List<bool> planetsVisited = GameManager.Instance.GetPlanetsVisited();
        List<bool> starsVisited = GameManager.Instance.GetStarsVisited();
        List<bool> galaxiesVisited = GameManager.Instance.GetGalaxiesVisited();

        SetImageAlpha(planet1, planetsVisited[0]);
        SetImageAlpha(planet2, planetsVisited[1]);
        SetImageAlpha(planet3, planetsVisited[2]);
        SetImageAlpha(planet4, planetsVisited[3]);
        SetEnableComponent(planet1, planetsVisited[0]);
        SetEnableComponent(planet2, planetsVisited[1]);
        SetEnableComponent(planet3, planetsVisited[2]);
        SetEnableComponent(planet4, planetsVisited[3]);

        SetImageAlpha(star1, starsVisited[0]);
        SetImageAlpha(star2, starsVisited[1]);
        SetImageAlpha(star3, starsVisited[2]);
        SetImageAlpha(star4, starsVisited[3]);
        SetEnableComponent(star1, starsVisited[0]);
        SetEnableComponent(star2, starsVisited[1]);
        SetEnableComponent(star3, starsVisited[2]);
        SetEnableComponent(star4, starsVisited[3]);

        SetImageAlpha(galaxy1, galaxiesVisited[0]);
        SetImageAlpha(galaxy2, galaxiesVisited[1]);
        SetImageAlpha(galaxy3, galaxiesVisited[2]);
        SetImageAlpha(galaxy4, galaxiesVisited[3]);
        SetEnableComponent(galaxy1, galaxiesVisited[0]);
        SetEnableComponent(galaxy2, galaxiesVisited[1]);
        SetEnableComponent(galaxy3, galaxiesVisited[2]);
        SetEnableComponent(galaxy4, galaxiesVisited[3]);
    }

    private void SetImageAlpha(Image img, bool visited)
    {
        Color c = img.color;
        c.a = visited ? 1f : 0.3f;
        img.color = c;
    }

    private void SetEnableComponent(Image img, bool visited)
    {
        img.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = visited;
    }
}
