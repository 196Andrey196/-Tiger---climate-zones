using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private Button _soundBtn, _musicBtn;
    [SerializeField] private Sprite _soundImageOn, _musicImageOn, _soundImageOff, _musicImageOff;
    [SerializeField] private bool _soundStatusBtn;
    [SerializeField] private bool _musicStatusBtn;



    void Start()
    {
        _animator = GetComponent<Animator>();
        _soundStatusBtn = false;
        _musicStatusBtn = false;
        _soundBtn.onClick.AddListener(() => SwitchSound(_soundBtn));
        _musicBtn.onClick.AddListener(() => SwitchMusic(_musicBtn));
        SeteButtonStatus(_musicBtn, ref _musicStatusBtn, _musicImageOn, _musicImageOff, ref SoundManager.musicVolumeStatus);
        SeteButtonStatus(_soundBtn, ref _soundStatusBtn, _soundImageOn, _soundImageOff, ref SoundManager.soundVolumeStatus);
    }

    private void SwitchMusic(Button button)
    {
        SoundManager.instance.ClickButton();
        SoundManager.instance.SetVolumeMusic(SoundManager.musicVolumeStatus);
        SeteButtonStatus(button, ref _musicStatusBtn, _musicImageOn, _musicImageOff, ref SoundManager.musicVolumeStatus);
    }

    private void SwitchSound(Button button)
    {
        SoundManager.instance.ClickButton();
        SoundManager.instance.SetVolumeSound(SoundManager.soundVolumeStatus);
        SeteButtonStatus(button, ref _soundStatusBtn, _soundImageOn, _soundImageOff, ref SoundManager.soundVolumeStatus);
    }
    private void SeteButtonStatus(Button button, ref bool buttonStatus, Sprite imageOn, Sprite imageOff, ref bool statusFlag)
    {
        Image buttonImg = button.GetComponent<Image>();
        if (buttonStatus == false && statusFlag == false)
        {
            statusFlag = true;
            buttonStatus = true;
            buttonImg.sprite = imageOn;
        }
        else
        {
            statusFlag = false;
            buttonStatus = false;
            buttonImg.sprite = imageOff;
        }
    }





}
