using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Manager in game scene
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;

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
}
