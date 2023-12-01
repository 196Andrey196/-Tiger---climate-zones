using System;
using UnityEngine;
using UnityEngine.UI;


public class FSMStateSunny : FSMState
{

     public FSMStateSunny(FSM fsm, GameObject sunnyPrefab, GameObject tiger, GameObject wetnessIndicator, Color colorIndicatorSunny, Sprite iconSunny, float changeInterval) : base(fsm)
     {
          _fsM = fsm;
          _sunnyPrefab = sunnyPrefab;
          _wetherIndicator = wetnessIndicator;
          _colorIndicatorSunny = colorIndicatorSunny;
          _iconSunny = iconSunny;
          _playerInfo = tiger.GetComponent<PlayerInfo>();
          _changeInterval = changeInterval;
     }
     private FSM _fsM;
     [SerializeField] private GameObject _sunnyPrefab;
     private SunnyRaySpawner _sunnyPointSpawner;
     private PlayerInfo _playerInfo;

     private Color _colorIndicatorSunny;
     private Sprite _iconSunny;
     private GameObject _wetherIndicator;
     private float _changeInterval;
     private float _startInterval;

     public override void Enter()
     {
          GameManager.instance.ActiveAbilitie(true, "Sunny");
          Time.timeScale = 1;
          WeatherManager.instance.ChangeWeather("FSMStateSunny");
          _startInterval = _changeInterval;
          _colorIndicatorSunny.a = 1;
          _wetherIndicator.GetComponent<Image>().color = _colorIndicatorSunny;
          _wetherIndicator.transform.GetChild(0).GetComponent<Image>().sprite = _iconSunny;
          _sunnyPointSpawner = _sunnyPrefab.transform.GetChild(0).GetComponent<SunnyRaySpawner>();
          _sunnyPrefab.SetActive(true);
          Debug.Log(_fsM.currentState + " Enter State");
          _sunnyPointSpawner.StartSpawn();
     }
     public override void Exit()
     {
          GameManager.instance.ActiveAbilitie(false, "Sunny");
          _sunnyPrefab.SetActive(false);
          Debug.Log(_fsM.currentState + " Exit State");
          _sunnyPointSpawner.StopSpawn();
          WeatherManager.destroyObjectsOnScene?.Invoke("RayPoint");
          ReactionOnWeather.wetherPlayerIndicator?.Invoke(-_playerInfo.curentWetherLevel);
          _playerInfo.curentWetherLevel = 0;
          _changeInterval = _startInterval;
     }
     public override void Update()
     {

          _changeInterval -= Time.deltaTime;
          if (_changeInterval < 0)
          {
               _fsM.SetState<FSMStateRainy>();
          }
     }
}
