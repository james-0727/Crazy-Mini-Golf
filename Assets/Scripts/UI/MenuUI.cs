using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UI controller for main menu
/// </summary>
public class MenuUI : MonoBehaviour
{
    [SerializeField] private RectTransform _contentParent;
    [SerializeField] private LevelUIItem _item;

    private void Start()
    {
        LevelData.LoadAll();
        RefreshLevelView();
    }

    public void RefreshLevelView()
    {
        List<LevelData> levelDatas = LevelData.GetAll();
        int lastUnlockedLvl = PlayerPrefs.GetInt("Last Unlocked Level", 1);

        foreach (LevelData levelData in levelDatas)
        {
            LevelUIItem UIItem = Instantiate(_item, _contentParent); 
            UIItem.SetDetails(levelData.Level, levelData.Level <= lastUnlockedLvl);
            int level = levelData.Level;
            UIItem.Button.onClick.AddListener(() => OnLevelButtonClicked(level));
        }
    }

    public void OnLevelButtonClicked(int level)
    {
        PlayerPrefs.SetInt("Selected Level", level);
        SceneManager.LoadScene(1);
    }
}
