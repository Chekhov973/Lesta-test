using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _cameraRoot;
    [SerializeField] private float _cameraSpeed = 500;
    [SerializeField] private float _jumpForce = 1;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _offset;
    public Rigidbody Rb => _rb;
    private RaycastHit hit;
    private Vector3 _direction;
    private Vector3 _forward;
    private Vector3 _side;
    private Vector3 _down;
    private Vector3 _additionalForce;
    private Vector3 _up;
    private float _lastMouseData;
    public Vector3 AdditionalForce { get => _additionalForce; set => _additionalForce = value; }


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnValidate()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }
    }

    void FixedUpdate()
    {
        if (_rb.isKinematic)
            return;



        if (Physics.Raycast(transform.position, transform.up * -1, out hit, _offset, _layerMask) && hit.collider.enabled)
        {
            _rb.velocity = Vector3.zero;
            _up = Vector3.zero;
            _side = Input.GetAxis("Horizontal") * transform.right;
            _forward = Input.GetAxis("Vertical") * transform.forward;

            _down = Vector3.Project(_rb.velocity, transform.up);;

            if (Input.GetKey(KeyCode.Space))
            {
                    _up = transform.up * _jumpForce;
            }

            _rb.velocity = _up;
            _rb.AddForce((_side + _forward) * speed + _down, ForceMode.VelocityChange);

        }

        _rb.velocity += _additionalForce;


        float currentMouseData = Input.GetAxis("Mouse X");
        if (_lastMouseData != currentMouseData)
        {
            _lastMouseData = currentMouseData;
            Quaternion target = Quaternion.AngleAxis(currentMouseData * _cameraSpeed * Time.fixedDeltaTime, transform.up);
            transform.rotation = target * transform.rotation;
        }

        float angle = -Input.GetAxis("Mouse Y") * _cameraSpeed * Time.fixedDeltaTime;
        var currentX = _cameraRoot.localRotation.eulerAngles.x;
        if (currentX < 70 || currentX > 290)
        {
            _cameraRoot.Rotate(angle, 0, 0);
        }
        else if (currentX >= 70 && currentX < 180)
        {
            _cameraRoot.localRotation = Quaternion.Euler(69, 0, 0);
        }
        else if (currentX <= 290 && currentX > 180)
        {
            _cameraRoot.localRotation = Quaternion.Euler(291, 0, 0);

        }
    }
}
