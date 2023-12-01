using System;
using System.Collections;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    static public WeatherManager instance;
    [Header("Sunny Weather Settings")]
    [SerializeField] private Color _colorIndicatorSunny;
    [SerializeField] private Sprite _iconSunny;
    [SerializeField] private Color _sunnyBgColor;
    [SerializeField] private GameObject _sunnyPrefab;
    [Header("Rainy Weather Settings")]
    [SerializeField] private Color _colorIndicatorRainy;
    [SerializeField] private Sprite _iconRainy;
    [SerializeField] private Color _rainyBgColor;
    [SerializeField] private GameObject _cloud;
    [SerializeField] private GameObject _umbrela;
    [SerializeField] private GameObject _wetnessIndicator;
    [Header("Rainy Other Settings")]
    private FSM _fsm;
    public static float changeInterval;
    [SerializeField] private float _curentInterval;
    [SerializeField] GameObject _tiger;
    public static Action<string> destroyObjectsOnScene;


    private void Start()
    {

        _curentInterval = changeInterval;
        if (instance != null)
        {
            return;
        }
        instance = this;

        _fsm = new FSM();
        _fsm.AddState(new FSMStateSunny(_fsm, _sunnyPrefab, _tiger, _wetnessIndicator, _colorIndicatorSunny, _iconSunny, _curentInterval));
        _fsm.AddState(new FSMStateRainy(_fsm, _cloud, _tiger, _wetnessIndicator, _colorIndicatorRainy, _iconRainy, _umbrela, _curentInterval));
        _fsm.SetState<FSMStateSunny>();

    }
    void Update()
    {
        _fsm.Update();
    }
    private void OnEnable()
    {
        destroyObjectsOnScene += DestroyObjectsOnScene;
    }

    private void OnDisable()
    {
        destroyObjectsOnScene -= DestroyObjectsOnScene;
    }

    private void DestroyObjectsOnScene(string tagToDelete)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToDelete);

        if (objectsWithTag.Length != 0)
        {
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
        }
    }
    public void ChangeWeather(String currentWeather)
    {
        switch (currentWeather)
        {
            case "FSMStateSunny":
                StartCoroutine(ChangeColorSmoothly(_sunnyBgColor));
                break;
            case "FSMStateRainy":
                StartCoroutine(ChangeColorSmoothly(_rainyBgColor));
                break;
        }

    }
    private IEnumerator ChangeColorSmoothly(Color color)
    {

        SpriteRenderer imageComponent = GetComponent<SpriteRenderer>();
        float duration = 1.0f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Color newColor = Color.Lerp(imageComponent.color, color, elapsedTime / duration);

            imageComponent.color = newColor;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        imageComponent.color = color;

    }

}
