using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    private FirebaseFirestore db;

    [Header("UI References (Optional)")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text survivalTimeText;
    [SerializeField] private TMP_Text bonusText;

    private const string CollectionName = "PlayerScores";

    private void Awake()
    {
        InitializeSingleton();
        InitializeDatabase();
    }

    private void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void InitializeDatabase()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    /// <summary>
    /// Guarda los datos de un jugador en la colección PlayerScores
    /// </summary>
    public async Task SavePlayerDataAsync(string playerName, int score = -1, float survivalTime = -1f, int bonusCollected = -1)
    {
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("[FirebaseManager] Player name is missing. Data not saved.");
            return;
        }

        // Si no se pasan valores, se intentan leer desde UI
        if (score < 0) score = ParseIntFromText(scoreText);
        if (survivalTime < 0) survivalTime = ParseFloatFromText(survivalTimeText);
        if (bonusCollected < 0) bonusCollected = ParseIntFromText(bonusText);

        try
        {
            var playerData = new Dictionary<string, object>
            {
                { "PlayerName", playerName },
                { "Score", score },
                { "SurvivalTime", survivalTime },
                { "BonusCollected", bonusCollected },
                { "DateTime", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") }
            };

            await db.Collection(CollectionName).AddAsync(playerData);

            Debug.Log($"[FirebaseManager] Data saved for {playerName} (Score: {score}, Time: {survivalTime}, Bonus: {bonusCollected})");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[FirebaseManager] Failed to save data: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene los mejores jugadores de PlayerScores ordenados por Score descendente
    /// </summary>
    public async Task<List<Dictionary<string, object>>> GetHighscoresAsync(int limit = 10)
    {
        var highscores = new List<Dictionary<string, object>>();

        try
        {
            var query = db.Collection(CollectionName)
                          .OrderByDescending("Score")
                          .Limit(limit);

            var snapshot = await query.GetSnapshotAsync();

            foreach (var doc in snapshot.Documents)
            {
                highscores.Add(doc.ToDictionary());
            }

            Debug.Log($"[FirebaseManager] Retrieved {highscores.Count} highscores from {CollectionName}.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[FirebaseManager] Failed to load highscores: {ex.Message}");
        }

        return highscores;
    }

    // Utility: parse safely from UI text
    private int ParseIntFromText(TMP_Text text)
    {
        if (text == null) return 0;
        return int.TryParse(text.text, out int value) ? value : 0;
    }

    private float ParseFloatFromText(TMP_Text text)
    {
        if (text == null) return 0f;
        return float.TryParse(text.text, out float value) ? value : 0f;
    }
}
