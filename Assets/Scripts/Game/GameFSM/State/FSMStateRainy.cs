
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FSMStateRainy : FSMState
{
    public FSMStateRainy(FSM fsm, GameObject cloud, GameObject tiger, GameObject wetnessIndicator, Color colorIndicatorRainy, Sprite iconRainy, GameObject umbrela, float changeInterval) : base(fsm)
    {
        _fsM = fsm;
        if (cloud != null) _cloud = cloud;
        _wetherIndicator = wetnessIndicator;
        _tigerMovment = tiger.GetComponent<TigerMovment>();
        _playerInfo = tiger.GetComponent<PlayerInfo>();
        _umbrela = umbrela;
        _colorIndicatorRainy = colorIndicatorRainy;
        _iconRainy = iconRainy;
        _changeInterval = changeInterval;
    }
    private FSM _fsM;
    private GameObject _cloud;
    private DropSpawner _dropSpawner;
    private TigerMovment _tigerMovment;
    private PlayerInfo _playerInfo;
    private GameObject _wetherIndicator;
    private Color _colorIndicatorRainy;
    private Sprite _iconRainy;
    private GameObject _umbrela;
    public static bool activeState = false;
    private float _changeInterval;
    private float _startInterval;

    public static float _gameTimer;
    private float _timeInterval = 1f;
    public static Action changeGameTimerToZero;


    public override void Enter()
    {
        GameManager.instance.ActiveAbilitie(true, "Rainy");
        Time.timeScale = 1;
        WeatherManager.instance.ChangeWeather("FSMStateRainy");
        _startInterval = _changeInterval;
        _colorIndicatorRainy.a = 1;
        _wetherIndicator.GetComponent<Image>().color = _colorIndicatorRainy;
        _wetherIndicator.transform.GetChild(0).GetComponent<Image>().sprite = _iconRainy;
        activeState = true;
        _dropSpawner = _cloud.GetComponent<DropSpawner>();
        _cloud.SetActive(true);
        Debug.Log(_fsM.currentState + " Enter State");
        if (_cloud.activeSelf) _dropSpawner.StartSpawn();
        _tigerMovment.SetSpot();
    }

    public override void Exit()
    {
        GameManager.instance.ActiveAbilitie(false,"Rainy");
        activeState = false;
        _cloud.SetActive(false);
        Debug.Log(_fsM.currentState + " Exit State");
        _dropSpawner.StopSpawn();
        _tigerMovment.UnsetSpot();
        ReactionOnWeather.wetherPlayerIndicator?.Invoke(-_playerInfo.curentWetherLevel);
        _playerInfo.curentWetherLevel = 0;
        _umbrela.SetActive(activeState);
        _changeInterval = _startInterval;
        WeatherManager.destroyObjectsOnScene?.Invoke("Drop");
    }

    public override void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (!_umbrela.activeSelf)
                {
                    _umbrela.SetActive(true);
                }
                else
                {
                    return;
                }
            }
        }
        _gameTimer += Time.deltaTime;
        if (_gameTimer >= _timeInterval)
        {
            OtherUI.encreaseCoins?.Invoke(5);
            OtherUI.encreasScore?.Invoke(10f);
            _gameTimer = 0;
        }
        _changeInterval -= Time.deltaTime;
        if (_changeInterval < 0)
        {
            _fsM.SetState<FSMStateSunny>();
        }

    }
}
