using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tornado : Ability
{
    public GameObject spawnObject;
    
    override 
    public void Behavior()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var obj = Instantiate(spawnObject, hit.point, Quaternion.identity);
            obj.transform.Rotate(0, 180, 0);
        }
    }
}
