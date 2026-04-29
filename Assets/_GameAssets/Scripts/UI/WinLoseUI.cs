using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private GameObject _blackBackgroundObject;
    [SerializeField] private GameObject _winPopupObject;
    [SerializeField] private GameObject _losePopupObject;

    [Header("Settings")]
    [SerializeField] private float _animationDuration;

    private Image _blackBackgroundImage;
    private RectTransform _winPopupRectTransform;
    private RectTransform _losePopupRectTransform;
    private WinPopup _winPopup;
    private LosePopup _losePopup;

    private void Awake()
    {
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _winPopupRectTransform = _winPopupObject.GetComponent<RectTransform>();
        _losePopupRectTransform = _losePopupObject.GetComponent<RectTransform>();

        _winPopup = _winPopupObject.GetComponent<WinPopup>();
        _losePopup = _losePopupObject.GetComponent<LosePopup>();
        
        _winPopupRectTransform.localScale = Vector3.zero;
        _losePopupRectTransform.localScale = Vector3.zero;
    }

    public void OnGameWin()
    {
        _blackBackgroundObject.SetActive(true);
        _winPopupObject.SetActive(true);
        _losePopupObject.SetActive(false);

        _winPopup.SetTimerText(_timerUI.GetFormattedTime());

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear).SetUpdate(true).SetLink(gameObject);
        _winPopupRectTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack).SetUpdate(true).SetLink(gameObject);
    }

    public void OnGameLose()
    {
        _blackBackgroundObject.SetActive(true);
        _winPopupObject.SetActive(false);
        _losePopupObject.SetActive(true);

        _losePopup.SetTimerText(_timerUI.GetFormattedTime());

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear).SetUpdate(true).SetLink(gameObject);
        _losePopupRectTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack).SetUpdate(true).SetLink(gameObject);
    }
}
