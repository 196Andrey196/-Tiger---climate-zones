using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveRayPoint : MonoBehaviour
{
    [SerializeField] private Image _imageForeTimer;
    [SerializeField] private float _timeForLive;
    private ObjectInfo _objectInfo;
    [SerializeField] private AudioClip _tapSound;
    private void Start()
    {
        StartCoroutine(StartTimer());
        _objectInfo = GetComponent<ObjectInfo>();
    }


    private void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosition2D = new Vector2(touchPosition.x, touchPosition.y);

            RaycastHit2D hit = Physics2D.Raycast(touchPosition2D, Vector2.zero);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<LiveRayPoint>()?.OnInputDown();
            }
        }
    }
    private IEnumerator StartTimer()
    {
        float currentTime = _timeForLive;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            float fillValue = currentTime / _timeForLive;
            _imageForeTimer.fillAmount = fillValue;

            yield return null;
        }

        TimerCompleted();
    }
    private void TimerCompleted()
    {
        ReactionOnWeather.reactionOnwetherEfects(_objectInfo.damageCost);
        Destroy(gameObject);
    }

    public void OnInputDown()
    {
        SoundManager.instance.PlaySound(_tapSound, 0.5f);
        OtherUI.encreasScore?.Invoke(20f);
        OtherUI.encreaseCoins?.Invoke(15);
        Destroy(gameObject);
    }

}
