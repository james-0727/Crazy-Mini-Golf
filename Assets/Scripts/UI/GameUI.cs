using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI controller
/// </summary>
public class GameUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public Slider PowerSlider => _slider;

    private static GameUI _singleton;

    private void Awake()
    {
        _singleton = this;
    }

    public static GameUI Get()
    {
        return _singleton;
    }
}
