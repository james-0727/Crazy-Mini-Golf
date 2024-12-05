using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Level UI Item
/// </summary>
public class LevelUIItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameObject _lockedObject;
    [SerializeField] private Button _btn;

    public Button Button => _btn;

    public void SetDetails(int level, bool isLocked)
    {
        _levelText.text = $"Level {level}";
        _btn.interactable = !isLocked;
        _lockedObject.SetActive(isLocked);
    }
}
