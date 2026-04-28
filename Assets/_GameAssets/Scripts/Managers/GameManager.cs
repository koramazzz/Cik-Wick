using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]  
    [SerializeField] private EggCounterUI _eggCounterUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;

    public static GameManager Instance { get; private set; }
    private int _currentEggCount;

    public void Awake()
    {
        Instance = this;
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);

        if (_currentEggCount == _maxEggCount)
        {
            _eggCounterUI.SetEggCompleted();
            // Implement win condition logic here (e.g., load next level, show win screen, etc.)
        }
    }
}
