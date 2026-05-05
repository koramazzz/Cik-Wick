using UnityEngine;

public class CatStateController : MonoBehaviour
{
    [SerializeField] private CatState currentState = CatState.Idle;

    private void Start()
    {
        ChangeState(CatState.Walking);
    }

    public void ChangeState(CatState newState)
    {
        if (currentState == newState) { return; }
        
        currentState = newState;
        Debug.Log("Cat state changed to: " + currentState);

    }

    public CatState GetCurrentState()
    {
        return currentState;
    }
}
