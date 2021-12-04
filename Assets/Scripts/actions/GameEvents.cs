using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<float> onDamageDone;

    public void DamageDone(float damage)
    {
        if (onDamageDone != null)
        {
            onDamageDone(damage);
        }
    }
}
