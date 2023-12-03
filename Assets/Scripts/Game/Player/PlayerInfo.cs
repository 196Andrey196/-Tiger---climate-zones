using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private float _speed;
    public float speed { get { return _speed; } }
    static public float health;
    [SerializeField] private float _startHealth;
    public float startHealth { get { return _startHealth; } set { if (startHealth != 0) _startHealth = value; } }
    [SerializeField] private float _curentWetherLevel = 0f;
    public float curentWetherLevel
    {
        get { return _curentWetherLevel; }
        set
        {
            if (value <= _maxWetherLevel)
            {
                _curentWetherLevel = value;
            }
            else
            {
                _curentWetherLevel = _maxWetherLevel;
            }
        }
    }
    [SerializeField] private float _maxWetherLevel;
    public float maxWetherLevel { get { return _maxWetherLevel; } }
    static public float coinsInWallet = 600;
    [SerializeField] private float _curentCoinsCount;
    public float curentCoinsCount { get { return _curentCoinsCount; } set { if (value > 0) _curentCoinsCount = value; } }
    private void Awake()
    {
        _startHealth = health;
        _curentCoinsCount = coinsInWallet;
    }
}
