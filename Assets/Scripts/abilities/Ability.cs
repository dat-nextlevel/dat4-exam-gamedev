using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Ability : MonoBehaviour
{
    public GameObject indicator;
    public float cooldown = 3;

    private float lastUsed;
    protected GameObject _indicator;
    protected Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_indicator)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _indicator.transform.position = hit.point;
            }
        }
    }

    private bool CanFire()
    {
        if (lastUsed + cooldown <= Time.time)
        {
            return true;
        }

        return false;
    }

    public bool Fire()
    {
        if (CanFire())
        {
           Behavior();
           lastUsed = Time.time;
           return true;
        }
        
        
        Debug.Log("on Cooldown");
        return false;
    }
    
    public void CreateIndicator(float size)
    {
        if (_indicator)
        {
            return;
        }
        
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            _indicator = Instantiate(indicator, hit.point, Quaternion.identity);
            _indicator.transform.localScale = new Vector3(size, 0.1f, size);
        }
    }
    
    public void RemoveIndicator()
    {
        if (_indicator)
        {
            Destroy(_indicator);
        }

    }
    
    public abstract void Behavior();
}
