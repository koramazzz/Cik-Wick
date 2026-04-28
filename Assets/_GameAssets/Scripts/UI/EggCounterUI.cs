using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _eggCounterText;

    [Header("Settings")]
    [SerializeField] private Color _eggCounterColor;
    [SerializeField] private float _colorChangeDuration;
    [SerializeField] private float _scaleDuration;
    [SerializeField] private Ease _scaleEase;


    private RectTransform _eggCounterRectTransform;

    private void Awake()
    {
        _eggCounterRectTransform = _eggCounterText.gameObject.GetComponent<RectTransform>();
    }

    public void SetEggCounterText(int currentEggCount, int maxEggCount)
    {
        _eggCounterText.text = currentEggCount.ToString() + "/" + maxEggCount.ToString();
    }

    public void SetEggCompleted()
    {
        _eggCounterText.DOColor(_eggCounterColor, _colorChangeDuration);
        _eggCounterRectTransform.DOScale(Vector3.one * 1.2f, _scaleDuration).SetEase(_scaleEase);
    }
}
