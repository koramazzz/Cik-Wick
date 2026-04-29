using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    private PlayerController _playerController;
    private StateController _stateController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _stateController = GetComponent<StateController>();
    }

    private void Start()
    {
        _playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
    }

    private void Update()
    {
        if (!GameManager.IsGameplayActive)
        {
            return;
        }
        SetPlayerAnimations();
    }

    private void PlayerController_OnPlayerJumped()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        Invoke(nameof(ResetJumping), 0.5f);
    }

    private void ResetJumping()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
    }

    private void SetPlayerAnimations()
    {
        var currentPlayerState = _stateController.GetCurrentPlayerState();

        switch(currentPlayerState)
        {
            case PlayerState.Idle:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                break;

            case PlayerState.Move:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                break;
            
            case PlayerState.Slide:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                break;
            
            case PlayerState.SlideIdle:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                break;

            default:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                break;
        }
    }
}
