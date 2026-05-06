using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;

    private void OnEnable()
    {
        BackgroundMusic.Instance.PlayBackgroundMusic(false);

        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

        AudioManager.Instance.Play(SoundType.LoseSound);
    }

    public void SetTimerText(string time)
    {
        _timerText.text = time;
    }

    private void OnTryAgainButtonClicked()
    {   
        AudioManager.Instance.Play(SoundType.TransitionSound);
        TransitionManager.Instance.LoadLevel(Consts.Scenes.GAME_SCENE);
    }

    private void OnMainMenuButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.TransitionSound);
        TransitionManager.Instance.LoadLevel(Consts.Scenes.MENU_SCENE);
    }
}