using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisionAbilityHandler : MonoBehaviour
{
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AbilityArea"))
        {
            var ability = other.GetComponent<Ability>();
            ability.Collision();
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AbilityArea"))
        {
            var ability = other.GetComponent<Ability>();
            ability.NoCollision();
        }
    }


}
