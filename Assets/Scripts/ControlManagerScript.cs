using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManagerScript : MonoBehaviour
{
    private Controls input = null;
    private Vector2 inputVector = Vector2.zero;


    void Awake()
    {
        input = new Controls();
    }


    void Update()
    {

         
    }

    private void FixedUpdate()
    {

    }    

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnInputPerformed;
        input.Player.Movement.canceled += OnInputCanceled;
    }
    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnInputPerformed;
        input.Player.Movement.canceled -= OnInputCanceled;
    }

    private void OnInputPerformed(InputAction.CallbackContext ctx)
    {
        inputVector = ctx.ReadValue<Vector2>();
    }

    private void OnInputCanceled(InputAction.CallbackContext ctx)
    {
        inputVector = Vector2.zero;
    }
}
