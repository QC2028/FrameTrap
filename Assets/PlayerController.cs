using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour //this class recieves the inputs from the input handler and sends the data to player actions each frame
{
    [SerializeField] private int playerIndex = 0; //this is what the inputhandler can see and attaches controllers based on this
    public PlayerActions actions;
    Vector2 moveVector = Vector2.zero;
    bool AButtonPressed = false;
    bool BButtonPressed = false;
    bool CButtonPressed = false;
    int FrameCounter = 0;



    void FixedUpdate()
    {
        FrameCounter++; //new frame
        actions.InputAction(moveVector, AButtonPressed, BButtonPressed, CButtonPressed, FrameCounter, playerIndex); //send input action for this frame
        AButtonPressed = false; //reset button press booleans
        BButtonPressed = false;
        CButtonPressed = false;
    }

    public int GetPlayerIndex() 
    {
        return playerIndex;
    }

    public void SetMoveVector(Vector2 input) //input functions to be accessed by player input handler
    {
        moveVector = input;        
    }
    public void AButtonPress()
    {
        AButtonPressed = true;       
    }
    public void BButtonPress()
    {
        BButtonPressed = true;
    }
    public void CButtonPress()
    {
        CButtonPressed = true;
    }
    public void Pause()
    {
        Debug.Log("pause button pressed");
    }
}
