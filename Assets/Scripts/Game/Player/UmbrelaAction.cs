using UnityEngine;
using UnityEngine.EventSystems;

public class UmbrelaAction : MonoBehaviour
{
    [SerializeField] private bool _isHoldingUmbrella = false;


    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (gameObject.activeSelf)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        return;
                    }

                    SetUmbrellaPosition(touch.position);
                    _isHoldingUmbrella = true;
                }
            }
        }

        if (_isHoldingUmbrella && Input.touchCount == 0)
        {
            _isHoldingUmbrella = false;
        }

        if (_isHoldingUmbrella)
        {
            UpdateUmbrellaPosition(Input.mousePosition);
        }
    }

    private void SetUmbrellaPosition(Vector2 position)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        worldPosition.z = 0;

        transform.position = position;
    }

    private void UpdateUmbrellaPosition(Vector2 mousePosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;

        transform.position = worldPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Drop")) Destroy(other.gameObject);
    }
}
