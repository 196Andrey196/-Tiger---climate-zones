using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button _continue, _menu;

    private void Start()
    {
        _continue.onClick.AddListener(Continue);
        _menu.onClick.AddListener(Menu);
    }

    private void Menu()
    {
        SoundManager.instance.ClickButton();
        SceneManager.LoadScene(0);
    }

    private void Continue()
    {
        SoundManager.instance.ClickButton();
        gameObject.SetActive(false);
        SoundManager.instance.SetDefaultMusic();
        Time.timeScale = 1;
    }
}
