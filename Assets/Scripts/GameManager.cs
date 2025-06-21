using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int currentScore = 0;

    [Header("Scores to complete")]
    private List<int> scoresToCompleteLevel = new List<int>
    {
        400, // Score for Planet 1
        600, // Score for Planet 2
        800, // Score for Planet 3
        1000, // Score for Planet 4
        1500, // Score for Star 1
        2000, // Score for Star 2
        2500, // Score for Star 3
        3000, // Score for Star 4
        4000, // Score for Galaxy 1
        5000, // Score for Galaxy 2
        6000, // Score for Galaxy 3
        7000 // Score for Galaxy 4
    };

    private List<int> levelScore;
    private List<bool> planetsVisited;
    private List<bool> starsVisited;
    private List<bool> galaxiesVisited;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetCurrentScore() => currentScore;
    public void SetCurrentScore(int value) => currentScore = Mathf.Max(value, 0);

    public List<int> GetScoresToCompleteLevel() => scoresToCompleteLevel;

    public List<int> GetLevelScore()
    {
        if (levelScore == null)
        {
            levelScore = new List<int>(new int[scoresToCompleteLevel.Count]);
        }
        return levelScore;
    }
    public void SetLevelScore(List<int> levelScore)
    {
        if (levelScore != null && levelScore.Count == scoresToCompleteLevel.Count)
        {
            this.levelScore = levelScore;
        }
    }

    public List<bool> GetPlanetsVisited() => planetsVisited ??= new List<bool> { true, false, false, false };
    public void SetPlanetsVisited(List<bool> value)
    {
        if (value != null && value.Count == 4)
        {
            planetsVisited = value;
        }
    }

    public List<bool> GetStarsVisited() => starsVisited ??= new List<bool> { false, false, false, false };
    public void SetStarsVisited(List<bool> value)
    {
        if (value != null && value.Count == 4)
        {
            starsVisited = value;
        }
    }

    public List<bool> GetGalaxiesVisited() => galaxiesVisited ??= new List<bool> { false, false, false, false };
    public void SetGalaxiesVisited(List<bool> value)
    {
        if (value != null && value.Count == 4)
        {
            galaxiesVisited = value;
        }
    }
}
