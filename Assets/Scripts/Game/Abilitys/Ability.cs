using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities")]
public class Ability : ScriptableObject
{
    public string wetherMode;
    public string curentWetherMode;
    public string abilityName;
    public string discription;
    public Sprite icon;
    public float cooldownAbility;
    public float durationAbility;
    public float costForShop;
    public bool hasBuyed = false;
    public bool hasActive = false;
    public bool abilityReload = true;
}
