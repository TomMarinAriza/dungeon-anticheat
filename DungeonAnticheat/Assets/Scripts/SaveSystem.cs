using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("Referencias (igual que antes)")]
    public Transform    player;
    public PlayerHealth playerHealth;

    // ── Ruta del archivo JSON ─────────────────────────────────────────────────
    // Se guarda en: C:\Users\TU_USUARIO\AppData\LocalLow\...\savegame.json
    // Ábrelo con el Bloc de notas para la demo del anticheat
    private string SavePath => Path.Combine(Application.persistentDataPath, "savegame.json");

    // Seed única por sesión — impide que el tramposo pre-calcule el hash
    private static readonly string SessionSeed = System.Guid.NewGuid().ToString();

    // ── Estructura del JSON ───────────────────────────────────────────────────
    [System.Serializable]
    private class SaveData
    {
        public float  x;
        public float  y;
        public int    health;
        public string hash;      // ← el anticheat vive aquí
        public string savedAt;
    }

    // ── GUARDAR ───────────────────────────────────────────────────────────────
    public void SaveGame()
    {
        var data = new SaveData
        {
            x       = player.position.x,
            y       = player.position.y,
            health  = playerHealth.currentHealth,
            savedAt = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        // El hash se calcula DESPUÉS de asignar los valores
        data.hash = ComputeHash(data.health, data.x, data.y);

        string json = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(SavePath, json);

        Debug.Log($"[SAVE] Juego guardado en: {SavePath}");
        Debug.Log($"[SAVE] Contenido:\n{json}");
    }

    // ── CARGAR ────────────────────────────────────────────────────────────────
    public void LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("[SAVE] No hay archivo de guardado.");
            return;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // ── VERIFICACIÓN DE INTEGRIDAD ─────────────────────────────────────
        string expectedHash = ComputeHash(data.health, data.x, data.y);

        if (data.hash != expectedHash)
        {
            // ¡Trampa detectada! El hash del archivo no coincide con los valores
            Debug.LogError("[ANTICHEAT] ¡TRAMPA DETECTADA! El archivo fue modificado.");
            Debug.LogError($"  Health en archivo : {data.health}");
            Debug.LogError($"  Hash en archivo   : {data.hash}");
            Debug.LogError($"  Hash esperado     : {expectedHash}");

            // No carga los valores manipulados — rechaza la partida
            return;
        }

        // ── CARGA EXITOSA ──────────────────────────────────────────────────
        player.position            = new Vector2(data.x, data.y);
        playerHealth.currentHealth = data.health;

        Debug.Log("[SAVE] ✓ Juego cargado. Integridad verificada.");
    }

    // ── HASH SHA-256 ──────────────────────────────────────────────────────────
    private string ComputeHash(int health, float x, float y)
    {
        // Combina los valores críticos + seed de sesión
        string raw = $"{health}:{x}:{y}:{SessionSeed}";
        using var sha   = SHA256.Create();
        byte[]    bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
        return System.Convert.ToBase64String(bytes);
    }

    // ── UTILIDAD: abre la carpeta del archivo ──────────────────────────────────
    // Llama esto desde un botón en el juego para la demo
    public void OpenSaveFolder()
    {
        Application.OpenURL("file://" + Application.persistentDataPath);
    }

    private void Start()
    {
        LoadGame(); // Intenta cargar al iniciar el juego
        Debug.Log($"[SAVE] Archivo en: {SavePath}");
    }
}