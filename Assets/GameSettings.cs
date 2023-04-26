using UnityEngine;

public class GameSettings : MonoBehaviour // this class sets the game to the right framerate andchanges teh fixedupdate timing to 60fps
{
    void Start()
    {
        Time.fixedDeltaTime = 1.0f / 60.0f; //set fixed update to 60fps
        //QualitySettings.vSyncCount = 0; //turn off vsync
        Application.targetFrameRate = 60; //set update to 60fps
        //Debug.Log("fixedupdate & fps: "+Time.fixedDeltaTime + " & " + Application.targetFrameRate);
    }
}
