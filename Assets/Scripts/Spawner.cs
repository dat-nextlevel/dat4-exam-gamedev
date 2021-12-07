using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    private Camera cam;
    private Controls m_Controls;
    private readonly int _destroyDelay = 10;
    private bool isCasting = false;
    [SerializeField] GameObject prefab;
    private Material previewMaterial;
    private GameObject preview;


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
        m_Controls.Player.AbilityOne.performed += OnAbilityOne;
        //m_Controls.Player.LeftMouse.performed += Tornado;
    }

    void Update()
    {
        
    }
    
    private void OnAbilityOne(InputAction.CallbackContext ctx)
    {
        /*isCasting = true;
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Transform obj = (Transform) Instantiate(prefab);
        }
        */
        ActivateCastingMode();
        if (isCasting)
        {
            bool isCasted = false;
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                preview = Instantiate(prefab, hit.point, Quaternion.identity);
                preview.transform.position = hit.point;
                if (m_Controls.Player.LeftMouse.triggered)
                {
                    Tornado();
                }
            }
        }
    }

    private void ActivateCastingMode()
    {
        isCasting = true;
    }

    private void DeactivateCastingMode()
    {
        isCasting = false;
    }

    private void Tornado()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject tempObject = Instantiate(spawnObject, hit.point, Quaternion.identity);
            Destroy(tempObject, _destroyDelay);
        }
    }
}
