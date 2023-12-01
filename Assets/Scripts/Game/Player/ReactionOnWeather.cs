using System;
using System.Collections;
using UnityEngine;

public class ReactionOnWeather : MonoBehaviour
{
    public static Action<float> wetherPlayerIndicator;
    public static Action<float> reactionOnwetherEfects;
    public static Action damagePlayer;
    private PlayerInfo _playerInfo;
    [SerializeField] private float _curentTime;
    [SerializeField] private float _timeForHanging;
    [SerializeField] private bool _isTimerActive;



    private void Start()
    {
        _playerInfo = GetComponent<PlayerInfo>();
        _isTimerActive = false;

    }
    private void OnEnable()
    {
        FSMStateRainy.changeGameTimerToZero += SetNewToZero;
        reactionOnwetherEfects += ReactionOnwetherEfects;
    }
    private void OnDisable()
    {
        reactionOnwetherEfects -= ReactionOnwetherEfects;
        FSMStateRainy.changeGameTimerToZero -= SetNewToZero;
    }
    private void SetNewToZero()
    {
        FSMStateRainy._gameTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {

        if (colision.gameObject.CompareTag("Drop"))
        {
            _curentTime = _timeForHanging;
            FSMStateRainy.changeGameTimerToZero?.Invoke();
            ObjectInfo objectInfo = colision.GetComponent<ObjectInfo>();
            float wetherCost = objectInfo.damageCost;
            if (!_isTimerActive && _curentTime == _timeForHanging)
            {
                StartCoroutine(ChangeCurrentIndicator(wetherCost));
            }
            ReactionOnwetherEfects(wetherCost);
        }
    }
    private void ReactionOnwetherEfects(float damageCost)
    {
        _playerInfo.curentWetherLevel += damageCost;
        wetherPlayerIndicator?.Invoke(damageCost);
        if (_playerInfo.curentWetherLevel == _playerInfo.maxWetherLevel)
        {
            wetherPlayerIndicator?.Invoke(-_playerInfo.curentWetherLevel);
            _playerInfo.curentWetherLevel = 0;
            damagePlayer?.Invoke();
        }
    }
    private IEnumerator ChangeCurrentIndicator(float stateCost)
    {
        _isTimerActive = true;

        while (_curentTime > 0 && _isTimerActive)
        {
            _curentTime -= Time.deltaTime;
            yield return null;
        }

        if (_curentTime <= 0)
        {
            if (_playerInfo.curentWetherLevel > 0)
            {
                _playerInfo.curentWetherLevel -= stateCost;
                wetherPlayerIndicator?.Invoke(-stateCost);
                _curentTime = _timeForHanging;
            }

        }

        if (_playerInfo.curentWetherLevel == 0)
        {
            _isTimerActive = false;
            StopCoroutine(ChangeCurrentIndicator(stateCost));
        }
        else
        {
            StartCoroutine(ChangeCurrentIndicator(stateCost));
        }
    }


}
