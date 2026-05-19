using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{
    [Header("Panel del menú de pausa")]
    [SerializeField] private GameObject pausePanel;

    [Header("Referencias")]
    [SerializeField] private SaveSystem saveSystem; // ← asígnalo en el Inspector

    private bool _isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused) Resume();
            else           Pause();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void Save()
    {
        // Verifica que la referencia existe antes de llamarla
        if (saveSystem == null)
        {
            Debug.LogError("[PAUSE] SaveSystem no asignado en el Inspector.");
            return;
        }

        saveSystem.SaveGame();
        Debug.Log("[PAUSE] Partida guardada.");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }


    void Start()
    {
        pausePanel.SetActive(false);
    }
}
