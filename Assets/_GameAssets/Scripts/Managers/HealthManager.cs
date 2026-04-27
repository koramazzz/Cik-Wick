using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;
            // TODO: ANIMATE DAMAGE

            if (_currentHealth <= 0)
            {
                //TODO: DEATH
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
