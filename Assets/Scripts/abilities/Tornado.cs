using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Tornado : Ability
{
    public GameObject spawnObject;

    private float lastDamageTick;
    private bool isColliding = false;

    override 
    public GameObject Behavior()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var obj = Instantiate(spawnObject, hit.point, Quaternion.identity);
            obj.transform.Rotate(0, 180, 0);

            return obj;
        }

        return null;
    }

    public void Update()
    {
        base.Update();

        if (isColliding && lastDamageTick + 2 <= Time.time)
        {
            GameEvents.current.DamageDone(5);
            lastDamageTick = Time.time;
        }
    }

    override 
    public void Collision()
    {
        isColliding = true;
    }

    override 
    public void NoCollision()
    {
        isColliding = false;
    }
    
}
