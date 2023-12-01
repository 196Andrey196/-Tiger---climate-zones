using System;
using System.Collections;
using UnityEngine;

public class SlowDrop : MonoBehaviour
{
    public static Action<float> useAbility;
    [SerializeField] private GameObject _dropPrefab;
    [SerializeField] private float _startDropGravity;
    private Rigidbody2D _dropRb;

    private void OnEnable()
    {
        useAbility += UseAbility;
    }
    private void OnDisable()
    {
        useAbility -= UseAbility;
    }
    private void Start()
    {
        _dropRb = _dropPrefab.GetComponent<Rigidbody2D>();
        _startDropGravity = 1.5f;
    }
    public void UseAbility(float timer)
    {
        StartCoroutine(FollowPlayerCoroutine(timer));
    }
    private IEnumerator FollowPlayerCoroutine(float timer)
    {
        float elapsedTime = 0f;

        while (elapsedTime < timer)
        {
            _dropRb.gravityScale = 0.3f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _dropRb.gravityScale = _startDropGravity;

    }
}
