using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _forceIncreaseAmount;
    [SerializeField] private float _resetBoostDuration;

    public void Collect()
    {
        _playerController.SetJumpForce(_forceIncreaseAmount, _resetBoostDuration);
        Destroy(gameObject);
    }
}
