using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Agrega una referencia a LevelLoader
    private LevelLoader levelLoader;

    private void Awake()
    {
        // Encuentra la instancia actual de LevelLoader en la escena
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void Menu()
    {
        // Usa LevelLoader para cargar la escena
        levelLoader.LoadNextLevel("MainMenu");
    }

    public void Level1()
    {
        levelLoader.LoadNextLevel("BetaMap");
    }

    public void Death()
    {
        levelLoader.LoadNextLevel("GameOver");
    }

    public void Victory()
    {
        levelLoader.LoadNextLevel("Victory");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}