using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{
    [Header("Panel del menú de pausa")]
    [SerializeField] GameObject pausePanel;

    private bool _isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused) Resume();
            else Pause();
        }
    }

    // ── Pausar ────────────────────────────────────────────────────────────────
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;   // congela el juego
        _isPaused = true;
        Debug.Log("[PAUSE] Juego pausado.");
    }

    // ── Reanudar ──────────────────────────────────────────────────────────────
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;   // reactiva el juego
        _isPaused = false;
        Debug.Log("[PAUSE] Juego reanudado.");
    }

    // ── Guardar ───────────────────────────────────────────────────────────────
    public void Save()
    {
        // Llama al SaveSystem que ya tienes
        FindObjectOfType<SaveSystem>().SaveGame();
        Debug.Log("[PAUSE] Partida guardada desde el menú.");
    }

    // ── Salir ─────────────────────────────────────────────────────────────────
    public void QuitGame()
    {
        Time.timeScale = 1f;   // restaura antes de salir
        Application.Quit();
        Debug.Log("[PAUSE] Saliendo del juego.");  // solo visible en el editor
    }
}
