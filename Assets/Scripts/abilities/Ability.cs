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
            if (CanFire())
            {
                _indicator.GetComponent<MeshRenderer>().sharedMaterial.SetColor("BaseColor", new Color(0.5f, 1, 0));
            }
            else
            {
                _indicator.GetComponent<MeshRenderer>().sharedMaterial.SetColor("BaseColor", new Color(1, 0, 0.18f));

            }
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var point = hit.point;
                point.y = 0.5f;
                _indicator.transform.position = point;
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
            // Why do I need to do this?
            var point = hit.point;
            point.y = indicator.transform.localScale.y;
            
            _indicator = Instantiate(indicator, point, Quaternion.identity);
            _indicator.transform.localScale = new Vector3(size, _indicator.transform.localScale.y, size);
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
