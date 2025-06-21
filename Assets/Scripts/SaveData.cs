using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<int> levelScore = new List<int>(new int[12]);
    public List<bool> planetsVisited = new List<bool> { true, false, false, false };
    public List<bool> starsVisited = new List<bool> { false, false, false, false };
    public List<bool> galaxiesVisited = new List<bool> { false, false, false, false };
}
