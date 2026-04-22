using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _currentPlayerState = PlayerState.Idle;

    private void Start()
    {
        _currentPlayerState = PlayerState.Idle;
    }

    public void ChangePlayerState(PlayerState newPlayerState)
    {
        if (_currentPlayerState == newPlayerState) { return; }

        _currentPlayerState = newPlayerState;
    }

    public PlayerState GetCurrentPlayerState()
    {
        return _currentPlayerState;
    }
}

