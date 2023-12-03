using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Animator _animator;
    public static bool _endTutorial;
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (!_endTutorial)
        {
            Time.timeScale = 0;
            StartCoroutine(EndTutorial());
            _animator.CrossFade("Tutorial", 0);
        }
    }
    IEnumerator EndTutorial()
    {
        _endTutorial = true;
        while (true)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Tutorial") &&
                _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {

                Time.timeScale = 1;
                break;
            }
            yield return null;
        }
    }

}
