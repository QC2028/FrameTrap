using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour //this class recieves the inputs from the input handler and sends the data to player actions each frame
{
    [SerializeField] private int playerIndex = 0; //this is what the inputhandler can see and attaches controllers based on this
    public PlayerActions actions;

    public InputData newInput;


    private void Awake()
    {
        newInput = new InputData(playerIndex);
    }

    void FixedUpdate()
    {
        newInput.FrameCounter++; //new frame
        actions.InputAction(newInput); //send input action for this frame
        newInput.AButtonPressed = false; //reset button press and release booleans
        newInput.AButtonReleased = false;
        newInput.BButtonPressed = false;
        newInput.BButtonReleased = false;
        newInput.CButtonPressed = false;
        newInput.CButtonReleased = false;
    }

    public int GetPlayerIndex() 
    {
        return playerIndex;
    }

    public void SetMoveVector(Vector2 input) //input functions to be accessed by player input handler
    {
        newInput.movementVector = input;
    }
    public void AButtonPress()
    {
        newInput.AButtonPressed = true; 
        newInput.AButtonHeld = true; //start holding on press
    }
    public void AButtonRelease()
    {
        newInput.AButtonReleased = true;
        newInput.AButtonHeld = false; //stop holding on release
    }

    public void BButtonPress()
    {
        newInput.BButtonPressed = true;
        newInput.BButtonHeld = true;
    }
    public void BButtonRelease()
    {
        newInput.BButtonReleased = true;
        newInput.BButtonHeld = false;
    }

    public void CButtonPress()
    {
        newInput.CButtonPressed = true;
        newInput.CButtonHeld = true;
    }
    public void CButtonRelease()
    {
        newInput.CButtonReleased = true;
        newInput.CButtonHeld = false;
    }

    public void Pause()
    {
        Debug.Log("pause button pressed");
    }
}
