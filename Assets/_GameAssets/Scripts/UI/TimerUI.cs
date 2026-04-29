using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _timerRotatableTransform;
    [SerializeField] private TMP_Text _timerText;

    [Header("Settings")]
    [SerializeField] private float _rotationDuration;
    [SerializeField] private Ease _rotationEase;

    private float _elapsedTime;
    private bool _isTimerRunning;
    private Tween _rotationTween;

    private void Start()
    {
        PlayRotationAnimation();
        StartTimer();

        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Play:
            case GameState.Resume:

                ResumeTimer();
                break;

            case GameState.Pause:
            case GameState.GameOver:

                PauseTimer();
                break;
        }
    }

    private void PlayRotationAnimation()
    {
        _rotationTween = _timerRotatableTransform.DORotate(new Vector3(0, 0, -360), _rotationDuration, RotateMode.FastBeyond360).SetEase(_rotationEase).SetLoops(-1, LoopType.Restart).SetLink(gameObject);
    }

    private void StartTimer()
    {
        _isTimerRunning = true;
        _elapsedTime = 0f;
        InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
    }

    private void UpdateTimerUI()
    {
        if (!_isTimerRunning)
        {
            return;
        }

        _elapsedTime++;
        _timerText.text = GetFormattedTime();
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void PauseTimer()
    {
        _isTimerRunning = false;
        CancelInvoke(nameof(UpdateTimerUI));
        _rotationTween.Pause();
    }

    private void ResumeTimer()
    {
        if (!_isTimerRunning)
        {
            _isTimerRunning = true;
            InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
            _rotationTween.Play();
        }
    }
}
