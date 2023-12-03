using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoard : MonoBehaviour
{
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private Ability[] _allAbilities;
    [SerializeField] private Button[] _abilityButtons;
    [SerializeField] private GameObject _buyMenu;
    public static List<Ability> purchasedAbilities = new List<Ability>();
    public static Action<bool> buttonInteractebl;

    private void OnEnable()
    {
        buttonInteractebl += SetBuyedAbility;
    }
    private void OnDisable()
    {
        buttonInteractebl -= SetBuyedAbility;
    }


    private void Start()
    {
        CreateButtons();
        foreach (Ability ability in _allAbilities)
        {
            ability.hasBuyed = false;

        }
    }
    private void Update()
    {
        SetBuyedAbility(true);
    }

    private void SetItemStatus()
    {

        for (int i = 0; i < _allAbilities.Length; i++)
        {
            if (_allAbilities[i].hasBuyed == true)
            {
                _abilityButtons[i].transform.GetChild(3).gameObject.SetActive(true);
                SetMarkAlpha(_abilityButtons[i].transform.GetChild(0).GetComponent<Image>());
                _abilityButtons[i].interactable = false;
            }
        }
    }
    private void SetMarkAlpha(Image itemImage)
    {
        Color buttonColor = itemImage.color;
        buttonColor.a = 0.5f;
        itemImage.color = buttonColor;
    }
    private void SetBuyedAbility(bool itemStatus)
    {
        foreach (Ability ability in _allAbilities)
        {
            if (purchasedAbilities.Contains(ability))
            {
                ability.hasBuyed = itemStatus;
            }
        }
        SetItemStatus();
    }

    private void CreateButtons()
    {
        _abilityButtons = new Button[_allAbilities.Length];

        for (int i = 0; i < _allAbilities.Length; i++)
        {
            Ability ability = _allAbilities[i];
            Button newButton = Instantiate(_buttonPrefab, transform);
            Image buttonImage = newButton.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            TextMeshProUGUI coins = newButton.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI discraption = newButton.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
            Button discraptionBtn = newButton.transform.GetChild(2).GetComponent<Button>();
            Transform discraptionPanel = discraptionBtn.transform.GetChild(1);
            Button cancelBtn = discraptionBtn.transform.GetChild(1).GetChild(0).GetComponent<Button>();
            if (ability.icon != null)
            {
                buttonImage.sprite = ability.icon;
                coins.text = ability.costForShop.ToString();
                discraption.text = ability.discription;
            }
            newButton.onClick.AddListener(() => OnAbilityButtonClicked(ability, newButton));
            discraptionBtn.onClick.AddListener(() => OnDiscraptionButtonClicked(discraptionPanel));
            cancelBtn.onClick.AddListener(() => CancelButtonClicked(discraptionPanel.gameObject));
            _abilityButtons[i] = newButton;
        }
    }

    private void CancelButtonClicked(GameObject discraptionPanel)
    {
        SoundManager.instance.ClickButton();
        if (discraptionPanel.activeSelf) discraptionPanel.SetActive(false);
        else return;
    }

    private void OnDiscraptionButtonClicked(Transform discraptionPanel)
    {
        SoundManager.instance.ClickButton();
        if (discraptionPanel != null)
        {
            if (!discraptionPanel.gameObject.activeSelf) discraptionPanel.gameObject.SetActive(true);
            else discraptionPanel.gameObject.SetActive(false);
        }
    }

    private void OnAbilityButtonClicked(Ability ability, Button button)
    {
        SoundManager.instance.ClickButton();
        _buyMenu.SetActive(true);
        buttonInteractebl?.Invoke(false);
        ButtonManager.buttonInteractebl?.Invoke(false);
        if (_buyMenu.activeSelf) BuyMenu.setInfoAboutItem?.Invoke(ability);
    }
}
