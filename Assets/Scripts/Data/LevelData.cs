using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Level Data
/// </summary>
[CreateAssetMenu(fileName = "Level Data", menuName = "Game/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    [SerializeField] private int _level;
    [SerializeField] private Sprite _thumbnail;
    [SerializeField] private GameObject _trackPrefab;

    public int Level => _level;
    public GameObject TrackPrefab => _trackPrefab;

    private static List<LevelData> card_list = new List<LevelData>();

    public static void LoadAll()
    {
        card_list = Resources.LoadAll<LevelData>("Levels").ToList();
    }

    public static List<LevelData> GetAll()
    {
        return card_list;
    }
}
