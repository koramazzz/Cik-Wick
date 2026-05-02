using UnityEngine;

public class PlayerParticleCollector : MonoBehaviour
{
    [SerializeField] private Transform _playerVisualTransform;

    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _playerRigidbody = GetComponentInParent<Rigidbody>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(_playerRigidbody, _playerVisualTransform);
        }
    }
}
