using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button _settingBTN;
    private Animator _settingBTNAnimator;
    [SerializeField] private Button _play;
    [SerializeField] private GameObject _playMenu;

    [SerializeField] private Button _exitGame;
    [SerializeField] private bool _activeMenuSetting;

    public static Action<bool> buttonInteractebl;

    private void OnEnable()
    {
        buttonInteractebl += ButtonInteractebl;
    }
    private void OnDisable()
    {
        buttonInteractebl -= ButtonInteractebl;
    }
    private void Start()
    {
        _activeMenuSetting = false;
        _settingBTN.onClick.AddListener(ClickSettingBTN);
        _exitGame.onClick.AddListener(ExitGame);
        _play.onClick.AddListener(Play);
        _settingBTNAnimator = _settingBTN.GetComponent<Animator>();
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game!!");
    }

    private void ButtonInteractebl(bool interactebl)
    {
        _play.interactable = interactebl;
        _settingBTN.interactable = interactebl;
        _exitGame.interactable = interactebl;
    }

    private void Play()
    {
        SoundManager.instance.ClickButton();
        if (!_playMenu.activeSelf)
        {
            _playMenu.SetActive(true);
            ItemBoard.buttonInteractebl?.Invoke(false);
            buttonInteractebl?.Invoke(false);
        }

    }

    private void ClickSettingBTN()
    {
        SoundManager.instance.ClickButton();
        if (!_activeMenuSetting)
        {
            _activeMenuSetting = true;
            _settingBTNAnimator.SetBool("settingMenuACtive", _activeMenuSetting);
        }
        else
        {
            _activeMenuSetting = false;
            _settingBTNAnimator.SetBool("settingMenuACtive", _activeMenuSetting);
        }
    }
}
