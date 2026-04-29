using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;

    private void Awake()
    {
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    public void SetTimerText(string time)
    {
        _timerText.text = time;
    }

    private void OnTryAgainButtonClicked()
    {
        SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);
    }

    private void OnMainMenuButtonClicked()
    {
    }
}