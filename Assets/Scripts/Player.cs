using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{
    [Header("Combat")] 
    public float baseDamage;
    public float baseAttackCooldown;
    private float currentAttackCooldown;
    
    [Header("Movement")]
    public float accel;
    public float deccel;
    public float maxSpeed;
    
    private Vector3 velocity;
    
    private Vector2 lastTouchPos;
    private Vector2 touchVelocity;

    private List<Enemy> _targetsInRange;

    public override void Initialize()
    {
        base.Initialize();
        lastTouchPos = Vector2.zero;
        AddWeapon<Weapon_Basic>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            ProcessTouch();
        }
        else if (Input.GetMouseButton(0))
        {
            ProcessMouse();
        }
        else
        {
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, deccel * Time.deltaTime);
        }

        transform.position += velocity * Time.deltaTime;
        if (velocity.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(velocity.normalized);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (weapons.Any(x => x is Weapon_Basic))
            {
                RemoveWeapon<Weapon_Basic>();
            }
            else
            {
                AddWeapon<Weapon_Basic>();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weapons.Any(x => x is Weapon_Tracking))
            {
                RemoveWeapon<Weapon_Tracking>();
            }
            else
            {
                AddWeapon<Weapon_Tracking>();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (weapons.Any(x => x is Weapon_Ricochet))
            {
                RemoveWeapon<Weapon_Ricochet>();
            }
            else
            {
                AddWeapon<Weapon_Ricochet>();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (weapons.Any(x => x is Weapon_Piercing))
            {
                RemoveWeapon<Weapon_Piercing>();
            }
            else
            {
                AddWeapon<Weapon_Piercing>();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (weapons.Any(x => x is Weapon_Boomerang))
            {
                RemoveWeapon<Weapon_Boomerang>();
            }
            else
            {
                AddWeapon<Weapon_Boomerang>();
            }
        }
    }

    private void ProcessTouch()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            touchVelocity = touch.position - lastTouchPos;
            lastTouchPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Began)
        {
            lastTouchPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            lastTouchPos = Vector2.zero;
        }

        velocity += new Vector3(touchVelocity.x, 0, touchVelocity.y) * (accel * Time.deltaTime);
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }

    private void ProcessMouse()
    {
        touchVelocity = (Vector2)Input.mousePosition - lastTouchPos;
        lastTouchPos = Input.mousePosition;
        
        velocity += new Vector3(touchVelocity.x, 0, touchVelocity.y) * (accel * Time.deltaTime);
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }
    
}
