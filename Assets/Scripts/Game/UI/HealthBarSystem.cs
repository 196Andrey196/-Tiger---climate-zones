using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSystem : MonoBehaviour
{

    [SerializeField] private GameObject _livePrfab;
    [SerializeField] private Sprite _livePrfabBG;
    [SerializeField] private Sprite _liveIMG;
    [SerializeField] private List<GameObject> _lives;
    [SerializeField] private int _nextHeartForDamage;
    [SerializeField] private PlayerInfo _playerInfo;
    [SerializeField] private AudioClip _gameOverSound;
    public static Action addHeart;


    void Start()
    {
        _nextHeartForDamage = 0;
        FillLivesList();
        if (_lives.Count != 0 && _playerInfo.startHealth != 0) InstantiateListObjects();
    }

    private void OnEnable()
    {
        ReactionOnWeather.damagePlayer += TakeDamage;
        addHeart += AddHeart;
    }
    private void OnDisable()
    {
        ReactionOnWeather.damagePlayer -= TakeDamage;
        addHeart -= AddHeart;
    }

    private void Update()
    {
        if (_playerInfo.startHealth == 0) OtherUI.gameOver?.Invoke(_gameOverSound);
    }
    private void TakeDamage()
    {
        if (_playerInfo.startHealth != 0)
        {
            ChangeHeart();
            _playerInfo.startHealth -= 1;
        }

    }

    private void FillLivesList()
    {
        for (int i = 0; i < _playerInfo.startHealth; i++)
        {
            _lives.Add(_livePrfab);
        }
    }
    private void InstantiateListObjects()
    {
        for (int i = 0; i < _playerInfo.startHealth; i++)
        {
            Instantiate(_livePrfab, transform);
        }
    }
    private void ChangeHeart()
    {
        if (_nextHeartForDamage < transform.childCount)
            _nextHeartForDamage++;

        Image lastImage = transform.GetChild(transform.childCount - _nextHeartForDamage).GetComponent<Image>();
        lastImage.sprite = _livePrfabBG;
    }
    private void AddHeart()
    {

        Image lastImage = transform.GetChild(transform.childCount - _nextHeartForDamage).GetComponent<Image>();
        if (lastImage != null)
        {
                lastImage.sprite = _liveIMG;
                _nextHeartForDamage--;
        }

    }


}



