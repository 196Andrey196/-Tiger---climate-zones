using UnityEngine;

public class SunnyRayRenderer : MonoBehaviour
{

    private LineRenderer _lineRenderer;

    [SerializeField] private GameObject _sunny;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _sunny = GameObject.FindWithTag("Sunny");
    }

    void Update()
    {
        if (_lineRenderer.positionCount != 2)
        {
            _lineRenderer.positionCount = 2;
        }

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _sunny.transform.position);
    }



}


