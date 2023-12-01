using System;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countMoney;
    public static Action<float> decreasMoney;
    private void OnEnable()
    {
        decreasMoney += DecreasMoney;
    }
    private void OnDisable()
    {
        decreasMoney -= DecreasMoney;
    }

    public void DecreasMoney(float cost)
    {
        if (cost != 0 && cost > 0) PlayerInfo.coinsInWallet -= cost;
    }

    private void Update()
    {
        _countMoney.text = PlayerInfo.coinsInWallet.ToString();
    }

}
