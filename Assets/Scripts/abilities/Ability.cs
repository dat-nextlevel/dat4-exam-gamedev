using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Ability : MonoBehaviour
{
    public GameObject indicator;
    public float cooldown = 3;
    public float duration = 0;

    private float lastUsed;
    protected GameObject _indicator;
    protected Camera cam;

    private Dictionary<GameObject, float> spawnedEntities = new Dictionary<GameObject, float>();
    private Dictionary<GameObject, float> collidingEntities = new Dictionary<GameObject, float>();
    
    private void Awake()
    {
        cam = Camera.main;
    }
    
    // Update is called once per frame
    protected virtual void Update()
    {
        handleIndicator();
        handleShouldDespawn();
        handleCollidingBehavior();
    }

    private bool CanFire()
    {
        if (lastUsed == 0)
        {
            return true;
        }
        if(lastUsed + cooldown <= Time.time)
        {
            return true;
        }

        return false;
    }

    public bool Fire()
    {
        if (CanFire())
        {
           var obj = SpawnBehavior();
           lastUsed = Time.time;

           spawnedEntities.Add(obj, Time.time);
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

    private void handleIndicator()
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

    private void handleShouldDespawn()
    {
        if (duration > 0)
        {
            for (int i = spawnedEntities.Count - 1; i >= 0; i--)
            {
                var entity = spawnedEntities.ElementAt(i);
                if (duration + entity.Value <= Time.time)
                {
                    var ability = entity.Key.GetComponent<Ability>();
                    ability.NoCollision(entity.Key); // Cancel a collision if player was inside.

                    spawnedEntities.Remove(entity.Key);
                    Destroy(entity.Key);
                }
            }

            
        }   
    }

    private void handleCollidingBehavior()
    {
        for (int i = collidingEntities.Count - 1; i >= 0; i--)
        {
            var entity = collidingEntities.ElementAt(i);
            var didBehavior = CollidingBehavior(entity);
            if (didBehavior)
            {
                collidingEntities[entity.Key] = Time.time;
            }
        }
        
    }
    
    public void Collision(GameObject entity)
    {
        collidingEntities.Add(entity, 0f);
    }
    
    public void NoCollision(GameObject entity)
    {
        collidingEntities.Remove(entity);
    }
    
    
    public abstract GameObject SpawnBehavior();
    public abstract bool CollidingBehavior(KeyValuePair<GameObject, float> entity);
    


}
