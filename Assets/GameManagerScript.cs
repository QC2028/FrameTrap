using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public PlayerActions player1;
    public PlayerActions player2;
    public GameObject winScreen;
    public GameObject oldCanvas;
    public TextMeshProUGUI winText;

    void Update()
    {
        if (player1.playerHealth <= 0)
        {
            oldCanvas.SetActive(false);
            winScreen.SetActive(true);
            winText.text = "PLAYER 2 WINS";
        }
        else if (player2.playerHealth <= 0)
        {
            oldCanvas.SetActive(false);
            winScreen.SetActive(true);
            winText.text = "PLAYER 2 WINS";
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
