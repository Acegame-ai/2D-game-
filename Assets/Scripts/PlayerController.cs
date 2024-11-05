using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] Text ammoDisplay;
    [SerializeField] Text killCountDisplay;

    Rigidbody2D _rb;
    Camera _mainCamera;

    float _moveVertical;
    float _moveHorizontal;
    float _moveSpeed = 5f;
    float _speedLimiter = 0.7f;
    Vector2 _moveVelocity;
    Vector2 _currentVelocity;

    Vector2 _mousePos;
    Vector2 _offset;

    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _bulletSpawn;
    private int _bulletamnt = 100;
    private int _currentAmmo;
    bool _disablePlayer = false;

    float _bulletSpeed = 15f;
    float _fireRate = 0.1f;
    float _nextFireTime = 0f;

    private float defaultFireRate = 0.1f;
    private float currentFireRate;
    private bool isSpeedReduced = false;
    private float defaultSpeed;
    private float reducedSpeed = 2.5f;
    private float increasedDamageMultiplier = 20f;
    private float defaultDamage = 10f;

    float _playerHealth = 100f;
    private bool isInvincible = false;

   

    IEnumerator Damaged()
    {
        isInvincible = true;
        _disablePlayer = true;
        yield return new WaitForSeconds(0.5f);
        _disablePlayer = false;
        isInvincible = false;
    }

    public int GetCurrentAmmo()
    {
        return _currentAmmo;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
        _currentAmmo = _bulletamnt;
        UpdateAmmoDisplay();
        currentFireRate = defaultFireRate;
        defaultSpeed = _moveSpeed;
    }

    void Update()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");

        _moveVelocity = new Vector2(_moveHorizontal, _moveVertical) * _moveSpeed;

        if (Input.GetMouseButton(0) && Time.time >= _nextFireTime)
        {
            if (_currentAmmo > 0)
            {
                _nextFireTime = Time.time + currentFireRate; // Use currentFireRate
                StartCoroutine(Fire());
            }
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ammo"))
        {
            AddMoreBullets();
            Debug.Log("Bullet amount increased");
        }
    }

    void MovePlayer()
    {
        if (_moveHorizontal != 0 || _moveVertical != 0)
        {
            if (_moveHorizontal != 0 && _moveVertical != 0)
            {
                _moveVelocity *= _speedLimiter;
            }
            _rb.velocity = Vector2.SmoothDamp(_rb.velocity, _moveVelocity, ref _currentVelocity, 0.1f);
        }
        else
        {
            _rb.velocity = Vector2.SmoothDamp(_rb.velocity, Vector2.zero, ref _currentVelocity, 0.1f);
        }
    }

    void RotatePlayer()
    {
        _mousePos = Input.mousePosition;
        Vector3 screenPoint = _mainCamera.WorldToScreenPoint(transform.localPosition);
        _offset = new Vector2(_mousePos.x - screenPoint.x, _mousePos.y - screenPoint.y).normalized;

        float angle = Mathf.Atan2(_offset.y, _offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    IEnumerator Fire()
    {
        _currentAmmo--;
        UpdateAmmoDisplay();
        GameObject bullet = Instantiate(_bullet, _bulletSpawn.position, Quaternion.identity);
        bullet.layer = LayerMask.NameToLayer("PlayerBullet"); // Assign PlayerBullet layer
        bullet.GetComponent<Rigidbody2D>().velocity = _offset * _bulletSpeed;
        yield return new WaitForSeconds(3);
        Destroy(bullet);
    }

    public void IncreaseFireRate()
    {
        currentFireRate = defaultFireRate * 0.5f; // Faster fire rate
        StartCoroutine(ResetFireRateAfterDuration());
    }

    public void AddMoreBullets()
    {
        _currentAmmo += 50; // Increase ammo count
        UpdateAmmoDisplay();
    }

    public void IncreaseDamage()
    {
        _bulletSpeed = defaultDamage * increasedDamageMultiplier; // Increase damage
        StartCoroutine(ResetDamageAfterDuration());
    }

    public void ReduceSpeed()
    {
        if (!isSpeedReduced)
        {
            _moveSpeed = reducedSpeed;
            isSpeedReduced = true;
            StartCoroutine(ResetSpeedAfterDuration());
        }
    }

    private IEnumerator ResetFireRateAfterDuration()
    {
        yield return new WaitForSeconds(15f);
        currentFireRate = defaultFireRate;
    }

    private IEnumerator ResetDamageAfterDuration()
    {
        yield return new WaitForSeconds(15f);
        _bulletSpeed = defaultDamage;
    }

    private IEnumerator ResetSpeedAfterDuration()
    {
        yield return new WaitForSeconds(15f);
        _moveSpeed = defaultSpeed;
        isSpeedReduced = false;
    }

    void UpdateAmmoDisplay()
    {
        if (ammoDisplay != null)
        {
            ammoDisplay.text = "Ammo: " + _currentAmmo.ToString();
        }
    }
}
