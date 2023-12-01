using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{
    [SerializeField] private Image _wetIndicator;
    private void OnEnable()
    {
        ReactionOnWeather.wetherPlayerIndicator += ChangeIndicator;
    }
    private void OnDisable()
    {
        ReactionOnWeather.wetherPlayerIndicator -= ChangeIndicator;
    }
    private void ChangeIndicator(float cost)
    {
            _wetIndicator.fillAmount += cost / 100;
    }
}
