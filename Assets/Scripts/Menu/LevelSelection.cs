using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button[] _levelButons;
    private void Start()
    {
        _cancelButton.onClick.AddListener(CancelMenu);
        foreach (Button button in _levelButons)
        {
            button.onClick.AddListener(() => ChangeLevel(button));
        }
    }

    private void CancelMenu()
    {
        SoundManager.instance.ClickButton();
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            ItemBoard.buttonInteractebl?.Invoke(true);
            ButtonManager.buttonInteractebl?.Invoke(true);
        }
    }
    private void ChangeLevel(Button button)
    {
        SoundManager.instance.ClickButton();
        if (button.name == "Easy")
        {
            WeatherManager.changeInterval = 6f;
            PlayerInfo.health = 5;
            SunnyRaySpawner.countSunnyRayPoint = 3;
        }
        else if (button.name == "Medium")
        {
            WeatherManager.changeInterval = 12f;
            PlayerInfo.health = 4;
            SunnyRaySpawner.countSunnyRayPoint = 5;
        }
        else if (button.name == "Hard")
        {
            WeatherManager.changeInterval = 15f;
            PlayerInfo.health = 3;
            SunnyRaySpawner.countSunnyRayPoint = 9;
        }
        SceneManager.LoadScene(1);
    }
}
