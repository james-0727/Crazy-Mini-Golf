using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Manager in game scene
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;
    private int _currentScore;

    private void Awake()
    {
        _singleton = this;
    }
    private void Start()
    {
        LoadSelectedLevel();
    }

    private void LoadSelectedLevel()
    {
        int selectedLevel = PlayerPrefs.GetInt("Selected Level", 1);

        List<LevelData> levelDatas = LevelData.GetAll();

        foreach(LevelData levelData in levelDatas)
        {
            if (levelData.Level == selectedLevel)
            {
                Instantiate(levelData.TrackPrefab);
                break;
            }
        }
    }

    public static GameManager Get()
    {
        return _singleton;
    }

    public void EndGame()
    {
        GameUI.Get().ShowEndGamePanel(_currentScore);
    }
}
