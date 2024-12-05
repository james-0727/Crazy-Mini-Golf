using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// UI controller
/// </summary>
public class GameUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [Header("End Game Panel")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _endGamePanel;

    public Slider PowerSlider => _slider;

    private static GameUI _singleton;

    private void Awake()
    {
        _singleton = this;
    }

    public void ShowEndGamePanel(int score)
    {
        _scoreText.text = score.ToString();
        _endGamePanel.SetActive(true);
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public static GameUI Get()
    {
        return _singleton;
    }
}
