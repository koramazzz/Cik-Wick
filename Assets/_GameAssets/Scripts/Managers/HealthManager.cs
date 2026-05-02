using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }
    public event Action OnPlayerDeath;

    [Header("References")]
    [SerializeField] private PlayerHealthUI _playerHealthUI;

    [Header("Settings")]
    [SerializeField, Min(1)] private int _maxHealth;

    private int _currentHealth;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;
            _playerHealthUI.AnimateDamage();
            
            if (_currentHealth <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += Mathf.Max(_currentHealth + healAmount, _maxHealth);
        }
    }
}
