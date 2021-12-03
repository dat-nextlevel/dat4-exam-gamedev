using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    private Camera cam;
    private Controls m_Controls;

    private void OnEnable()
    {
        m_Controls.Enable();
    }

    private void OnDisable()
    {
        m_Controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    void Awake()
    {
        m_Controls = new Controls();
        m_Controls.Player.LeftMouse.performed += Tornado;
    }

    void Update()
    {

    }



    private void Tornado(InputAction.CallbackContext ctx)
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var obj = Instantiate(spawnObject, hit.point, Quaternion.identity);
            obj.transform.Rotate(0, 180, 0);
        }
    }
}
