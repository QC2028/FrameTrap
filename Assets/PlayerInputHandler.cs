using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour //this class is instantiated as a prefab for players that join into the game via unity input system, it sends inputs to the player gameobjects by identifying the player id
{
    private PlayerController playerController;
    private PlayerInput pi;
    private int index;  
    
    private void Awake()
    {
        pi = GetComponent<PlayerInput>(); //find player input components
        var controllers = FindObjectsOfType<PlayerController>(); 
        index = pi.playerIndex;
        playerController = controllers.FirstOrDefault(m => m.GetPlayerIndex() == index); //https://www.youtube.com/watch?v=2YhGK-PXz7g
    }

    public void OnMovement(InputAction.CallbackContext ctx) // input actions to be accessed by unity input manager
    {
        playerController.SetMoveVector(ctx.ReadValue<Vector2>());
    }
    public void OnAButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerController.AButtonPress();
        }
        if (ctx.canceled)
        {

        }
        
    }
    public void OnBButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerController.BButtonPress();
        }
        if (ctx.canceled)
        {

        }

    }
    public void OnCButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerController.CButtonPress();
        }
        if (ctx.canceled)
        {

        }

    }
    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerController.Pause();
        }
        if (ctx.canceled)
        {

        }

    }
}
