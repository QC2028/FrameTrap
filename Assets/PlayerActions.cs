using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlayerActions : MonoBehaviour //this class controls the players actions based on inputs that are passed in from the player controller
{
    List<InputData> inputHistory; //new list of inputdata struct

    Vector2 moveVector = Vector2.zero;
    bool AButtonIsPressed = false;
    bool BButtonIsPressed = false;
    bool CButtonIsPressed = false;



    Vector3 newVector = Vector3.zero;
    private bool isAttacking = false;

    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private int inputReaderLength = 15;


    
    
    
    
    
    private void Awake()
    {
        inputHistory = new List<InputData>(); //new list of inputdata struct
    }


    private void FixedUpdate()
    {

        if(inputHistory.Count > inputReaderLength)
        {
            //if (!isAttacking) //if not attacking, move player in space (but can still read inputs if attacking)
            //{
            //    newVector = new Vector3(moveVector.x * moveSpeed, 0, 0);
            //    transform.position += newVector;
            //}
            if (!isAttacking) //if not attacking, move player in space (but can still read inputs if attacking)
            {
                newVector = new Vector3(inputHistory[inputHistory.Count - 1].movementVector.x * moveSpeed, 0, 0);
                transform.position += newVector;
            }

            if (inputHistory[inputHistory.Count - 1].AButtonPressed)
            {
                Debug.Log("A Button Pressed");
            }
            if (inputHistory[inputHistory.Count - 1].BButtonPressed)
            {
                Debug.Log("B Button Pressed");
            }
            if (inputHistory[inputHistory.Count - 1].CButtonPressed)
            {
                Debug.Log("C Button Pressed");
            }
        }

        //Debug.Log(inputHistory.Count);
        //Debug.Log(inputHistory.Count + " " + inputHistory[inputHistory.Count - 1].playerIndex);
        //Debug.Log("pi: " + inputHistory[inputHistory.Count - 1].playerIndex + " Frame: " + inputHistory[inputHistory.Count - 1].FrameCounter + ", Vector: " + inputHistory[inputHistory.Count - 1].movementVector + ", A: " + inputHistory[inputHistory.Count - 1].AButtonPressed + ", B: " + inputHistory[inputHistory.Count - 1].BButtonPressed + ", C: " + inputHistory[inputHistory.Count - 1].CButtonPressed);
    }

    public void InputAction(Vector2 movementVector, bool AButtonPressed, bool BButtonPressed, bool CButtonPressed, int FrameCounter, int playerIndex) //pass in input data from player controller
    {
        InputData inputData = new InputData(); //create new inputdata struct and fill with passed in data
        inputData.playerIndex = playerIndex;
        inputData.movementVector = movementVector;
        inputData.AButtonPressed = AButtonPressed;
        inputData.BButtonPressed = BButtonPressed;
        inputData.CButtonPressed = CButtonPressed;
        inputData.FrameCounter = FrameCounter;
        inputHistory.Add(inputData); //add this struct to the input history

        //moveVector = movementVector; //set current movement
        //AButtonIsPressed = AButtonPressed;
        //BButtonIsPressed = BButtonPressed;
        //CButtonIsPressed = CButtonPressed;
    }

    void inputAttack()
    {

    }
}

struct InputData // struct to hold actions for each frame
{
    public int playerIndex;
    public Vector2 movementVector;
    public bool AButtonPressed;
    public bool BButtonPressed;
    public bool CButtonPressed;
    public int FrameCounter;
}
