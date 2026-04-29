using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool IsGameplayActive => Instance != null && 
        (Instance._currentGameState == GameState.Play || Instance._currentGameState == GameState.Resume);

    public event Action<GameState> OnGameStateChanged;

    
    [Header("References")]  
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;

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
        Time.timeScale = gameState == GameState.Pause || gameState == GameState.GameOver ? 0f : 1f;
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);

        if (_currentEggCount == _maxEggCount)
        {
            // WIN
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
