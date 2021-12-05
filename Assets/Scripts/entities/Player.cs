using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Controls m_Controls;
    public float moveSpeed = 5.0f;
    private Vector3 _direction;
    private CharacterController m_characterController;
    private Plane m_plane;
    private Animator m_animator;
    [SerializeField] private Transform m_modelTransform;

    private Vector3 lookingPoint;

    void OnEnable()
    {
        m_Controls.Enable();
    }

    void OnDisable()
    {
        m_Controls.Disable();
    }


    void Awake()
    {
        m_Controls = new Controls();
        m_characterController = GetComponentInChildren<CharacterController>();
        m_animator = GetComponentInChildren<Animator>();

        m_plane = new Plane(Vector3.up, transform.position);
    }
    
    void Update()
    {
        
        var h = m_Controls.Player.Horizontal.ReadValue<float>();
        var v = m_Controls.Player.Vertical.ReadValue<float>();
        
        HandleMovement(h, v);

        var mousePos = Mouse.current.position.ReadValue();
        RotateTowardsMouse(mousePos);
        
        Animate(h, v);
    }


    private void HandleMovement(float horizontal, float vertical)
    {

        m_characterController.Move(Vector3.Normalize(new Vector3(horizontal, 0, vertical)) * moveSpeed * Time.deltaTime);
    }

    private void RotateTowardsMouse(Vector2 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        float hitDist = 0f;

        if (m_plane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            lookingPoint = targetPoint;
            
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0f;
            targetRotation.z = 0f;
            
            //The magic number "7f" is turnspeed!
            m_modelTransform.rotation = Quaternion.Slerp(m_modelTransform.rotation, targetRotation, 7f * Time.deltaTime);
            
        }
    }
    /*private void OnLeftMouse()
    {
        Debug.Log("Do Stuff");
    }
    */

    private void Animate(float horizontal, float vertical)
    {
        var movement = new Vector3(horizontal, 0f, vertical);
        var velocityX = Vector3.Dot(movement.normalized, transform.right);
        var velocityZ = Vector3.Dot(movement.normalized, transform.forward);

        m_animator.SetFloat("velocityX", velocityX, 0.1f, Time.deltaTime);
        m_animator.SetFloat("velocityZ", velocityZ, 0.1f, Time.deltaTime);
    }

    private void OnAbilityOne()
    {
        Debug.Log("Q was pressed at " + Pointer.current.position.ReadValue());
    }
}
