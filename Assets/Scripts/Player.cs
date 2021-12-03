using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Controls m_Controls;
    private float moveSpeed = 5.0f;
    private Vector3 _direction;
    private CharacterController m_characterController;
    private Plane m_plane;
    private Animator m_animator;
    [SerializeField] private Transform m_modelTransform;

    private bool isMoving = false;

    void OnEnable()
    {
        m_Controls.Enable();
        m_animator = GetComponent<Animator>();
    }

    void OnDisable()
    {
        m_Controls.Disable();
    }


    void Awake()
    {
        m_Controls = new Controls();
        m_characterController = GetComponent<CharacterController>();

        m_plane = new Plane(Vector3.up, transform.position);
    }

    void Update()
    {
        var h = m_Controls.Player.Horizontal.ReadValue<float>();
        var v = m_Controls.Player.Vertical.ReadValue<float>();
        var mousePos = Mouse.current.position.ReadValue();

        if (h != 0f || v != 0f)
        {
            if (!isMoving)
            {
                isMoving = true;
                m_animator.SetBool("isMoving", true);
            }
        }
        else
        {
            if (isMoving)
            {

                isMoving = false;
                m_animator.SetBool("isMoving", false);
            }
        }

        m_characterController.Move(new Vector3(h, 0.0f, v) * moveSpeed * Time.deltaTime);

        RotateTowardsMouse(mousePos);
    }

    private void RotateTowardsMouse(Vector2 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        float hitDist = 0f;

        if (m_plane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
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

    private void OnAbilityOne()
    {
        Debug.Log("Q was pressed at " + Pointer.current.position.ReadValue());
    }
}
