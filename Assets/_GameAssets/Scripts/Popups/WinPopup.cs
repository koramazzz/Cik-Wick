using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _oneMoreButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;

    private void OnEnable()
    {   
        BackgroundMusic.Instance.PlayBackgroundMusic(false);

        _oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

        AudioManager.Instance.Play(SoundType.WinSound);
    }

    public void SetTimerText(string time)
    {
        _timerText.text = time;
    }

    private void OnOneMoreButtonClicked()
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
