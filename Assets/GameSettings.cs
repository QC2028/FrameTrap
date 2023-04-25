using UnityEngine;

public class GameSettings : MonoBehaviour
{
    void Start()
    {
        Time.fixedDeltaTime = 1.0f / 60.0f; //set fixed update to 60fps
        //QualitySettings.vSyncCount = 0; //turn off vsync
        Application.targetFrameRate = 60; //set update to 60fps
        Debug.Log("fixedupdate & fps: "+Time.fixedDeltaTime + " & " + Application.targetFrameRate);
    }
}
