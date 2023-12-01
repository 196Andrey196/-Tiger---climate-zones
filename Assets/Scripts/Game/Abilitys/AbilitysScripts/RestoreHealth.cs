
using System;
using UnityEngine;

public class RestoreHealth : MonoBehaviour
{
    [SerializeField] PlayerInfo _playerInfo;
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
        _playerInfo.startHealth += 1;
        HealthBarSystem.addHeart?.Invoke();
    }
}
