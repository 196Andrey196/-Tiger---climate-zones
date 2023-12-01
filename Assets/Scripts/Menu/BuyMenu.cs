using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BuyMenu : MonoBehaviour
{
    [SerializeField] private Image _itemIconObject;
    [SerializeField] private TextMeshProUGUI _priseText;
    [SerializeField] Button _confirmationBtn;
    private Image _mark;
    [SerializeField] Button _refusal;
    [SerializeField] private Ability _ability;
    [SerializeField] private Button _selectedButton;
    [SerializeField] private int _countBuyedAbility;
    [SerializeField] private GameObject _messageAboutMoney;
    [SerializeField] private AudioClip _buyItemSound;
    public static Action<Ability> setInfoAboutItem;

    private void OnEnable()
    {
        setInfoAboutItem += SetInfo;
    }
    private void OnDisable()
    {
        setInfoAboutItem -= SetInfo;
    }
    private void Start()
    {
        _mark = _confirmationBtn.transform.GetChild(0).GetComponent<Image>();
        _confirmationBtn.onClick.AddListener(() => BuyItem(_ability));
        _refusal.onClick.AddListener(HideMenu);
        ItemBoard.buttonInteractebl?.Invoke(false);
        ButtonManager.buttonInteractebl?.Invoke(false);
    }

    private void Update()
    {
        if (PlayerInfo.coinsInWallet != 0 && PlayerInfo.coinsInWallet >= _ability.costForShop)
        {
            SetMarkAlpha(1f);
            _confirmationBtn.interactable = true;
            _messageAboutMoney.SetActive(false);
        }
        else
        {
            SetMarkAlpha(0.5f);
            _confirmationBtn.interactable = false;
            _messageAboutMoney.SetActive(true);
        }
    }

    private void SetMarkAlpha(float alphaValue)
    {
        Color buttonColor = _mark.color;
        buttonColor.a = alphaValue;
        _mark.color = buttonColor;
    }

    private void HideMenu()
    {
        SoundManager.instance.ClickButton();
        ItemBoard.buttonInteractebl?.Invoke(true);
        ButtonManager.buttonInteractebl?.Invoke(true);
        gameObject.SetActive(false);

    }

    private void BuyItem(Ability ability)
    {
        SoundManager.instance.ClickButton();
        SoundManager.instance.PlaySound(_buyItemSound, 0.025f);
        ShopManager.decreasMoney?.Invoke(ability.costForShop);
        ability.hasBuyed = true;
        ItemBoard.purchasedAbilities.Insert(_countBuyedAbility, ability);
        _countBuyedAbility++;
        ButtonManager.buttonInteractebl?.Invoke(true);

        gameObject.SetActive(false);

    }

    private void SetInfo(Ability ability)
    {
        _ability = ability;
        _itemIconObject.sprite = ability.icon;
        _priseText.text = ability.costForShop.ToString();
    }

}
