using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool IsGameplayActive => Instance != null && 
        (Instance._currentGameState == GameState.Play || Instance._currentGameState == GameState.Resume);

    public event Action<GameState> OnGameStateChanged;
    
    [Header("References")]  
    [SerializeField] private CatController _catController;
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private PlayerHealthUI _playerHealthUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    [SerializeField] private float _delayBeforeGameOver = 1f;

    private int _currentEggCount;
    private GameState _currentGameState;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
        _catController.OnCatCatched += CatController_OnCatCatched;
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.CutScene);
        BackgroundMusic.Instance.PlayBackgroundMusic(true);
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

    private IEnumerator OnGameOver(bool _isCatCatched)
    {
        yield return new WaitForSeconds(_delayBeforeGameOver);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();

        if (_isCatCatched)
        {
            AudioManager.Instance.Play(SoundType.CatSound);
        }
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver(false));
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }

    private void CatController_OnCatCatched()
    {
        _playerHealthUI.AnimateDamageForAll();
        CameraShake.Instance.ShakeCamera(3f, 1.2f, 0.2f);
        StartCoroutine(OnGameOver(true));
    }
}
