using UnityEngine;
using Random = UnityEngine.Random;

public class PerlinNoise : MonoBehaviour
{
    #region --Fields / Properties--
    
    /// <summary>
    /// Allows you to limit the speed of the game object.
    /// </summary>
    [SerializeField]
    private float _maxSpeed;
    
    /// <summary>
    /// Controls the random X offset of the Perlin noise graph.
    /// </summary>
    private float _xOffset;

    /// <summary>
    /// Controls the random Y offset of the Perlin noise graph.
    /// </summary>
    private float _yOffset;
    
    /// <summary>
    /// Game object's current speed and direction.
    /// </summary>
    private Vector3 _velocity;
    
    /// <summary>
    /// How fast the game object's velocity is changing.
    /// </summary>
    private Vector3 _acceleration;

    /// <summary>
    /// Cached Transform component.
    /// </summary>
    private Transform _transform;
    
    /// <summary>
    /// Recalculation time for new random offsets of the Perlin noise graph.
    /// </summary>
    private float _time = .1f;
    
    /// <summary>
    /// Recalculation timer to track current _time.
    /// </summary>
    private float _timer;

    #endregion
    
    #region --Unity Specific Methods--

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        //Track recalculation _timer and _time.
        _timer += Time.deltaTime;
        if (_timer > _time)
        {
            CalculatePerlinNoise();
        }
        
        Move();
    }
    
    #endregion
    
    #region --Custom Methods--

    /// <summary>
    /// Handles movement.
    /// </summary>
    private void Move()
    {
        _velocity += _acceleration;
        if (_maxSpeed > 0 && _velocity.magnitude > _maxSpeed)
        {
            _velocity = _velocity.normalized * _maxSpeed;
        }
        _transform.position += _velocity;
        
        _acceleration = Vector3.zero;
    }

    /// <summary>
    /// Calculates a random value for Perlin noise and increments the offsets each calculation.
    /// </summary>
    private void CalculatePerlinNoise()
    {
        float _random = Random.Range(-50.0f, 50.0f);
        float _perlinNoise = Mathf.PerlinNoise(_xOffset, _yOffset);
        
        _acceleration.x = _perlinNoise * _random;

        _random = Random.Range(-50.0f, 50.0f);
        _acceleration.y = _perlinNoise * _random;

        _acceleration.z = Random.Range(_acceleration.x, _acceleration.y);

        _timer = 0;
        _time = Random.Range(.1f, .5f);
        _xOffset += .1f;
        _yOffset += .1f;
    }
    
    #endregion
    
}
