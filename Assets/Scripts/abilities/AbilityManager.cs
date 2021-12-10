using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    private Ability abilityOne;
    private Controls m_controls;
    private Ability currentAbility;

    private void OnEnable()
    {
        m_controls.Enable();
    }
    
    private void OnDisable()
    {
        m_controls.Disable();
    }

    private void Awake()
    {
        m_controls = new Controls();
    }

    void Start()
    {
        // Little scuffed but will do.
        abilityOne = GetComponent<Tornado>();

        m_controls.Player.LeftMouse.performed += ctx => FireCurrentAbility();
        m_controls.Player.AbilityOne.performed += ctx => AbilityOne();
    }

    private void AbilityOne()
    {
        currentAbility = abilityOne;
        currentAbility.CreateIndicator(3);
    }


    private void FireCurrentAbility()
    {
        if (currentAbility == null)
        {
            return;
        }

        if (currentAbility.Fire())
        {
            currentAbility.RemoveIndicator();
            currentAbility = null;
        }

    }

}
