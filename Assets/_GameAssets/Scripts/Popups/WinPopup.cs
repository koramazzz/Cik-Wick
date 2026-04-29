using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _oneMoreButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;

    private void Awake()
    {
        _oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    public void SetTimerText(string time)
    {
        _timerText.text = time;
    }

    private void OnOneMoreButtonClicked()
    {
        SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);
    }

    private void OnMainMenuButtonClicked()
    {
    }
}
