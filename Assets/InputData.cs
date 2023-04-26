using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputData // struct to hold actions for each frame
{
    public int playerIndex;
    public Vector2 movementVector;
    public int FrameCounter;

    public bool AButtonPressed;
    public bool AButtonHeld;
    public bool AButtonReleased;

    public bool BButtonPressed;
    public bool BButtonHeld;
    public bool BButtonReleased;

    public bool CButtonPressed;
    public bool CButtonHeld;
    public bool CButtonReleased;

    public InputData(int playerIndex)
    {
        this.playerIndex = playerIndex;
        this.movementVector = Vector2.zero;
        this.FrameCounter = 0;

        this.AButtonPressed = false;
        this.AButtonHeld = false;
        this.AButtonReleased = false;

        this.BButtonPressed = false;
        this.BButtonHeld = false;
        this.BButtonReleased = false;
            
        this.CButtonPressed = false;
        this.CButtonHeld = false;
        this.CButtonReleased = false;
    }
}
