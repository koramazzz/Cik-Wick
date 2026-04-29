using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _playerHealthImages;
    
    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealthySprite;
    [SerializeField] private Sprite _playerUnhealthySprite;

    [Header("Settings")]
    [SerializeField] private float _damageAnimationDuration;
    [SerializeField] private Ease _destroyAnimationEase;
    [SerializeField] private Ease _recoverAnimationEase;

    private RectTransform[] _playerHealthTransforms;

    private void Awake()
    {
        _playerHealthTransforms = new RectTransform[_playerHealthImages.Length];

        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            _playerHealthTransforms[i] = _playerHealthImages[i].GetComponent<RectTransform>();
        }
    }

    private void AnimateDamageSprite(Image activeImage,RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0, _damageAnimationDuration).SetEase(_destroyAnimationEase).SetLink(gameObject).OnComplete(() =>
        {
            activeImage.sprite = _playerUnhealthySprite;
            activeImageTransform.DOScale(1f, _damageAnimationDuration).SetEase(_recoverAnimationEase).SetLink(gameObject);
        });
    }

    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i].sprite == _playerHealthySprite)
            {
                AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);
                break;
            }
        }
    }

    public void AnimateDamageForAll()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i].sprite == _playerHealthySprite)
            {
                AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);
            }
        }
    }
}
