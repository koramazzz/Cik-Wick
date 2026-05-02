using UnityEngine;

public class FireDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] private float _force;
    [SerializeField, Min(1)] private int _damageAmount;

    public void GiveDamage(Rigidbody playerRigidbody, Transform playerVisualTransform)
    {        
        HealthManager.Instance.Damage(_damageAmount);

        playerRigidbody.AddForce(-playerVisualTransform.forward * _force, ForceMode.Impulse);
    }
}