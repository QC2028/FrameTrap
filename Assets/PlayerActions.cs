using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlayerActions : MonoBehaviour //this class controls the players actions based on inputs that are passed in from the player controller
{
    List<InputData> inputHistory; //new list of inputdata struct
    InputData latestInput; //empty inputdata to hold latest input when recieved

    Vector3 newVector = Vector3.zero; //used to move player
    private bool isBusy = false; //used to check if player cannot do other actions because they are doing something else

    [SerializeField] private float moveSpeed = 0.1f; //player movement multiplier
    [SerializeField] private int inputReaderLength = 15; //how far back the inputs are held for special moves
    [SerializeField] private int inputBufferLength = 5; //how far before a player stops being busy that the inputs are read

    
    private void Awake()
    {
        inputHistory = new List<InputData>(); //new list of inputdata struct
    }


    private void FixedUpdate()
    {

        if(inputHistory.Count > inputReaderLength) 
        {
            latestInput = inputHistory[inputHistory.Count - 1];

            //MOVEMENT
            if (!isBusy) //if not attacking, move player in space (but can still read inputs if attacking)
            {
                newVector = new Vector3(latestInput.movementVector.x * moveSpeed, 0, 0);
                transform.position += newVector;
            }

            //A BUTTON
            if (latestInput.AButtonPressed)
            {
                
            }
            if (latestInput.AButtonHeld)
            {
                
            }
            if (latestInput.AButtonReleased)
            {
                
            }

            //B BUTTON
            if (latestInput.BButtonPressed)
            {

            }
            if (latestInput.BButtonHeld)
            {

            }
            if (latestInput.BButtonReleased)
            {

            }

            //C BUTTON
            if (latestInput.CButtonPressed)
            {

            }
            if (latestInput.CButtonHeld)
            {

            }
            if (latestInput.CButtonReleased)
            {

            }

            //Debug.Log("index-" + latestInput.playerIndex + " frame-" + latestInput.FrameCounter + " vector-" + latestInput.movementVector + " APress-" + latestInput.AButtonPressed + " AHold-" + latestInput.AButtonHeld + " ARel-" + latestInput.AButtonReleased);
        }
    }

    public void InputAction(InputData input) //pass in input data from player controller
    {
        inputHistory.Add(input); //add this struct to the input history
    }
}

