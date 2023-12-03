using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    public void UseAbility(Ability clickedAbility, Image reloadImage)
    {
        clickedAbility.abilityReload = true;
        StartCoroutine(ReloadAbility(reloadImage, clickedAbility));
        if (clickedAbility.abilityName == "Umbrela")
        {
            Umbrela.useAbility?.Invoke(clickedAbility.durationAbility);
        }
        else if (clickedAbility.abilityName == "SlowDrop")
        {
            SlowDrop.useAbility?.Invoke(clickedAbility.durationAbility);
        }
        else if (clickedAbility.abilityName == "TakeAutomaticalSunny")
        {
            TakeAotpmaticalSunny.useAbility?.Invoke(clickedAbility.durationAbility);
        }

        else if (clickedAbility.abilityName == "RestoreHealth")
        {
           RestoreHealth.useAbility?.Invoke(clickedAbility.durationAbility);
        }
    }
    private IEnumerator ReloadAbility(Image reloadImage, Ability clickedAbility)
    {
        float timer = clickedAbility.cooldownAbility;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            reloadImage.fillAmount = timer / clickedAbility.cooldownAbility;
            yield return null;
        }

        reloadImage.fillAmount = 0;
        clickedAbility.abilityReload = false;
        if (clickedAbility.curentWetherMode == clickedAbility.wetherMode || clickedAbility.wetherMode == "Unical") clickedAbility.hasActive = true;

    }
}

