using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCollisionHandler : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Entity"))
        {
            var ability = GetComponent<Ability>();
            ability.Collision(other.gameObject);
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Entity"))
        {
            var ability = GetComponent<Ability>();
            ability.NoCollision(other.gameObject);
        }
    }
}
