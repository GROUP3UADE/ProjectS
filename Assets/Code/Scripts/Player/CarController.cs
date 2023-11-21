using UnityEngine;

public class CarController : MonoBehaviour
{
    #region Serializables

    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _aceleration;
    [SerializeField]
    private CharacterControl _input;
    #endregion

    #region Componentes

    private Rigidbody2D _rb;
    private Animator _anim;

    #endregion
    
    /// <summary>
    /// Determina la direccion en la que quedo mirando el auto. 
    /// Para animaciones y sprite.
    /// </summary>
    public Vector2 Dir { get; private set; }

    private void Awake()
    {
        _input = new CharacterControl();
        _input.Car.Traction.performed += trac => ApplyTraction(trac.ReadValue<int>());
        _input.Car.Rotation.performed += trac => ApplyRotation(trac.ReadValue<int>());
        Dir = Vector2.up;
    }
    // Use this for initialization
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void ApplyTraction(int t) => _rb.AddForce(new Vector2(t, 0) * _aceleration);

    private void ApplyRotation(int r)
    {
        transform.Rotate(r > 0 ? new Vector3(0, -45) : new Vector3(0, 45));
        print("rota");
    }

    private void FixedUpdate() => _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _maxSpeed);

    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }
}