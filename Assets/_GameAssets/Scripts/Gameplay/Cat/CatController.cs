using System;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public event Action OnCatCatched;
    [Header("References")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _playerTransform;

    [Header("Movement Settings")]
    [SerializeField] private float _defaultSpeed = 5f;
    [SerializeField] private float _chaseSpeed = 7f;

    [Header("Navigation Settings")]
    [SerializeField] private float _patrolRadius = 10f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private int _maxDestinationAttempts = 10;
    [SerializeField] private float _chaseDistanceThreshold = 1.5f;
    [SerializeField] private float _chaseDistance = 2f;



    private NavMeshAgent _catAgent;
    private CatStateController _catStateController;

    private Vector3 _initialPosition;
    private float _timer;
    private bool _isWaiting = false;
    private bool _isChasing = false;
    private bool _hasCaughtPlayer;

    private void Awake()
    {
        _catAgent = GetComponent<NavMeshAgent>();
        _catStateController = GetComponent<CatStateController>();
        _catAgent.speed = _defaultSpeed;
    }
    private void Start()
    {
        _initialPosition = transform.position;
        SetRandomDestination();
    }

    private void Update()
    {
        if (_hasCaughtPlayer)
        {
            return;
        }

        if (_playerController.CanCatChase())
        {
            SetChaseMovement();
        }
        else
        {
            SetPatrolMovement();
        }
    }

    private void SetChaseMovement()
    {
        _isChasing = true;
        _catAgent.speed = _chaseSpeed;
        _catStateController.ChangeState(CatState.Chasing);
        
        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        Vector3 offsetPosition = _playerTransform.position - directionToPlayer * _chaseDistanceThreshold;
        _catAgent.SetDestination(offsetPosition);

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _chaseDistance && _isChasing)
        {
            _hasCaughtPlayer = true;
            OnCatCatched?.Invoke();
            _catStateController.ChangeState(CatState.Attacking);
            _isChasing = false;
        }
    }

    private void SetPatrolMovement()
    {
        _isChasing = false;
        _catAgent.speed = _defaultSpeed;

        if (_catAgent.hasPath && !_catAgent.pathPending && _catAgent.remainingDistance <= _catAgent.stoppingDistance)
        {
            if (!_isWaiting)
            {
                _isWaiting = true;
                _timer = _waitTime;
                _catStateController.ChangeState(CatState.Idle);
            }
        }

        if (_isWaiting)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _isWaiting = false;
                SetRandomDestination();
                _catStateController.ChangeState(CatState.Walking);
            }
        }
    }

    private void SetRandomDestination()
    {
        int attempts = 0;
        bool destinationSet = false;

        while (attempts < _maxDestinationAttempts && !destinationSet)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _patrolRadius;
            randomDirection += _initialPosition;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _patrolRadius, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;

                if (IsPositionValid(finalPosition))
                {
                    _catAgent.SetDestination(finalPosition);
                    destinationSet = true;
                }

                else
                {
                    attempts++;
                }
            }

            else
            {
                attempts++;
            }
        }

        if (!destinationSet)
        {
            Debug.LogWarning("Failed to find a valid patrol destination after " + _maxDestinationAttempts + " attempts.");
            _isWaiting = true;
            _timer = _waitTime * 2;
            _catStateController.ChangeState(CatState.Idle);
        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        return !NavMesh.Raycast(transform.position, position, out _, NavMesh.AllAreas);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = (_initialPosition != Vector3.zero) ? _initialPosition : transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, _patrolRadius);
    }
}
