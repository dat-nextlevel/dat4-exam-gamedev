using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float maxHealth = 20f;

    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onDamageDone += OnDamageDone;
        currentHealth = maxHealth;
    }

    private void OnDamageDone(GameObject entity, float damage)
    {
        if (entity == gameObject)
        {
            Debug.Log("Should do damage: " + damage);
        }
    }
}
