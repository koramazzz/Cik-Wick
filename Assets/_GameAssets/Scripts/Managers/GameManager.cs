using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    
    [Header("References")]  
    [SerializeField] private EggCounterUI _eggCounterUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;

    private int _currentEggCount;
    private GameState _currentGameState;

    public void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }

    public void ChangeGameState(GameState newGameState)
    {
        _currentGameState = newGameState;
        SetGameTimeScale(newGameState);
        OnGameStateChanged?.Invoke(newGameState);
    }
    
    private void SetGameTimeScale(GameState gameState)
    {
        Time.timeScale = gameState == GameState.Pause ? 0f : 1f;
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);

        if (_currentEggCount == _maxEggCount)
        {
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
        }
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
