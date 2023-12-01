
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAotpmaticalSunny : MonoBehaviour
{

    [SerializeField] string _tag;
    public static Action<float> useAbility;
    [SerializeField] List<GameObject> _list;


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
        StartCoroutine(FollowPlayerCoroutine(timer));
    }
    private IEnumerator FollowPlayerCoroutine(float timer)
    {
        Debug.Log(1);
        float elapsedTime = 0f;

        while (elapsedTime < timer)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(_tag);

            foreach (GameObject obj in taggedObjects)
            {
                obj.GetComponent<LiveRayPoint>()?.OnInputDown();
            }

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}

