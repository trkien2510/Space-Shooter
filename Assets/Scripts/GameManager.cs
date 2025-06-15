using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int score = 0;
    private int bestScore = 0;

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

    public int GetScore() => score;
    public void SetScore(int value) => score = Mathf.Max(value, 0);

    public int GetBestScore() => bestScore;
    public void SetBestScore(int score) => bestScore = Mathf.Max(score, 0);
}
