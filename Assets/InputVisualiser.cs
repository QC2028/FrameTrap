using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputVisualiser : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject joystick; //circle in the middle of visualiser
    public GameObject aButton;
    public GameObject bButton;
    public GameObject cButton;

    Vector3 originalVector = Vector3.zero;
    Vector3 tempVector = Vector3.zero;

    private void Awake()
    {
        originalVector = joystick.transform.position;
    }

    void Update()
    {
        joystick.transform.position = originalVector; //reset circle position back to middle before calculating next position
        tempVector = playerController.newInput.movementVector.normalized * 2.2f; //normalise vector to show circle rather than square
        joystick.transform.Translate(tempVector);

        aButton.SetActive(false); bButton.SetActive(false); cButton.SetActive(false);


        if(playerController.newInput.AButtonHeld) //highlights button presses
        {
            aButton.SetActive(true);
        }
        if (playerController.newInput.BButtonHeld)
        {
            bButton.SetActive(true);
        }
        if (playerController.newInput.CButtonHeld)
        {
            cButton.SetActive(true);
        }

    }
}
