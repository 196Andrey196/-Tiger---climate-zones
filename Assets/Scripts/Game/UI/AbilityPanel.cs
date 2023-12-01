using UnityEngine;
using UnityEngine.UI;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] private Transform _abilityPanel;
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AbilityManager _abilityManager;
    [SerializeField] private Button[] _abilityButtons;
    private void Start()
    {
        CreateButtons();
    }
    private void Update()
    {
        UpdateAbilityButtonsActivity();
    }
    private void CreateButtons()
    {
        _abilityButtons = new Button[ItemBoard.purchasedAbilities.Count];

        for (int i = 0; i < ItemBoard.purchasedAbilities.Count; i++)
        {
            Ability ability = ItemBoard.purchasedAbilities[i];
            Button newButton = Instantiate(_buttonPrefab, _abilityPanel);
            Image buttonImage = newButton.GetComponent<Image>();

            if (ability.icon != null)
            {
                buttonImage.sprite = ability.icon;
            }

            newButton.onClick.AddListener(() => OnAbilityButtonClicked(ability, newButton));
            _abilityButtons[i] = newButton;
        }
    }
    private void OnAbilityButtonClicked(Ability clickedAbility, Button button)
    {
        SoundManager.instance.ClickButton();
        Image reloadImage = button.transform.GetChild(0).GetComponent<Image>();
        if (clickedAbility != null && reloadImage != null)
        {
            _abilityManager.UseAbility(clickedAbility, reloadImage);
            clickedAbility.hasActive = false;
        }
    }
    private void UpdateAbilityButtonsActivity()
    {
        for (int i = 0; i < _abilityButtons.Length; i++)
        {
            Ability ability = ItemBoard.purchasedAbilities[i];
            _abilityButtons[i].interactable = ability.hasActive;
        }
    }
}
