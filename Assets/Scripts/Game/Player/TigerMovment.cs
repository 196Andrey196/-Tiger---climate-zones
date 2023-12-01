using System.Collections;
using UnityEngine;

public class TigerMovment : MonoBehaviour
{
    [SerializeField] private Transform _startSpot;
    public Transform startPoint { get { return _startSpot; } }
    [SerializeField] private Transform[] _moveSpots;
    [SerializeField] private int _currentSpotIndex = 0;
    private Rigidbody2D _rb;
    private bool _isMoving = true;
    [SerializeField] private Transform _newSpot;
    private Animator _animator;
    private Vector2 previousPosition;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    void Update()
    {
        Vector2 currentPos = transform.position;
        Vector2 movementDirection = currentPos - previousPosition;

        if (movementDirection.x > 0f) 
        {
            _animator.CrossFade("MoveRight", 0);
        }
        else if (movementDirection.x < 0f) 
        {
            _animator.CrossFade("MoveLeft", 0);
        }
        else 
        {
            _animator.CrossFade("Idle", 0);
        }

        previousPosition = currentPos;
    }



    private IEnumerator MoveBetweenSpots()
    {
        _newSpot = _moveSpots[_currentSpotIndex];
        while (_isMoving)
        {
            Vector3 direction = (transform.position - _newSpot.position).normalized;


            if (Vector2.Distance(transform.position, _newSpot.position) < 0.1f)
            {
                _newSpot = _moveSpots[_currentSpotIndex];
                _currentSpotIndex++;

                if (_currentSpotIndex >= _moveSpots.Length)
                {
                    _currentSpotIndex = 0;
                }
            }
            MoveToSpot(_newSpot);
            yield return null;
        }
    }

    public void SetSpot()
    {
        _isMoving = true;
        StartCoroutine(MoveBetweenSpots());
    }

    public void UnsetSpot()
    {
        _isMoving = false;
        StopCoroutine(MoveBetweenSpots());
        StartCoroutine(ReturnToStartCoroutine());
    }
    private IEnumerator ReturnToStartCoroutine()
    {
        while (Vector2.Distance(transform.position, _startSpot.position) > 0.1f)
        {
            MoveToSpot(_startSpot);
            yield return null;
        }
    }


    private void MoveToSpot(Transform nextPoint)
    {
        float speed = GetComponent<PlayerInfo>().speed;
        if (nextPoint != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPoint.position, speed * Time.deltaTime);
        }
    }

}
