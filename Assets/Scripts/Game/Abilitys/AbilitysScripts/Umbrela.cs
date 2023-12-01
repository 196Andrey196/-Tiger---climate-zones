using System;
using System.Collections;
using System.Numerics;
using UnityEngine;

public class Umbrela : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _umbrela;
    public static Action<float> useAbility;
    private void OnEnable()
    {
        useAbility += UseAbility;
    }
    private void OnDisable()
    {
        useAbility -= UseAbility;
    }

    public void UseAbility(float timer)
    {
        if (_umbrela != null)
        {
            StartCoroutine(FollowPlayerCoroutine(timer));
        }
    }
    private IEnumerator FollowPlayerCoroutine(float timer)
    {
        float elapsedTime = 0f;
        if (!_umbrela.activeSelf) _umbrela.SetActive(true);

        while (elapsedTime < timer)
        {
            if (_playerTransform != null)
            {
                _umbrela.transform.position = _playerTransform.position + new UnityEngine.Vector3(0, _playerTransform.position.y + 2.5f, 0);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (_umbrela.activeSelf) _umbrela.SetActive(false);

    }

}
