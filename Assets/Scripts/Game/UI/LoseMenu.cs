using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] private Button _retry;
    [SerializeField] private Button _menu;
    [SerializeField] private AudioClip _menuSound;
    void Start()
    {
        _retry.onClick.AddListener(Retry);
        _menu.onClick.AddListener(Menu);
        SoundManager.instance.PlayBGSound(_menuSound);

    }

    private void Menu()
    {
        SceneManager.LoadScene(0);
    }

    private void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
