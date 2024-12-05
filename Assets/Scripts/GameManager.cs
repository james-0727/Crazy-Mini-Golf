using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Manager in game scene
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;
    private int _currentScore;
    private float _startTime;
    private int _minScore = 10;
    private int _maxScore = 100;

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

        _startTime = Time.time;
    }

    public static GameManager Get()
    {
        return _singleton;
    }

    public void EndGame()
    {
        // calculate the score with time for now
        // the faster you finish, the more score point you will get
        _currentScore = Math.Clamp(_maxScore - (int)((Time.time - _startTime) / 2), _minScore, _maxScore);

        GameUI.Get().ShowEndGamePanel(_currentScore);
    }
}
