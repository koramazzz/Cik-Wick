using System;
using DG.Tweening;
using MaskTransitions;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _settingsPopupObject;
    [SerializeField] private GameObject _blackBackgroundObject;

    [Header("Sprites")]
    [SerializeField] private Sprite _musicActiveSprite;
    [SerializeField] private Sprite _musicInactiveSprite;
    [SerializeField] private Sprite _soundsActiveSprite;
    [SerializeField] private Sprite _soundsInactiveSprite;

    [Header("Buttons")]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundsButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    [Header("Settings")]
    [SerializeField] private float _animationDuration;

    private Image _blackBackgroundImage;
    private bool _isMusicActive = true;
    private bool _isSoundsActive = true;

    private void Awake()
    {
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _settingsPopupObject.transform.localScale = Vector3.zero;

        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        _musicButton.onClick.AddListener(OnMusicButtonClicked);
        _soundsButton.onClick.AddListener(OnSoundsButtonClicked);
    }

    private void OnSettingsButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        GameManager.Instance.ChangeGameState(GameState.Pause);

        _blackBackgroundObject.SetActive(true);
        _settingsPopupObject.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear).SetUpdate(true).SetLink(gameObject);
        _settingsPopupObject.transform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack).SetUpdate(true).SetLink(gameObject);

    }

    private void OnResumeButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        GameManager.Instance.ChangeGameState(GameState.Resume);

        _blackBackgroundImage.DOFade(0f, _animationDuration).SetEase(Ease.Linear).SetUpdate(true).SetLink(gameObject);
        _settingsPopupObject.transform.DOScale(0f, _animationDuration).SetEase(Ease.OutExpo).SetUpdate(true).SetLink(gameObject).OnComplete(() =>
        {
            _blackBackgroundObject.SetActive(false);
            _settingsPopupObject.SetActive(false);
        });
    }

    private void OnMainMenuButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.TransitionSound);
        TransitionManager.Instance.LoadLevel(Consts.Scenes.MENU_SCENE);
    }

    private void OnMusicButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _isMusicActive = !_isMusicActive;
        _musicButton.image.sprite = _isMusicActive ? _musicActiveSprite : _musicInactiveSprite;
        BackgroundMusic.Instance.PlayBackgroundMusic(_isMusicActive);
    }

    private void OnSoundsButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _isSoundsActive = !_isSoundsActive;
        _soundsButton.image.sprite = _isSoundsActive ? _soundsActiveSprite : _soundsInactiveSprite;
        AudioManager.Instance.SetSoundEffectsMute(!_isSoundsActive);
    }
}
