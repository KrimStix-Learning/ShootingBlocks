using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TextMeshProUGUI secondsSurvivedUI;

    bool gameOver;

    void Start()
    {
        FindObjectOfType<PlayerController>().OnPlayerDeath += OnGameOver;
    }
    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        if (secondsSurvivedUI != null)
            secondsSurvivedUI.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();
        else
        {
            Debug.LogWarning("TextMeshProUGUI object is missing or destroyed!");
        }

        gameOver = true;
    }
}
