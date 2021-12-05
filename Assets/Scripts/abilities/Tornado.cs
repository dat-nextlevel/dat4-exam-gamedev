using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Tornado : Ability
{
    public GameObject spawnObject;
    private bool isColliding;

    public float tickDamageCooldown = 3f;

    override 
    public GameObject SpawnBehavior()
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

    public override bool CollidingBehavior(KeyValuePair<GameObject, float> entity)
    {
        if (entity.Value == 0 || tickDamageCooldown + entity.Value <= Time.time)
        {
            GameEvents.current.DamageDone(entity.Key, 7);
            return true;
        }

        return false;

    }
}
