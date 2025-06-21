using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SavaSystem
{
    private static string saveLocation = Path.Combine(Application.persistentDataPath, "SpaceShooterDate.json");

    public static void SaveGame()
    {
        SaveData gameData;
        if (File.Exists(saveLocation))
        {
            gameData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
        }
        else
        {
            gameData = new SaveData();
        }

        int levelIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (levelIndex >= 0 && levelIndex < gameData.levelScore.Count)
        {
            gameData.levelScore[levelIndex] = GameManager.Instance.GetCurrentScore();
        }
        gameData.planetsVisited = GameManager.Instance.GetPlanetsVisited();
        gameData.starsVisited = GameManager.Instance.GetStarsVisited();
        gameData.galaxiesVisited = GameManager.Instance.GetGalaxiesVisited();

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveLocation, json);
    }

    public static void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                GameManager.Instance.SetLevelScore(saveData.levelScore);
                GameManager.Instance.SetPlanetsVisited(saveData.planetsVisited);
                GameManager.Instance.SetStarsVisited(saveData.starsVisited);
                GameManager.Instance.SetGalaxiesVisited(saveData.galaxiesVisited);

                UpdateVisitedStatus();
            }
            else
            {
                UnityEngine.Events.UnityAction<Scene, LoadSceneMode> onSceneLoaded = null;
                onSceneLoaded = (scene, mode) =>
                {
                    GameManager.Instance.SetLevelScore(saveData.levelScore);
                    GameManager.Instance.SetPlanetsVisited(saveData.planetsVisited);
                    GameManager.Instance.SetStarsVisited(saveData.starsVisited);
                    GameManager.Instance.SetGalaxiesVisited(saveData.galaxiesVisited);

                    SceneManager.sceneLoaded -= onSceneLoaded;
                };
                SceneManager.sceneLoaded += onSceneLoaded;
            }
        }
        else
        {
            SaveGame();
        }
    }

    public static void DeleteSave()
    {
        if (File.Exists(saveLocation))
        {
            File.Delete(saveLocation);
        }
    }

    public static void UpdateVisitedStatus()
    {
        var levelScore = GameManager.Instance.GetLevelScore();
        var scoresToComplete = GameManager.Instance.GetScoresToCompleteLevel();

        var planets = GameManager.Instance.GetPlanetsVisited();
        for (int i = 0; i < 4; i++)
        {
            if ((float)levelScore[i] / scoresToComplete[i] >= 0.8f)
            {
                planets[i] = true;
                if (i + 1 < 4)
                    planets[i + 1] = true;
            }
        }
        GameManager.Instance.SetPlanetsVisited(planets);

        var stars = GameManager.Instance.GetStarsVisited();
        for (int i = 4; i < 8; i++)
        {
            if ((float)levelScore[i] / scoresToComplete[i] >= 0.8f)
            {
                stars[i - 4] = true;
                if (i + 1 < 8)
                    stars[i - 3] = true;
            }
        }
        GameManager.Instance.SetStarsVisited(stars);

        var galaxies = GameManager.Instance.GetGalaxiesVisited();
        for (int i = 8; i < 12; i++)
        {
            if ((float)levelScore[i] / scoresToComplete[i] >= 0.8f)
            {
                galaxies[i - 8] = true;
                if (i + 1 < 12)
                    galaxies[i - 7] = true;
            }
        }
        GameManager.Instance.SetGalaxiesVisited(galaxies);
    }
}
