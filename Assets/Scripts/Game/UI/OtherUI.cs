using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtherUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _coinsCounter;
    [SerializeField] private AudioClip _addCoinsSound;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private AudioClip _pauseMenuSound;
    [SerializeField] private Button _pause;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private PlayerInfo _playerInfo;
    private float _curentScore;
    public static Action<float> encreasScore;
    public static Action<AudioClip> gameOver;
    public static Action<float> encreaseCoins;
    private void OnEnable()
    {
        encreasScore += EncreasScore;
        gameOver += GameOverAndShowMenu;
        encreaseCoins += EncreaseCoins;
    }
    private void OnDisable()
    {
        encreasScore -= EncreasScore;
        gameOver -= GameOverAndShowMenu;
        encreaseCoins -= EncreaseCoins;
    }

    private void Start()
    {
        _score.text = "0";
        _coinsCounter.text = "0";
        _pause.onClick.AddListener(() => SetPause());
    }

    private void Update()
    {
        _score.text = _curentScore.ToString();
        _coinsCounter.text = _playerInfo.curentCoinsCount.ToString();
    }
    private void EncreasScore(float newScore)
    {
        if (_addCoinsSound != null) SoundManager.instance.PlaySound(_addCoinsSound, 0.2f);
        _curentScore += newScore;
    }

    private void EncreaseCoins(float coins)
    {
        _playerInfo.curentCoinsCount += coins;
    }

    private void SetPause()
    {
        SoundManager.instance.ClickButton();
        Time.timeScale = 0;
        SoundManager.instance.PlayBGSound(_pauseMenuSound);
        if (_pauseMenu != null) _pauseMenu.SetActive(true);
    }
    private void GameOverAndShowMenu(AudioClip gameOverSound)
    {

            SoundManager.instance.PlaySound(gameOverSound, 0.01f);
        PlayerInfo.coinsInWallet = _playerInfo.curentCoinsCount;
        _gameOverMenu.SetActive(true);
        StopAllCoroutines();
        Time.timeScale = 0;
    }
}

