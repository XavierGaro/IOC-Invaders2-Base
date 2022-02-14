using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public const float MIN_FIRE_RATE = 0.05f;

    private Vector2 rawInput;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] float paddingSides = 0.5f;
    [SerializeField] float paddingTop = 4;
    [SerializeField] float paddingBottom = 2;

    //private WeaponAbstract _weapon;
    private WeaponAbstract[] _weapons;


    // Es fa servir un viewport, des de 0,0 fins a 1,1
    private Vector2 _minBounds;
    private Vector2 _maxBounds;

    private void Start()
    {
        _weapons = GetComponents<WeaponAbstract>();
        //_weapon = GetComponent<WeaponAbstract>();
        
        Camera mainCamera = Camera.main;
        _minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        _maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, _minBounds.x + paddingSides, _maxBounds.x - paddingSides);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, _minBounds.y + paddingBottom, _maxBounds.y - paddingTop);

        transform.position = newPos;
    }

    // Reconocido automáticamente por el InputSystem
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    // Reconocido automáticamente por el InputSystem
    void OnFire(InputValue value)
    {
        // if (_weapon != null)
        // {
        //     _weapon.isFiring = value.isPressed;
        // }
        
        if (_weapons != null)
        {
            foreach (var weapon in _weapons)
            {
                weapon.isFiring = value.isPressed;    
            }
            
        }
    }
}