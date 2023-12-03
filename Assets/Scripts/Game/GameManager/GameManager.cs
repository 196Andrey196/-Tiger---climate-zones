using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (var ability in ItemBoard.purchasedAbilities)
        {
            if (ability.wetherMode == "Unical") ability.hasActive = true;
            if (ability.hasActive == true && ability.wetherMode != "Unical") ability.hasActive = false;
            ability.abilityReload = false;
        }
    }
    public void ActiveAbilitie(bool hasActive, string wetherMod)
    {

        foreach (var ability in ItemBoard.purchasedAbilities)
        {
            if (ability.curentWetherMode != "Unical") ability.curentWetherMode = wetherMod;
            if (ability.wetherMode == wetherMod && ability.abilityReload == false && ability.curentWetherMode == wetherMod)
            {
                ability.hasActive = hasActive;
            }
        }

    }

}
