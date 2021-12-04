using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    private Controls m_controls;
    private Ability currentAbility;
    
    private Ability abilityOne;

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

        abilityOne = GetComponent<Tornado>();
    }

    void Start()
    {
        m_controls.Player.LeftMouse.performed += ctx => FireCurrentAbility();
        m_controls.Player.AbilityOne.performed += ctx => AbilityOne();
    }

    private void AbilityOne()
    {
        currentAbility = abilityOne;
        currentAbility.CreateIndicator(5);
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
