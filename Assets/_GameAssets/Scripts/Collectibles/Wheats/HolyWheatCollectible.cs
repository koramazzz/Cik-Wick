using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _forceIncreaseAmount;
    [SerializeField] private float _resetBoostDuration;

    public void CollectHolyWheat()
    {
        _playerController.SetJumpForce(_forceIncreaseAmount, _resetBoostDuration);
        Destroy(gameObject);
    }
}
